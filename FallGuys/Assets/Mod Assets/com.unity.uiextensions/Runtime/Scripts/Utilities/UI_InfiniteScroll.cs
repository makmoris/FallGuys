/// Credit Tomasz Schelenz 
/// Sourced from - https://bitbucket.org/SimonDarksideJ/unity-ui-extensions/issues/81/infinite-scrollrect
/// Demo - https://www.youtube.com/watch?v=uVTV7Udx78k  - configures automatically.  - works in both vertical and horizontal (but not both at the same time)  - drag and drop  - can be initialized by code (in case you populate your scrollview content from code)
/// Updated by Febo Zodiaco - https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/issues/349/magnticinfinitescroll

using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
    /// <summary>
    /// Infinite scroll view with automatic configuration 
    /// 
    /// Fields
    /// - InitByUSer - in case your scrollrect is populated from code, you can explicitly Initialize the infinite scroll after your scroll is ready
    /// by calling Init() method
    /// 
    /// Notes
    /// - does not work in both vertical and horizontal orientation at the same time.
    /// - in order to work it disables layout components and size fitter if present(automatically)
    /// 
    /// </summary>
    [AddComponentMenu("UI/Extensions/UI Infinite Scroll")]
    public class UI_InfiniteScroll : MonoBehaviour
    {
        //if true user will need to call Init() method manually (in case the contend of the scrollview is generated from code or requires special initialization)
        [Tooltip("If false, will Init automatically, otherwise you need to call Init() method")]
        [HideInInspector]public bool InitByUser = false;
        protected ScrollRect _scrollRect;
        private ContentSizeFitter _contentSizeFitter;
        private VerticalLayoutGroup _verticalLayoutGroup;
        private HorizontalLayoutGroup _horizontalLayoutGroup;
        private GridLayoutGroup _gridLayoutGroup;
        protected bool _isVertical = false;
        protected bool _isHorizontal = false;
        private float _disableMarginX = 0;
        private float _disableMarginY = 0;
        private bool _hasDisabledGridComponents = false;
        protected List<RectTransform> items = new List<RectTransform>();
        private Vector2 _newAnchoredPosition = Vector2.zero;
        //TO DISABLE FLICKERING OBJECT WHEN SCROLL VIEW IS IDLE IN BETWEEN OBJECTS
        private float _threshold = 100f;
        private int _itemCount = 0;
        private float _recordOffsetX = 0;
        private float _recordOffsetY = 0;

        [SerializeField] private Canvas _canvas;
        [Space]
        [Header("Speed")]
        [SerializeField] private float maxSpeed;
        [SerializeField] private float searchStartSpeed;
        [Space]
        [SerializeField] private float currentSpeed;

        [Header("Scrolling time")]
        [SerializeField] private float scrollingTime;

        private float fullCircleValue = 0;
        private float itemWidth;

        private List<float> elementPositions = new List<float>();

        private bool initFullCircleValue;

        private bool isScrolling;
        private bool canStopScrolling;
        private bool isFullStop;

        Vector3 target = new Vector3();

        int targetIndex;

        private float startScrollingTime;

        private MapSelector mapSelector;

        protected virtual void Awake()
        {
            if (!InitByUser)
                Init();
        }


        private void FixedUpdate()
        {
            if (_scrollRect.content.rect.width != 0 && _horizontalLayoutGroup.spacing != 0 && !initFullCircleValue)
            {
                initFullCircleValue = true;
                fullCircleValue = _scrollRect.content.rect.width + _horizontalLayoutGroup.spacing;

                SetElementsPositionsValue();
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {

            }

            if (isScrolling)
            {
                if (!isFullStop)
                {
                    target = new Vector3(_scrollRect.content.localPosition.x + 10f, _scrollRect.content.localPosition.y,
                    _scrollRect.content.localPosition.z);
                    target += target;
                }

                if (currentSpeed < maxSpeed && !canStopScrolling)
                {
                    currentSpeed += 10;
                }

                if (canStopScrolling && !isFullStop)
                {
                    if (currentSpeed > searchStartSpeed)
                    {
                        currentSpeed -= 10;
                    }
                    else
                    {
                        isFullStop = true;
                        target = new Vector3(GetTargetPosition(targetIndex), _scrollRect.content.localPosition.y,
                    _scrollRect.content.localPosition.z);
                        Debug.Log(target);
                    }
                }

                _scrollRect.content.localPosition = Vector3.MoveTowards(_scrollRect.content.localPosition, target, currentSpeed * Time.deltaTime);

                if (isFullStop)
                {
                    if (Vector3.Distance(_scrollRect.content.localPosition, target) < 0.001f)
                    {
                        // Swap the position of the cylinder.
                        isScrolling = false;
                        Debug.Log("Переход на карту");
                        // анимашка выбранной карты, ее подсветка или типо того
                        SelectAnimation();
                        // переход на эту карту
                    }
                }
            }
        }

        private void SelectAnimation()
        {
            GameObject targetObject = items[targetIndex].gameObject;
            targetObject.transform.SetAsLastSibling();

            targetObject.transform.GetChild(targetObject.transform.childCount - 1).gameObject.SetActive(true);

            StartCoroutine(Scaling(targetObject));
        }

        public void CanStopScrolling()// сюда из MapSelector
        {
            float startTime = Time.time - startScrollingTime;

            if(startTime >= scrollingTime)// если уже прошло достаточно времени и сцена как раз загрузилась, то заканчиваем
            {
                canStopScrolling = true;
            }
            else// если недостаточно покрутили, то докручиваем
            {
                float waitTime = scrollingTime - startTime;
                StartCoroutine(WaitAndStopScrolling(waitTime));
            }

            Debug.Log($"Стартовое время скроллинга = {startScrollingTime}; Время с начала игры = {Time.time}; Время начала остановки = {startTime}");
        }

        public void SetTargetIndex(GameObject _targetObj, MapSelector _mapSelector)// сюда из MapSelector приходит нужная сцена
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].gameObject == _targetObj)
                {
                    targetIndex = i;
                    isScrolling = true;
                    startScrollingTime = Time.time;
                    break;
                }
            }

            mapSelector = _mapSelector;
        }

        private void SetElementsPositionsValue()
        {
            int elementsCount = items.Count;

            if (elementsCount % 2 == 0)// нужно, чтобы всегда было нечетное количество элементов
            {
                // нужно, чтобы изначально было нечетное число, иначе ошибка
                Debug.LogError("The map selection scroll must contain an odd number of elements");
            }

            int centerElementIndex = Mathf.CeilToInt(elementsCount / 2f) - 1;

            for (int i = 0; i < items.Count; i++)// инициализируем
            {
                elementPositions.Add(0);
            }

            elementPositions[centerElementIndex] = fullCircleValue;// центр

            int val = 1;
            for (int i = centerElementIndex - 1; i >= 0; i--)
            {
                elementPositions[i] = val * itemWidth + val * _horizontalLayoutGroup.spacing;

                val++;
            }

            for (int i = items.Count - 1; i > centerElementIndex; i--)
            {
                elementPositions[i] = val * itemWidth + val * _horizontalLayoutGroup.spacing;

                val++;
            }
        }

        private float GetTargetPosition(int targetElemntIdex)
        {
            float targetPos;

            //int numberOfCircle = Mathf.FloorToInt(_scrollRect.content.localPosition.x / fullCircleValue);
            int numberOfCircle = Mathf.RoundToInt(_scrollRect.content.localPosition.x / fullCircleValue);

            if (elementPositions[targetElemntIdex] == fullCircleValue)// середина
            {
                targetPos = (numberOfCircle * fullCircleValue) + elementPositions[targetElemntIdex];
            }
            else
            {
                targetPos = (numberOfCircle * fullCircleValue) + elementPositions[targetElemntIdex];
                // чтобы точно не откатился назад
                if (targetPos < _scrollRect.content.localPosition.x) targetPos += fullCircleValue;
                Debug.Log($"{targetPos} = {numberOfCircle} * {fullCircleValue} + {elementPositions[targetElemntIdex]} ||| было {_scrollRect.content.localPosition.x }");
            }

            return targetPos;
        }

        public virtual void SetNewItems(ref List<Transform> newItems)
        {
            Debug.Log("SetNewItems");
            if (_scrollRect != null)
            {
                if (_scrollRect.content == null && newItems == null)
                {
                    return;
                }

                if (items != null)
                {
                    items.Clear();
                    elementPositions.Clear();
                }

                for (int i = _scrollRect.content.childCount - 1; i >= 0; i--)
                {
                    Transform child = _scrollRect.content.GetChild(i);
                    child.SetParent(null);
                    GameObject.DestroyImmediate(child.gameObject);
                }

                foreach (Transform newItem in newItems)
                {
                    newItem.SetParent(_scrollRect.content);
                }

                SetItems();
            }
        }

        private void SetItems()
        {
            Debug.Log("SetItems");
            for (int i = 0; i < _scrollRect.content.childCount; i++)
            {
                var element = _scrollRect.content.GetChild(i).GetComponent<RectTransform>();

                items.Add(element);
            }

            _itemCount = _scrollRect.content.childCount;
            itemWidth = items[0].rect.width;
        }

        public void Init()
        {
            if (GetComponent<ScrollRect>() != null)
            {
                _scrollRect = GetComponent<ScrollRect>();
                _scrollRect.onValueChanged.AddListener(OnScroll);
                _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

                if (_scrollRect.content.GetComponent<VerticalLayoutGroup>() != null)
                {
                    _verticalLayoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
                }
                if (_scrollRect.content.GetComponent<HorizontalLayoutGroup>() != null)
                {
                    _horizontalLayoutGroup = _scrollRect.content.GetComponent<HorizontalLayoutGroup>();
                }
                if (_scrollRect.content.GetComponent<GridLayoutGroup>() != null)
                {
                    _gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
                }
                if (_scrollRect.content.GetComponent<ContentSizeFitter>() != null)
                {
                    _contentSizeFitter = _scrollRect.content.GetComponent<ContentSizeFitter>();
                }

                _isHorizontal = _scrollRect.horizontal;
                _isVertical = _scrollRect.vertical;

                if (_isHorizontal && _isVertical)
                {
                    Debug.LogError("UI_InfiniteScroll doesn't support scrolling in both directions, please choose one direction (horizontal or vertical)");
                }

                SetItems();
            }
            else
            {
                Debug.LogError("UI_InfiniteScroll => No ScrollRect component found");
            }
        }

        void DisableGridComponents()
        {
            if (_isVertical)
            {
                _recordOffsetY = items[1].GetComponent<RectTransform>().anchoredPosition.y - items[0].GetComponent<RectTransform>().anchoredPosition.y;
                if (_recordOffsetY < 0)
                {
                    _recordOffsetY *= -1;
                }
                _disableMarginY = _recordOffsetY * _itemCount / 2;
            }
            if (_isHorizontal)
            {
                _recordOffsetX = items[1].GetComponent<RectTransform>().anchoredPosition.x - items[0].GetComponent<RectTransform>().anchoredPosition.x;
                if (_recordOffsetX < 0)
                {
                    _recordOffsetX *= -1;
                }
                _disableMarginX = _recordOffsetX * _itemCount / 2;
            }

            if (_verticalLayoutGroup)
            {
                _verticalLayoutGroup.enabled = false;
            }
            if (_horizontalLayoutGroup)
            {
                _horizontalLayoutGroup.enabled = false;
            }
            if (_contentSizeFitter)
            {
                _contentSizeFitter.enabled = false;
            }
            if (_gridLayoutGroup)
            {
                _gridLayoutGroup.enabled = false;
            }
            _hasDisabledGridComponents = true;
        }

        public void OnScroll(Vector2 pos)
        {
            if (!_hasDisabledGridComponents)
                DisableGridComponents();

            for (int i = 0; i < items.Count; i++)
            {
                if (_isHorizontal)
                {
                    if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x > _disableMarginX + _threshold)
                    {
                        _newAnchoredPosition = items[i].anchoredPosition;
                        _newAnchoredPosition.x -= _itemCount * _recordOffsetX;
                        items[i].anchoredPosition = _newAnchoredPosition;
                        _scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();


                    }
                    //else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x < -_disableMarginX)
                    //{
                    //    _newAnchoredPosition = items[i].anchoredPosition;
                    //    _newAnchoredPosition.x += _itemCount * _recordOffsetX;
                    //    items[i].anchoredPosition = _newAnchoredPosition;
                    //    _scrollRect.content.GetChild(0).transform.SetAsLastSibling();
                    //}
                }

                if (_isVertical)
                {
                    if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y > _disableMarginY + _threshold)
                    {
                        _newAnchoredPosition = items[i].anchoredPosition;
                        _newAnchoredPosition.y -= _itemCount * _recordOffsetY;
                        items[i].anchoredPosition = _newAnchoredPosition;
                        _scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
                    }
                    else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y < -_disableMarginY)
                    {
                        _newAnchoredPosition = items[i].anchoredPosition;
                        _newAnchoredPosition.y += _itemCount * _recordOffsetY;
                        items[i].anchoredPosition = _newAnchoredPosition;
                        _scrollRect.content.GetChild(0).transform.SetAsLastSibling();
                    }
                }
            }
        }

        IEnumerator WaitAndStopScrolling(float time)
        {
            yield return new WaitForSeconds(time);
            canStopScrolling = true;
            Debug.Log($"Скролл остановился. Время = {Time.time}");
        }

        IEnumerator Scaling(GameObject selectObj)
        {
            Vector3 targetScale = new Vector3(1.1f, 1.1f, 1f);

            float elapsedTime = 0f;
            float fadeDuration = 7.5f;

            while (Vector3.Distance(selectObj.transform.localScale, targetScale) > 0.001f)
            {
                elapsedTime += Time.deltaTime;

                selectObj.transform.localScale = Vector3.Lerp(selectObj.transform.localScale, targetScale, elapsedTime / fadeDuration);
                yield return new WaitForFixedUpdate();
                //yield return null;
            }

            yield return new WaitForSeconds(1f);
            mapSelector.GoToLevel();
        }
    }
}
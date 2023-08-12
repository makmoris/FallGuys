using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VehicleBehaviour;

public class PostLevelPlaceController : MonoBehaviour
{
    protected GameManager _gameManager;
    public GameManager GameManager { set => _gameManager = value; }

    [Header("Post Level UI Controller")]
    [SerializeField] protected PostLevelUIController postLevelUIController;

    [Header("Cameras")]
    [SerializeField] protected GameObject gameCameraGO;
    [SerializeField] protected GameObject postCameraGO;

    [Header("UI")]
    [SerializeField] protected GameObject levelUIGO;
    [SerializeField] protected GameObject postUIGO;

    [Header("Winner Window")]
    [SerializeField] private GameObject winnerWindowGO;
    [Space]
    [SerializeField] private Transform winnerPlace;
    [Space]
    [SerializeField] private TextMeshProUGUI winnerNameText;
    [SerializeField] private TextMeshProUGUI cupsText;

    [Header("Player Elimination Place")]
    [SerializeField] private GameObject eleminationPlaceGO;
    [Space]
    [SerializeField] private float minTimeBeforDump = 1f;
    [SerializeField] private float maxTimeBeforDump = 2.5f;

    private List<PostPlace> postPlacesList = new List<PostPlace>();

    [Header("Next Game Stage")]
    [SerializeField] private float timeBeforeLoadNextGameStage = 4f;

    private void Awake()
    {
        if (!gameCameraGO.activeSelf) gameCameraGO.SetActive(true);
        if (postCameraGO.activeSelf) postCameraGO.SetActive(false);
        if (postUIGO.activeSelf) postUIGO.SetActive(false);
        if (!levelUIGO.activeSelf) levelUIGO.SetActive(true);

        FillPostPlacesList();
    }

    public void ShowPostPlace(List<GameObject> _winnersList, List<GameObject> _losersList, bool _isCurrentPlayerWinner)
    {
        if (_winnersList.Count == 1)// игра завершена, остался победитель. Показываем вин окно
        {
            ShowWinnerWindow(_winnersList[0]);
        }
        else// игра не завершена, еще будут другие режими. Показ анимации выбывания и загрузка следующей карты
        {
            ShowElimination(_winnersList, _losersList, _isCurrentPlayerWinner);
        }
    }

    private void ShowElimination(List<GameObject> _winnersList, List<GameObject> _losersList, bool _isCurrentPlayerWinner)
    {
        foreach (var winner in _winnersList)
        {
            if(postPlacesList.Count > 0)
            {
                winner.SetActive(true);

                PostPlace postPlace = postPlacesList[0];
                Transform postPlaceTransform = postPlace.GetPlaceTransform();
               
                winner.transform.SetParent(postPlaceTransform);
                winner.transform.localPosition = Vector3.zero;
                winner.transform.localRotation = Quaternion.Euler(Vector3.zero);

                postPlacesList.Remove(postPlace);
            }
        }

        foreach (var loser in _losersList)
        {
            if (postPlacesList.Count > 0)
            {
                loser.SetActive(true);

                PostPlace postPlace = postPlacesList[0];
                Transform postPlaceTransform = postPlace.GetPlaceTransform();

                loser.transform.SetParent(postPlaceTransform);
                loser.transform.localPosition = Vector3.zero;
                loser.transform.localRotation = Quaternion.Euler(Vector3.zero);

                float timeBeforDump = Random.Range(minTimeBeforDump, maxTimeBeforDump);

                postPlace.DumpThisCar(timeBeforDump);

                postPlacesList.Remove(postPlace);
            }
        }

        postLevelUIController.SetNumberOfWinnersText(_winnersList.Count);

        postUIGO.SetActive(true);

        if (_isCurrentPlayerWinner) levelUIGO.SetActive(false);

        postCameraGO.SetActive(true);
        gameCameraGO.SetActive(false);

        LoadNextGameStage();
    }

    private void ShowWinnerWindow(GameObject winnerGO)
    {
        gameCameraGO.SetActive(false);
        postCameraGO.SetActive(false);

        levelUIGO.SetActive(false);
        postUIGO.SetActive(false);

        winnerGO.SetActive(true);

        winnerGO.GetComponent<Rigidbody>().isKinematic = true;

        winnerGO.transform.SetParent(winnerPlace);
        winnerGO.transform.localPosition = Vector3.zero;
        winnerGO.transform.localRotation = Quaternion.Euler(Vector3.zero);

        PlayerName winnerName = winnerGO.GetComponent<PlayerName>();
        winnerName.HideNameDisplay();
        winnerNameText.text = winnerName.Name;

        Suspension[] allChieldSuspensions = winnerGO.GetComponentsInChildren<Suspension>();
        foreach (var sus in allChieldSuspensions)
        {
            sus.enabled = false;
            sus.transform.localPosition = new Vector3(sus.transform.localPosition.x, -0.1f, sus.transform.localPosition.z);
        }

        Transform[] allChieldTransforms = winnerGO.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldTransforms)
        {
            trn.gameObject.layer = 5;
        }

        winnerWindowGO.SetActive(true);
    }

    private void LoadNextGameStage()
    {
        StartCoroutine(WaitAndLoadNextGameStage());
    }

    private void FillPostPlacesList()
    {
        PostPlace[] allChieldPostPlaces = eleminationPlaceGO.GetComponentsInChildren<PostPlace>();
       
        foreach (var place in allChieldPostPlaces)
        {
            postPlacesList.Add(place);
        }

        System.Random rand = new();

        for (int i = postPlacesList.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            var tmp = postPlacesList[j];
            postPlacesList[j] = postPlacesList[i];
            postPlacesList[i] = tmp;
        }
    }

    IEnumerator WaitAndLoadNextGameStage()
    {
        yield return new WaitForSeconds(timeBeforeLoadNextGameStage);

        bool itWasNotTheLastGameStage = _gameManager.StartGameStage();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VehicleBehaviour;
using UnityEngine.UI;

public class PostLevelPlaceController : MonoBehaviour
{
    protected GameManager _gameManager;

    [Header("Post Level UI Controller")]
    [SerializeField] protected PostLevelUIController postLevelUIController;

    [Header("Cameras")]
    [SerializeField] protected GameObject gameCameraGO;
    [SerializeField] protected GameObject postCameraGO;

    [Header("UI")]
    [SerializeField] protected GameObject levelCanvasGO;
    [SerializeField] protected GameObject canvasForLeaveButton;
    [SerializeField] protected GameObject postPlaceCanvasGO;

    [Header("Winner Window")]
    [SerializeField] private GameObject winnerWindowGO;
    [Space]
    [SerializeField] private Transform winnerPlace;
    [Space]
    [SerializeField] private TextMeshProUGUI winnerNameText;
    [SerializeField] private TextMeshProUGUI cupsText;
    [SerializeField] private Button continueButton;

    [Header("Player Elimination Place")]
    [SerializeField] private GameObject eleminationPlaceGO;
    [Space]
    [SerializeField] private float minTimeBeforDump = 1f;
    [SerializeField] private float maxTimeBeforDump = 2f;

    private List<PostPlace> postPlacesList = new List<PostPlace>();

    [Header("Next Game Stage")]
    [SerializeField] private float timeBeforeLoadNextGameStage = 4f;

    private void Awake()
    {
        if (!gameCameraGO.activeSelf) gameCameraGO.SetActive(true);
        if (postCameraGO.activeSelf) postCameraGO.SetActive(false);
        if (postPlaceCanvasGO.activeSelf) postPlaceCanvasGO.SetActive(false);

        FillPostPlacesList();
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;

        SetContinueButtonEvent();
    }

    public void ShowPostPlace(List<GameObject> _winnersList, List<GameObject> _losersList, bool _isCurrentPlayerWinner, GameObject _currentPlayerGO)
    {
        DisableAudioListeners(_winnersList);
        DisableAudioListeners(_losersList);

        if (_winnersList.Count == 1)// игра завершена, остался победитель. Показываем вин окно
        {
            ShowWinnerWindow(_winnersList[0], _isCurrentPlayerWinner);
        }
        else// игра не завершена, еще будут другие режими. Показ анимации выбывания и загрузка следующей карты
        {
            ShowElimination(_winnersList, _losersList, _currentPlayerGO);
            LoadNextGameStage();
        }
    }

    private void ShowElimination(List<GameObject> _winnersList, List<GameObject> _losersList, GameObject _currentPlayerGO)
    {
        foreach (var winner in _winnersList)
        {
            if(postPlacesList.Count > 0)
            {
                winner.SetActive(true);

                PostPlace postPlace = postPlacesList[0];
                Transform postPlaceTransform = postPlace.GetPlaceTransform();

                if (winner == _currentPlayerGO) postPlace.RecolorCellForPlayer();
               
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

                if (loser == _currentPlayerGO) postPlace.RecolorCellForPlayer();

                loser.transform.SetParent(postPlaceTransform);
                loser.transform.localPosition = Vector3.zero;
                loser.transform.localRotation = Quaternion.Euler(Vector3.zero);

                float timeBeforDump = Random.Range(minTimeBeforDump, maxTimeBeforDump);

                postPlace.DumpThisCar(timeBeforDump);

                postPlacesList.Remove(postPlace);
            }
        }

        postLevelUIController.SetNumberOfWinnersText(_winnersList.Count);

        postPlaceCanvasGO.SetActive(true);

        //if (_isCurrentPlayerWinner) levelCanvasGO.SetActive(false);
        levelCanvasGO.SetActive(false);

        postCameraGO.SetActive(true);
        gameCameraGO.SetActive(false);
    }

    private void ShowWinnerWindow(GameObject winnerGO, bool _isCurrentPlayerWinner)
    {
        gameCameraGO.SetActive(false);
        postCameraGO.SetActive(false);

        levelCanvasGO.SetActive(false);
        canvasForLeaveButton.SetActive(false);
        postPlaceCanvasGO.SetActive(false);

        winnerGO.SetActive(true);

        winnerGO.GetComponent<Rigidbody>().isKinematic = true;

        winnerGO.transform.SetParent(winnerPlace);
        winnerGO.transform.localPosition = Vector3.zero;
        winnerGO.transform.localRotation = Quaternion.Euler(Vector3.zero);

        PlayerName winnerName = winnerGO.GetComponent<PlayerName>();
        winnerName.HideNameDisplay();
        winnerNameText.text = winnerName.Name;

        int currentPlayerCupsValue = _gameManager.GetCurrentPlayerCupsValue();

        if (_isCurrentPlayerWinner)
        {
            cupsText.text = currentPlayerCupsValue.ToString();
        }
        else
        {
            int minValue = 10;
            if (currentPlayerCupsValue > 50) minValue = currentPlayerCupsValue - 45;

            int randCupsValue = Random.Range(minValue, currentPlayerCupsValue + 50);

            cupsText.text = randCupsValue.ToString();
        }

        //Suspension[] allChieldSuspensions = winnerGO.GetComponentsInChildren<Suspension>();
        //foreach (var sus in allChieldSuspensions)
        //{
        //    sus.enabled = false;
        //    sus.transform.localPosition = new Vector3(sus.transform.localPosition.x, -0.1f, sus.transform.localPosition.z);
        //}

        Transform[] allChieldTransforms = winnerGO.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldTransforms)
        {
            trn.gameObject.layer = 5;
        }

        winnerWindowGO.SetActive(true);
    }

    private void DisableAudioListeners(List<GameObject> playersList)
    {
        foreach (var player in playersList)
        {
            player.GetComponent<AudioListener>().enabled = false;
        }
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

        _gameManager.StartGameStage();
    }

    private void SetContinueButtonEvent()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(_gameManager.PlayerClickedExitToLobby);
        continueButton.onClick.AddListener(DeactivateContinueButton);
    }

    private void DeactivateContinueButton()
    {
        continueButton.interactable = false;
    }
}

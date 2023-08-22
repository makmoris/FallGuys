using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lexic;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class GameMode
    {
        public GameModeEnum gameMode;
        public GameModeScenes gameModeScenes;
    }

    [System.Serializable]
    public class GameStage
    {
        [SerializeField] private string name;
        [Space]
        public int numberOfPlayers;
        public int numberOfWinners;
        public List<GameMode> gameModesList;
    }

    #region Debug GameStagesList
    [System.Serializable]
    public class GameStagesList
    {
        public List<GameStage> gameStagesList;
    }
    #endregion

    [Header("SceneLoader")]
    [SerializeField] private SceneLoader sceneLoader;

    [Header("Player Character")]
    [SerializeField] private CharacterManager characterManager;

    #region Debug GameStagesList
    [Space]
    [Header("Debug Game Stages")]
    [SerializeField] private int debugIndex;
    [SerializeField] private List<GameStagesList> AllGameStagesList;
    [Header("Current Debug Game Stages")]
    [SerializeField]private List<GameStage> gameStagesList = new List<GameStage>();
    #endregion

    //[Header("Game Stages")] // Correct without Debug GameStagesList
    //[SerializeField] private List<GameStage> gameStagesList; // Correct

    private int currentGameStage;

    private string previousGameModeEnumIndexKey = "PreviousGameModeEnum";

    private List<IPlayerData> players = new List<IPlayerData>();
    public List<IPlayerData> Players => players;

    [Space]
    [SerializeField] private List<string> testLosersNames;
    private List<IPlayerData> losersList = new List<IPlayerData>();// first in list - last in award
    private IPlayerData currentPlayer;

    [SerializeField]private int currentPlayerPlace;
    private bool isShowPlayerStatisticsWindowAfterGameInTheLobby;

    private bool isObserverMode;
    public bool IsObserverMode => isObserverMode;

    private bool isFirstStart = true;

    private NameGenerator nameGenerator;
    private List<string> namesList = new List<string>();

    private static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        nameGenerator = new NameGenerator();
    }

    private void OnEnable()
    {
        sceneLoader.lobbyOpenEvent += ResetValuesAfterLeavingToLobby;
        sceneLoader.lobbyOpenEvent += CheckTheDisplayOfThePlayerStatisticsWindowAfterGameInTheLobby;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartGameStage();
        }
    }

    public void StartGameStage()// ��������� �����. ���������� �� ������ "Play"
    {
        #region Debug GameStagesList
        gameStagesList = AllGameStagesList[debugIndex].gameStagesList;
        #endregion

        SceneField sceneToLoad;
       
        if (isFirstStart)
        {
            // �������� ������� ��� ��������, �� ������� ����� ������
            sceneToLoad = GetSceneToLoad();

            CreatePlayersForOneGameSession();

            isFirstStart = false;
            isObserverMode = false;

            isShowPlayerStatisticsWindowAfterGameInTheLobby = false;

            sceneLoader.LoadNextLevelSceneWithAnimation(sceneToLoad);
        }
        else
        {
            int nextStageIndex = currentGameStage + 1;
            if (nextStageIndex < gameStagesList.Count)
            {
                currentGameStage = nextStageIndex;

                sceneToLoad = GetSceneToLoad();

                sceneLoader.LoadNextLevelSceneWithAnimation(sceneToLoad);
            }
            else // ������ ������� ����� ���������. ����� ���� ���������� �� ����, ������ �� ����������
            {
                currentGameStage = 0;

                //sceneLoader.PrepareLobbyScene(); // ������� �������� ����� ����
            }
        }
    }

    public void StartGameStageFromTutorialWindow()
    {
        CreatePlayersForOneGameSession();

        isFirstStart = false;
    }

    public void EliminateLosersFromList(List<string> losersNames)
    {
        for (int i = 0; i < losersNames.Count; i++)
        {
            foreach (var player in players)
            {
                string loserName = player.Name;

                if (!losersList.Contains(player))// ����� �������� ��������
                {
                    if (loserName == losersNames[i])
                    {
                        players.Remove(player);
                        losersList.Add(player);
                        testLosersNames.Add(player.Name);

                        if (loserName == currentPlayer.Name)
                        {
                            isObserverMode = true;
                        }

                        break;
                    }
                }
            }
        }
    }

    public int GetNumberOfWinners()
    {
        return gameStagesList[currentGameStage].numberOfWinners;
    }

    public void PlayerClickedExitToLobby()
    {
        // �������� ����� ������
        GetCurrentPlayerPlace();
        // ��������� � ����� � ��������, ��� ���� �������� ���� ���������� ������
        isShowPlayerStatisticsWindowAfterGameInTheLobby = true;
        sceneLoader.LoadLobbyScene();
    }

    public void PlayerClickedExitToLobbyFromSettingsWindow()
    {
        sceneLoader.LoadLobbyScene();
    }

    public int GetCurrentPlayerCupsValue()
    {
        return CurrencyManager.Instance.Cups;
    }

    private void CheckTheDisplayOfThePlayerStatisticsWindowAfterGameInTheLobby()
    {
        if (isShowPlayerStatisticsWindowAfterGameInTheLobby)
        {
            // ���������� ���� ����� ������ ����� �����
            PlayerStatisticsAfterBattleWindow playerStatisticsAfterBattleWindow = FindObjectOfType<PlayerStatisticsAfterBattleWindow>(true);
            if (playerStatisticsAfterBattleWindow != null)
            {
                playerStatisticsAfterBattleWindow.gameObject.SetActive(true);
                playerStatisticsAfterBattleWindow.ShowStatistics(currentPlayerPlace);
            }
        }
    }

    private void CreatePlayersForOneGameSession()
    {
        ClearPlayersForOneGameSession();

        Player player = CreatePlayer();
        players.Add(player);

        currentPlayer = player;

        for (int i = 0; i < gameStagesList[currentGameStage].numberOfPlayers - 1; i++)
        {
            PlayerAI aiPlayer = CreateAIPlayer();
            players.Add(aiPlayer);
        }
    }

    private void GetCurrentPlayerPlace()// called when player click leave button or when game ower with one winner
    {
        if (isObserverMode)
        {
            int index = 0;

            if (players.Count > 0) index = players.Count - 1;

            losersList.Reverse();

            foreach (var loser in losersList)
            {
                string loserName = loser.Name;

                if (loserName == currentPlayer.Name)
                {
                    currentPlayerPlace = index + 1;
                    break;
                }

                index++;
            }
        }
        else // ������ ����� ������� ����, � ���� ������ �����
        {
            currentPlayerPlace = 0;
        }
    }

    private void ClearPlayersForOneGameSession()
    {
        players.Clear();
        losersList.Clear();
        testLosersNames.Clear();
    }

    private Player CreatePlayer()
    {
        string playerName = "Player";

        namesList.Add(playerName);

        GameObject playerPrefab = characterManager.GetPlayerPrefab();

        PlayerDefaultData playerDefaultData = characterManager.GetPlayerDefaultData();

        Weapon playerWeapon = characterManager.GetPlayerWeapon();
        
        Player player = new Player(playerName, playerPrefab, playerDefaultData, playerWeapon);

        return player;
    }

    private PlayerAI CreateAIPlayer()
    {
        //string aiName = nameGenerator.GetNextRandomName();
        int rand = Random.Range(0, 999);
        string aiName = $"Puncher_{rand}";

        if (namesList.Contains(aiName))
        {
            int randomNumber = Random.Range(1, 10);

            aiName = aiName.Insert(aiName.Length, randomNumber.ToString());
        }

        namesList.Add(aiName);

        Material aiColorMaterial = characterManager.GetRandomMaterial();

        Weapon aiWeapon = characterManager.GetRandomWeapon();

        PlayerAI ai = new PlayerAI(aiName, aiColorMaterial, aiWeapon);

        return ai;
    }

    private void ResetValuesAfterLeavingToLobby()
    {
        isFirstStart = true;
        currentGameStage = 0;

        namesList.Clear();

        PlayerPrefs.SetInt(previousGameModeEnumIndexKey, 999999);
    }


    private SceneField GetSceneToLoad()
    {
        int gameModeCount = gameStagesList[currentGameStage].gameModesList.Count;
        int randomGameModeIndex = Random.Range(0, gameModeCount);

        int previousGameModeEnumIndex = PlayerPrefs.GetInt(previousGameModeEnumIndexKey, 999999);
        
        if (previousGameModeEnumIndex == (int)gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameMode)
        {
            if (gameModeCount > 1)
            {
                if (randomGameModeIndex == 0) randomGameModeIndex++;
                else if (randomGameModeIndex == gameModeCount - 1) randomGameModeIndex--;
                else randomGameModeIndex++;
            }
        }
        PlayerPrefs.SetInt(previousGameModeEnumIndexKey, randomGameModeIndex);

        SceneField scene = gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameModeScenes.GetRandomScene();

        return scene;
    }

    private void OnDisable()
    {
        sceneLoader.lobbyOpenEvent -= ResetValuesAfterLeavingToLobby;
        sceneLoader.lobbyOpenEvent -= CheckTheDisplayOfThePlayerStatisticsWindowAfterGameInTheLobby;
    }
}

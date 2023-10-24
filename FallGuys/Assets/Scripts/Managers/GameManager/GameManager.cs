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
        //public GameModeScenes gameModeScenes;
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
    [SerializeField] private bool useRandomDebugIndex;
    [SerializeField] private int debugIndex;
    [SerializeField] private List<GameStagesList> AllGameStagesList;
    //[Header("Current Debug Game Stages")]
    private List<GameStage> gameStagesList = new List<GameStage>();
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

    [Header("Game Mode Scenes")]
    [SerializeField] private List<GameModeScenes> gameModeScenes;

    #region Analytics
    private int currentPlayerTookPlaceInGameStage;
    #endregion

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

    public void StartGameStage()// начальная точка. Вызывается по кнопке "Play"
    {
        #region Debug GameStagesList
        if(useRandomDebugIndex) debugIndex = Random.Range(0, AllGameStagesList.Count);

        gameStagesList = AllGameStagesList[debugIndex].gameStagesList;
        #endregion

        SceneField sceneToLoad;

        #region Analytics
        AnalyticsManager.Instance.UpdateUserPlay(!isObserverMode);
        #endregion

        if (isFirstStart)
        {
            // получаем локацию для загрузки, на которой будем играть
            sceneToLoad = GetSceneToLoad();

            CreatePlayersForOneGameSession();

            isFirstStart = false;
            isObserverMode = false;

            isShowPlayerStatisticsWindowAfterGameInTheLobby = false;

            sceneLoader.LoadNextLevelSceneWithAnimation(sceneToLoad);

            #region Analytics
            AnalyticsManager.Instance.UpdateUserPlay(!isObserverMode);

            AnalyticsManager.Instance.UpdateBattleId();
            AnalyticsManager.Instance.UpdateBattlesAmount();
            AnalyticsManager.Instance.UpdateConfigLevelsID(debugIndex);

            AnalyticsManager.Instance.GameStart();
            #endregion
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
            //else // значит текущий режим последний. После него переходить не надо, ничего не подгружаем
            //{
            //    currentGameStage = 0;

            //    //sceneLoader.PrepareLobbyScene(); // сделать загрузку через окно
            //}

            #region Analytics
            AnalyticsManager.Instance.UpdatePlayersAmount(gameStagesList[currentGameStage].numberOfPlayers);
            #endregion
        }

        #region Analytics
        AnalyticsManager.Instance.LevelStart();
        #endregion
    }

    public void StartGameStageFromTutorialWindow()
    {
        #region Analytics
        AnalyticsManager.Instance.UpdateUserPlay(!isObserverMode);
        #endregion

        CreatePlayersForOneGameSession();

        isFirstStart = false;

        #region Analytics
        AnalyticsManager.Instance.UpdateBattleId();
        AnalyticsManager.Instance.UpdateBattlesAmount();
        AnalyticsManager.Instance.UpdateConfigLevelsID(debugIndex);

        AnalyticsManager.Instance.GameStart();
        #endregion
    }

    public void EliminateLosersFromList(List<string> losersNames)
    {
        currentPlayerTookPlaceInGameStage = 0;

        for (int i = 0; i < losersNames.Count; i++)
        {
            foreach (var player in players)
            {
                string loserName = player.Name;

                if (!losersList.Contains(player))// чтобы избежать повторов
                {
                    if (loserName == losersNames[i])
                    {
                        players.Remove(player);
                        losersList.Add(player);
                        testLosersNames.Add(player.Name);

                        if (loserName == currentPlayer.Name)
                        {
                            isObserverMode = true;

                            #region Analytics
                            currentPlayerTookPlaceInGameStage = players.Count + 1;

                            AnalyticsManager.Instance.PlayerFailLevel();
                            #endregion
                        }

                        break;
                    }
                }
            }
        }

        #region Analytics
        if (!isObserverMode) AnalyticsManager.Instance.PlayerCompleteLevel();
        #endregion
    }

    public void SetWinnersNamesList(List<string> winnersNames)
    {
        if (!isObserverMode)
        {
            currentPlayerTookPlaceInGameStage = 0;
            int counter = 0;

            foreach (var winnerName in winnersNames)
            {
                counter++;
                if(winnerName == currentPlayer.Name)
                {
                    currentPlayerTookPlaceInGameStage = counter;
                    break;
                }
            }
        }

        #region Analytics
        AnalyticsManager.Instance.LevelFinish(currentPlayerTookPlaceInGameStage);

        if (currentGameStage == gameStagesList.Count - 1)
        {
            UpdateCurrentPlayerPlace();
            AnalyticsManager.Instance.GameFinish(currentPlayerPlace);
        }
        #endregion
    }

    public int GetNumberOfWinners()
    {
        return gameStagesList[currentGameStage].numberOfWinners;
    }

    public void PlayerClickedExitToLobby()
    {
        // получаем место игрока
        UpdateCurrentPlayerPlace();
        // переходим в лобби с пометкой, что надо показать окно статистики игрока
        isShowPlayerStatisticsWindowAfterGameInTheLobby = true;
        sceneLoader.LoadLobbyScene();

        #region Analytics
        AnalyticsManager.Instance.PlayerLeaveGame();
        #endregion
    }

    public void PlayerClickedExitToLobbyFromSettingsWindow()
    {
        sceneLoader.LoadLobbyScene();

        #region Analytics
        AnalyticsManager.Instance.PlayerLeaveGame();
        #endregion
    }

    public int GetCurrentPlayerCupsValue()
    {
        return CurrencyManager.Instance.Cups;
    }

    private void CheckTheDisplayOfThePlayerStatisticsWindowAfterGameInTheLobby()
    {
        if (isShowPlayerStatisticsWindowAfterGameInTheLobby)
        {
            // показываем окно статы игрока после битвы
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
        #region Debug GameStagesList
        gameStagesList = AllGameStagesList[debugIndex].gameStagesList;
        #endregion

        ClearPlayersForOneGameSession();

        Player player = CreatePlayer();
        players.Add(player);

        currentPlayer = player;

        for (int i = 0; i < gameStagesList[currentGameStage].numberOfPlayers - 1; i++)
        {
            PlayerAI aiPlayer = CreateAIPlayer();
            players.Add(aiPlayer);
        }

        #region Analytics
        AnalyticsManager.Instance.UpdatePlayersAmount(gameStagesList[currentGameStage].numberOfPlayers);
        #endregion
    }

    private void UpdateCurrentPlayerPlace()// called when player click leave button or when game ower with one winner
    {
        bool playerPlaceIsKnown = false;

        int counter = 0;
        foreach (var loser in losersList)
        {
            if (loser.Name == currentPlayer.Name)
            {
                playerPlaceIsKnown = true;

                currentPlayerPlace = (players.Count + losersList.Count) - counter;

                break;
            }

            counter++;
        }

        if (!playerPlaceIsKnown)
        {
            if (players.Count == 1)
            {
                currentPlayerPlace = 1;
            }
            else Debug.LogError("Player is still playing");
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

        GameObject aiPrefab = characterManager.GetRandomCarPrefab();

        Material aiColorMaterial = characterManager.GetRandomMaterial();

        Weapon aiWeapon = characterManager.GetRandomWeapon();

        PlayerAI ai = new PlayerAI(aiName, aiPrefab, aiColorMaterial, aiWeapon);

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

        GameModeScenes _gameModeScene;
        foreach (var gameMode in gameModeScenes)
        {
            if(gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameMode == gameMode.GameMode)
            {
                _gameModeScene = gameMode;
                SceneField scene = _gameModeScene.GetRandomScene();

                #region Analytics
                AnalyticsManager.Instance.UpdateGenreID(_gameModeScene.GameMode.ToString());
                AnalyticsManager.Instance.UpdateMapID(scene.SceneName);
                #endregion

                return scene;
            }
        }

        Debug.LogError($"Game mode {gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameMode} not found in Game Mode List");
        return null;

        //SceneField scene = gameStagesList[currentGameStage].gameModesList[randomGameModeIndex]._gameModeScene.GetRandomScene();
        //return scene;
    }

    private void OnDisable()
    {
        sceneLoader.lobbyOpenEvent -= ResetValuesAfterLeavingToLobby;
        sceneLoader.lobbyOpenEvent -= CheckTheDisplayOfThePlayerStatisticsWindowAfterGameInTheLobby;
    }
}

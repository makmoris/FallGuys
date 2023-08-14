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
        public int numberOfPlayers;
        public int numberOfWinners;
        public List<GameMode> gameModesList;
    }

    [Header("SceneLoader")]
    [SerializeField] private SceneLoader sceneLoader;

    [Header("Player Character")]
    [SerializeField] private CharacterManager characterManager;

    [Header("Game Stages")]
    [SerializeField] private List<GameStage> gameStagesList;

    private int currentGameStage;
    private int currentGameMode;

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

    public void StartGameStage()// начальная точка. Вызывается по кнопке "Play"
    {
        SceneField sceneToLoad;
       
        if (isFirstStart)
        {
            // получаем локацию для загрузки, на которой будем играть
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
            else // значит текущий режим последний. После него переходить не надо, ничего не подгружаем
            {
                currentGameStage = 0;

                //sceneLoader.PrepareLobbyScene(); // сделать загрузку через окно
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
        // получаем место игрока
        GetCurrentPlayerPlace();
        // переходим в лобби с пометкой, что надо показать окно статистики игрока
        isShowPlayerStatisticsWindowAfterGameInTheLobby = true;
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
        else // значит игрок остался один, у него первое место
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
        string aiName = nameGenerator.GetNextRandomName();

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
    }


    private SceneField GetSceneToLoad()
    {
        int gameModeCount = gameStagesList[currentGameStage].gameModesList.Count;
        int randomGameModeIndex = Random.Range(0, gameModeCount);

        currentGameMode = randomGameModeIndex;

        SceneField scene = gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameModeScenes.GetRandomScene();

        return scene;
    }

    private void OnDisable()
    {
        sceneLoader.lobbyOpenEvent -= ResetValuesAfterLeavingToLobby;
        sceneLoader.lobbyOpenEvent -= CheckTheDisplayOfThePlayerStatisticsWindowAfterGameInTheLobby;
    }
}

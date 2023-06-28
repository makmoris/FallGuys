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

    [SerializeField]private List<IPlayerData> players = new List<IPlayerData>();
    public List<IPlayerData> Players => players;

    private bool isFirstStart = true;
    private readonly string previousGameModeSceneNameKey = "PreviousGameModeSceneName";

    private NameGenerator nameGenerator;

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
        sceneLoader.lobbyOpenEvent += ResetValues;
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
        }
        else
        {
            int nextStageIndex = currentGameStage + 1;
            if (nextStageIndex < gameStagesList.Count)
            {
                currentGameStage = nextStageIndex;

                sceneToLoad = GetSceneToLoad();

                //sceneLoader.PrepareNextLevelScene(sceneToLoad);
                //sceneLoader.PrepareLobbyScene();
            }
            else // значит текущий режим последний. После него переходить не надо, ничего не подгружаем
            {
                currentGameStage = 0;

                //sceneLoader.PrepareLobbyScene();

                return;
            }
        }
        Debug.Log($"Количество игроков = {players.Count}");
        // запускаем окно скролла выбора карты. Передаем в MapSelector нужную сцену для визуального отображения sceneToLoad
        sceneLoader.LoadNextLevelSceneWithAnimation(sceneToLoad);
    }

    private void CreatePlayersForOneGameSession()
    {
        ClearPlayersForOneGameSession();

        Player player = CreatePlayer();
        players.Add(player);

        for (int i = 0; i < gameStagesList[currentGameStage].numberOfPlayers - 1; i++)
        {
            PlayerAI aiPlayer = CreateAIPlayer();
            players.Add(aiPlayer);
        }
    }

    private void ClearPlayersForOneGameSession()
    {
        players.Clear();
    }

    private Player CreatePlayer()
    {
        string playerName = "Player";

        GameObject playerPrefab = characterManager.GetPlayerPrefab();

        PlayerDefaultData playerDefaultData = characterManager.GetPlayerDefaultData();

        Weapon playerWeapon = characterManager.GetPlayerWeapon();
        
        Player player = new Player(playerName, playerPrefab, playerDefaultData, playerWeapon);

        return player;
    }

    private PlayerAI CreateAIPlayer()
    {
        string aiName = nameGenerator.GetNextRandomName();

        Material aiColorMaterial = characterManager.GetRandomMaterial();

        Weapon aiWeapon = characterManager.GetRandomWeapon();

        PlayerAI ai = new PlayerAI(aiName, aiColorMaterial, aiWeapon);

        return ai;
    }

    public void EliminateLosersFromList(List<string> losersNames)
    {
        Debug.Log("ELIMINATE");
        for (int i = 0; i < losersNames.Count; i++)
        {
            foreach (var ai in players)
            {
                string aiName = ai.Name;

                if (aiName == losersNames[i])
                {
                    players.Remove(ai);
                    break;
                }
            }
        }
    }

    public int GetNumberOfWinners()
    {
        return gameStagesList[currentGameStage].numberOfWinners;
    }

    private void ResetValues()
    {
        isFirstStart = true;
        currentGameStage = 0;
    }


    private SceneField GetSceneToLoad()
    {
        int gameModeCount = gameStagesList[currentGameStage].gameModesList.Count;
        int randomGameModeIndex = Random.Range(0, gameModeCount);

        currentGameMode = randomGameModeIndex;

        string previousSceneName = PlayerPrefs.GetString(previousGameModeSceneNameKey, "null");

        SceneField scene = gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameModeScenes.GetRandomScene(previousSceneName);

        PlayerPrefs.SetString(previousGameModeSceneNameKey, scene.SceneName);

        return scene;
    }

    private void OnDisable()
    {
        sceneLoader.lobbyOpenEvent -= ResetValues;
    }
}

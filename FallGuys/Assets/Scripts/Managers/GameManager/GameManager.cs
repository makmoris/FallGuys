using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Game Stages")]
    [SerializeField] private List<GameStage> gameStagesList;

    private int currentGameStage;
    private int currentGameMode;

    [Header("Player Character")]
    [SerializeField] private CharacterManager characterManager;
    private PlayerSettingsGM playerSettings = new PlayerSettingsGM();
    public PlayerSettingsGM PlayerSettings
    {
        get => playerSettings;
    }

    private List<PlayerSettingsGM> aiSettings = new List<PlayerSettingsGM>();
    public List<PlayerSettingsGM> AIPlayerSettings
    {
        get => aiSettings;
    }

    private bool isFirstStart = true;
    private readonly string previousGameModeSceneNameKey = "PreviousGameModeSceneName";

    private static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
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

    public void StartGameStage()// начальна€ точка. ¬ызываетс€ по кнопке "Play"
    {
        SceneField sceneToLoad;

        if (isFirstStart)
        {
            // получаем локацию дл€ загрузки, на которой будем играть
            sceneToLoad = GetSceneToLoad();
            // получаем игрока. ≈го данные по кастомизации и характеристикам.
            SetPlayerSettings();
            // получаем список данных ботов. ” каждого бота данные по кастомизации и харакетиристики. —охран€ем этот список и работаем с ним до конца сессии
            switch (gameStagesList[currentGameStage].gameModesList[currentGameMode].gameMode)
            {
                case GameModeEnum.Race:

                    for (int i = 0; i < gameStagesList[currentGameStage].numberOfPlayers - 1; i++)
                    {
                        PlayerSettingsGM raceAIPlayerSettings = SetAndGetRaceAISettings();
                        aiSettings.Add(raceAIPlayerSettings);
                    }

                    break;

                case GameModeEnum.Arena:

                    break;
            }
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
            else // значит текущий режим последний. ѕосле него переходить не надо, ничего не подгружаем
            {
                currentGameStage = 0;

                //sceneLoader.PrepareLobbyScene();

                return;
            }
        }

        // запускаем окно скролла выбора карты. ѕередаем в MapSelector нужную сцену дл€ визуального отображени€ sceneToLoad

        isFirstStart = false;

        sceneLoader.LoadNextLevelSceneWithAnimation(sceneToLoad);
    }


    public void PrepareNextGameStage()
    {
        //int nextStageIndex = currentGameStage + 1;
        //if (nextStageIndex < gameStagesList.Count)
        //{
        //    currentGameStage = nextStageIndex;

        //    SceneField sceneToLoad = GetSceneToLoad();

        //    sceneLoader.PrepareNextLevelScene(sceneToLoad);
        //    sceneLoader.PrepareLobbyScene();
        //}
        //else // значит текущий режим последний. ѕосле него переходить не надо, ничего не подгружаем
        //{
        //    currentGameStage = 0;

        //    sceneLoader.PrepareLobbyScene();

        //    return;
        //}
        sceneLoader.PrepareLobbyScene();
    }

    public int GetNumberOfWinners()
    {
        return gameStagesList[currentGameStage].numberOfWinners;
    }

    private void ResetValues()
    {
        isFirstStart = true;
        currentGameStage = 0;

        playerSettings = new();
        aiSettings = new();
    }

    private PlayerSettingsGM SetAndGetRaceAISettings()
    {
        PlayerSettingsGM raceAIPlayerSettings = new();
        raceAIPlayerSettings._prefab = characterManager.GetRandomRaceAIPrefab();
        raceAIPlayerSettings._defaultData = characterManager.GetPlayerDefaultData();
        raceAIPlayerSettings._limitsData = characterManager.GetPlayerLimitsData();

        raceAIPlayerSettings._weapon = characterManager.GetRandomWeapon();

        raceAIPlayerSettings._colorMaterial = characterManager.GetRandomColorMaterial();

        return raceAIPlayerSettings;
    }

    private void SetPlayerSettings()
    {
        playerSettings._prefab = characterManager.GetPlayerPrefab();
        playerSettings._defaultData = characterManager.GetPlayerDefaultData();
        playerSettings._limitsData = characterManager.GetPlayerLimitsData();
        playerSettings._weapon = characterManager.GetPlayerWeapon();
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

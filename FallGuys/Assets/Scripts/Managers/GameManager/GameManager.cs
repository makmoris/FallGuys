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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGameStage();
        }
    }

    public void StartGameStage()// ��������� �����. ���������� �� ������ "Play"
    {
        // �������� ������� ��� ��������, �� ������� ����� ������
        SceneField sceneToLoad = GetSceneToLoad();
        
        if (isFirstStart)
        {
            // �������� ������. ��� ������ �� ������������ � ���������������.
            SetPlayerSettings();
            // �������� ������ ������ �����. � ������� ���� ������ �� ������������ � ���������������. ��������� ���� ������ � �������� � ��� �� ����� ������
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

        // ��������� ���� ������� ������ �����. �������� � MapSelector ������ ����� ��� ����������� ����������� sceneToLoad

        isFirstStart = false;

        SceneManager.LoadScene(sceneToLoad);
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
}

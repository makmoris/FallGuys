using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMode
{
    [SerializeField] private string name;
    public GameModeScenes gameModeScenes;
}

[System.Serializable]
public class GameStage
{
    [SerializeField] private string name;
    public int numberOfPlayers;
    public int numberOfWinners;
    public List<GameMode> gameModesList;

}

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameStage> gameStagesList;

    [SerializeField]private int currentGameStage;

    private readonly string previousGameModeSceneNameKey = "PreviousGameModeSceneName";

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
        Debug.Log($"Scene for loading {sceneToLoad.SceneName}");
        // �������� ������. ��� ������ �� ������������ � ���������������

        // �������� ������ ������ �����. � ������� ���� ������ �� ������������ � ���������������. ��������� ���� ������ � �������� � ��� �� ����� ������

        // ��������� ���� ������� ������ �����. �������� � MapSelector ������ ����� ��� ����������� ����������� sceneToLoad
    }

    private SceneField GetSceneToLoad()
    {
        int gameModeCount = gameStagesList[currentGameStage].gameModesList.Count;
        int randomGameModeIndex = Random.Range(0, gameModeCount);

        string previousSceneName = PlayerPrefs.GetString(previousGameModeSceneNameKey, "null");

        SceneField scene = gameStagesList[currentGameStage].gameModesList[randomGameModeIndex].gameModeScenes.GetRandomScene(previousSceneName);

        PlayerPrefs.SetString(previousGameModeSceneNameKey, scene.SceneName);

        return scene;
    }
}

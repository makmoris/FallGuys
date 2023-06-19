using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameModeScenes", menuName = "Game Mode Scenes")]
public class GameModeScenes : ScriptableObject
{
    [SerializeField] private List<SceneField> sceneNamesList;

    public SceneField GetRandomScene(string previousSceneName)
    {
        int r = Random.Range(0, sceneNamesList.Count);
        SceneField randomScene = sceneNamesList[r];

        if (randomScene.SceneName == previousSceneName)
        {
            if (sceneNamesList.Count > 1)
            {
                if (r == 0) r++;
                else if (r == sceneNamesList.Count - 1) r--;
                else r++;

                randomScene = sceneNamesList[r];
            }
        }
        
        return randomScene;
    }
}

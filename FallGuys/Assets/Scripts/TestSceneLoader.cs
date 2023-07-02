using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneLoader : MonoBehaviour
{
    public int ind;
   

    public void NewScene()
    {
        SceneManager.LoadScene(ind);
    }

    public void TestLoad()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        gameManager.StartGameStage();
    }
}

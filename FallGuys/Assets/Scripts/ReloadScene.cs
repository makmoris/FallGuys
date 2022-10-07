using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
    

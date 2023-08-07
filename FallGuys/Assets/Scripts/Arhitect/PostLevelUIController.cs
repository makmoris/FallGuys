using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PostLevelUIController : MonoBehaviour
{
    protected GameManager _gameManager;
    public GameManager GameManager { set => _gameManager = value; }
}

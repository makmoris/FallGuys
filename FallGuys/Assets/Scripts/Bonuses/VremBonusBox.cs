using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VremBonusBox : MonoBehaviour
{
    public GameObject arenaBox;
    public GameObject ringsBox;

    private void Awake()
    {
        ShowBox();
    }

    public void ShowBox()
    {
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            arenaBox.SetActive(true);
            ringsBox.SetActive(false);
        }
        else
        {
            arenaBox.SetActive(false);
            ringsBox.SetActive(true);
        }
    }
}

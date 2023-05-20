using System.Collections;
using UnityEngine;

public class RaceMine : MonoBehaviour
{
    [SerializeField] private float reloadTime = 2f;

    public void ReloadMine(GameObject mine)
    {
        StartCoroutine(WaitAndReload(mine));
    }

    IEnumerator WaitAndReload(GameObject mine)
    {
        mine.SetActive(false);
        yield return new WaitForSeconds(reloadTime);
        mine.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHistory : MonoBehaviour
{
    private GameObject lastShooter;// последний стрелявший в игрока 
    [SerializeField] private float resetLastShooterTime = 2f;

    private Coroutine resetLastShooter = null;

    public void SetLastShooter(GameObject _shooter) // метод и корутина для того, чтобы отследить, выпал ли игрок с арены сам или его столкнули 
    {           // выстрелом. Если на момент попадания в DeadZone у упавшего есть lastShooter, то этот Shooter его и выпихнул своим выстрелом.
        lastShooter = _shooter;

        if (resetLastShooter != null) StopCoroutine(resetLastShooter);
        resetLastShooter = StartCoroutine(ResetLastShooter());
    }

    public GameObject GetLastShooter()// вызывает VisualIntermediary, чтобы проверить, кинуть ли игроку фраг
    {
        return lastShooter;
    }

    IEnumerator ResetLastShooter()
    {
        yield return new WaitForSeconds(resetLastShooterTime); // время сброса стрелка. Если упал после 1 секунды, значит это не стрелок его сбросил

        lastShooter = null;
    }
}

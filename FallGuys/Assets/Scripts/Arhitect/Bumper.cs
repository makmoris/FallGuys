using System;
using UnityEngine;

public class Bumper : MonoBehaviour /* ЧувакКоторогоНельзяНазывать */ // на обьекте нашего игрока будет висеть этот скрипт
{
    public event Action<Bonus> OnBonusGot;

    private void Start()
    {
        // для работы enabled = true/false
    }

    private void OnTriggerEnter(Collider other)
    {
        var bonus = other.GetComponent<Bonus>();
        if (bonus != null)
        {
            Bullet bullet = bonus.GetComponent<Bullet>();
            if (bullet != null)// если "бонус" оказался пулей
            {
                GameObject bulletParent = bullet.GetParent();// смотрим, кто эту пулю выпустил. Если "стрелок" совпадает с хозяином бампера
                if (bulletParent != this.gameObject)                 // значит это один и тот же игрок. Не бьем по самому себе.
                {
                    OnBonusGot?.Invoke(bonus);
                    bonus.Got();
                    Debug.Log($"Прилетела пуля в бампер");
                }
            }
            else
            {
                Explosion explosion = bonus.GetComponent<Explosion>();
                if (explosion == null)// взрыв вызывается через public GetBonus. Самим бампером ловим только баффы. + Взрыв не сразу после наезда
                {
                    if(other.GetComponent<DeadZone>() != null && enabled) // сделано, т.к. было по несколько вызовов
                    {
                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();

                        enabled = false; // enabled = true сделает DeadZone на респе
                    }

                    if (enabled)
                    {
                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();
                    }

                    Debug.Log($"{name} Event GetBonus {other.name}");
                }
            }
        }
    }

    public void GetBonus(Bonus bonus)// вызывается взрывом без коллайдера
    {
        Debug.Log("Public GetBonus");
        OnBonusGot?.Invoke(bonus);
        bonus.Got();
    }
}

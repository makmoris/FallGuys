using System;
using UnityEngine;

public class Bumper : MonoBehaviour /* ЧувакКоторогоНельзяНазывать */ // на обьекте нашего игрока будет висеть этот скрипт
{
    public event Action<Bonus> OnBonusGot;

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
                }
            }
            else
            {
                Debug.Log("Event GetBonus");
                OnBonusGot?.Invoke(bonus);
                bonus.Got();
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

using System;
using UnityEngine;

public class Bumper : MonoBehaviour /* „увак оторогоЌельз€Ќазывать */ // на обьекте нашего игрока будет висеть этот скрипт
{
    public event Action<Bonus> OnBonusGot;

    private void OnTriggerEnter(Collider other)
    {
        var bonus = other.GetComponent<Bonus>();
        if (bonus != null)
        {
            OnBonusGot?.Invoke(bonus);
            bonus.Got();
        }
    }
}

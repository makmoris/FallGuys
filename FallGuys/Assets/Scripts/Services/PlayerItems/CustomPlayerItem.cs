using UnityEngine;

namespace PunchCars.PlayerItems
{
    public abstract class CustomPlayerItem
    {
        public abstract string ID { get; protected set; }
        public abstract string Name { get; protected set; }
        public abstract Sprite Icon { get; protected set; }
        public abstract TierType TierType { get; protected set; }
        public abstract Sprite TierIcon { get; protected set; }
        public abstract bool IsAvailable { get; protected set; }

        // метод будет вызываться моделями, например CupRewardsModel или моделью для гаража, когда этот объект активируется (получает игрок)
        // та же модель будет обращаться в Памяти, чтобы установить новое значение по ID элемента
        public bool CheckIsAvailable()
        {
            return false;
        }
    }
}

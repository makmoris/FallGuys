using UnityEngine;

namespace PunchCars.PlayerItems
{
    public abstract class CustomPlayerItem
    {
        public abstract string ID { get; protected set; }
        public abstract string Name { get; protected set; }
        public abstract Sprite Icon { get; protected set; }
    }
}

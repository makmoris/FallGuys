using UnityEngine;

namespace PunchCars.PlayerItems
{
    public class CarPlayerItem : CustomPlayerItem
    {
        public override string ID { get; protected set ; }
        public override string Name { get; protected set; }
        public override Sprite Icon { get; protected set; }
        public override TierType TierType { get; protected set; }
        public override Sprite TierIcon { get; protected set; }
        public override bool IsAvailable { get; protected set; }
        public float HP { get; private set; }

        public CarPlayerItem(string id, string name, Sprite icon, TierType tierType, Sprite tierIcon, bool isAvailable, float hp)
        {
            ID = id;
            Name = name;
            Icon = icon;
            TierType = tierType;
            TierIcon = tierIcon;
            IsAvailable = isAvailable;
            HP = hp;
        }
    }
}

using UnityEngine;

namespace PunchCars.PlayerItems
{
    public class CarPlayerItem : CustomPlayerItem
    {
        public override string ID { get; protected set ; }
        public override string Name { get; protected set; }
        public override Sprite Icon { get; protected set; }
        public float HP { get; private set; }

        public CarPlayerItem(string id, string name, Sprite icon, float hp)
        {
            ID = id;
            Name = name;
            Icon = icon;
            HP = hp;
        }
    }
}

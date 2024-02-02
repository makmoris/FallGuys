using UnityEngine;

namespace PunchCars.PlayerItems
{
    public class WeaponPlayerItem : CustomPlayerItem
    {
        public override string ID { get; protected set; }
        public override string Name { get; protected set; }
        public override Sprite Icon { get; protected set; }
        public float Damage { get; private set; }
        public float RechargeTime { get; private set; }
        public float AttackRange { get; private set; }

        public WeaponPlayerItem(string id, string name, Sprite icon, float damage, float rechargeTime, float attackRange)
        {
            ID = id;
            Name = name;
            Icon = icon;
            Damage = damage;
            RechargeTime = rechargeTime;
            AttackRange = attackRange;
        }
    }
}

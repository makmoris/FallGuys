using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PunchCars.PlayerItems
{
    [CreateAssetMenu(fileName = "New Player Item", menuName = "PunchCars/PlayerItems/PlayerItem", order = 0)]
    public class PlayerItemSO : ScriptableObject, IPlayerItemProvider
    {
        [SerializeField] private PlayerItemType _playerItemType;
        [ShowIf("_playerItemType", PlayerItemType.Car)]
        [Min(0)][SerializeField] private float _hp;
        [ShowIfGroup("_playerItemType", PlayerItemType.Weapon)]
        [Min(0)][SerializeField] private float _damage, _rechargeTime, _attackRange;
        [Space]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField, PreviewField(75)] private Sprite _icon;

        public CustomPlayerItem GetPlayerItem()
        {
            switch (_playerItemType)
            {
                case PlayerItemType.Car:
                    return GetCarPlayerItem();

                case PlayerItemType.Weapon:
                    return GetWeaponPlayerItem();
            }

            throw new Exception();
        }

        private CustomPlayerItem GetCarPlayerItem()
        {
            return new CarPlayerItem(_id, _name, _icon, _hp);
        }

        private CustomPlayerItem GetWeaponPlayerItem()
        {
            return new WeaponPlayerItem(_id, _name, _icon, _damage, _rechargeTime, _attackRange);
        }
    }
}

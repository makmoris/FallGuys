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
        [SerializeField] private TierType _tierType;
        [SerializeField, PreviewField(75)] private Sprite _tierIcon;
        [Space]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private bool _isAvailable;
        [SerializeField, PreviewField(75)] private Sprite _icon;

        public CustomPlayerItem GetPlayerItem()
        {
            //if (!_isAvailable) _isAvailable = CheckIsAvailable();

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
            return new CarPlayerItem(_id, _name, _icon, _tierType, _tierIcon, _isAvailable, _hp);
        }

        private CustomPlayerItem GetWeaponPlayerItem()
        {
            return new WeaponPlayerItem(_id, _name, _icon, _tierType, _tierIcon, _isAvailable, _damage, _rechargeTime, _attackRange);
        }

        private bool CheckIsAvailable()
        {
            // здесь можно брать инфу из памяти, например по ID объекта, чтобы посмотреть был ли он открыт
            throw new Exception();
        }
    }
}

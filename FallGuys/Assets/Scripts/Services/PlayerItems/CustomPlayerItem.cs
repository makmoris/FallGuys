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

        // ����� ����� ���������� ��������, �������� CupRewardsModel ��� ������� ��� ������, ����� ���� ������ ������������ (�������� �����)
        // �� �� ������ ����� ���������� � ������, ����� ���������� ����� �������� �� ID ��������
        public bool CheckIsAvailable()
        {
            return false;
        }
    }
}

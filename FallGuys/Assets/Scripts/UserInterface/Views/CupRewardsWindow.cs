using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.UserInterface.Presenters;
using TMPro;

namespace PunchCars.UserInterface.Views
{
    public class CupRewardsWindow : BaseUiView<CupRewardsPresenter>
    {
        [SerializeField] private CupRewardItem _cupRewardItemPrefab;
        [SerializeField] private CupRewardProgressScale _cupRewardProgressScalePrefab;
        [Space]
        [SerializeField] private RectTransform _cupRewardsSectionParent;

        private readonly List<CupRewardItem> _cupRewardItems = new List<CupRewardItem>();
        private CupRewardProgressScale _cupRewardProgressScale;

        public void OnCupRewardWindowShow()
        {
            if (!gameObject.activeSelf)
            {
                Presenter.OnCupRewardsWindowShowed();
                gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            Presenter.UpdateCupRewardsProgressScale(_cupRewardProgressScale, _cupRewardItems);
        }

        public List<CupRewardItem> CreateCupRewardItems(int count)
        {
            for (var i = 0; i < count; i++)
                _cupRewardItems.Add(Instantiate(_cupRewardItemPrefab, _cupRewardsSectionParent));

            return _cupRewardItems;
        }

        public CupRewardProgressScale CreateCupRewardProgressScale()
        {
            _cupRewardProgressScale = Instantiate(_cupRewardProgressScalePrefab, _cupRewardsSectionParent);

            return _cupRewardProgressScale;
        }

        private void ClearCupRewardItems()
        {
            foreach (CupRewardItem cupRewardItem in _cupRewardItems)
                Destroy(cupRewardItem.gameObject);

            _cupRewardItems.Clear();
        }

        private void OnDisable()
        {
            ClearCupRewardItems();
            Destroy(_cupRewardProgressScale);
        }
    }
}

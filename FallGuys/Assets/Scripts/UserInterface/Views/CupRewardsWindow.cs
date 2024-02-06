using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.UserInterface.Presenters;

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

        public void OnCupRewardWindowShow()// кнопка в лобби
        {
            if (!gameObject.activeSelf)
            {
                Presenter.OnCupRewardsWindowShowed();

                gameObject.SetActive(true);
            }
        }

        public void OnCupRewardWindowShowAfterBattle()// кнопка "продолжить" на постбоевом экране
        {
            if (!gameObject.activeSelf)
            {
                Presenter.OnCupRewardsWindowShowedAfterBattle();

                gameObject.SetActive(true);
            }
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

        public CupRewardProgressScale GetCupRewardProgressScale() => _cupRewardProgressScale;

        private void ClearCupRewardItems()
        {
            foreach (CupRewardItem cupRewardItem in _cupRewardItems)
                Destroy(cupRewardItem.gameObject);

            _cupRewardItems.Clear();

            Destroy(_cupRewardProgressScale.gameObject);
        }

        private void OnDisable()
        {
            ClearCupRewardItems();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.UserInterface.Views;
using PunchCars.Models;
using Zenject;
using PunchCars.CupRewards;

namespace PunchCars.UserInterface.Presenters
{
    public class CupRewardsPresenter : BasePresenter<CupRewardsWindow>
    {
        private CupRewardsModel _cupRewardsModel;
        private CupRewardsWindow _cupRewardsView;

        private ICupRewardsProvider _cupRewardsProvider;

        [Inject]
        public CupRewardsPresenter(CupRewardsModel cupRewardsModel, IUiSystem uiSystem, ICupRewardsProvider cupRewardsProvider)
        {
            _cupRewardsModel = cupRewardsModel;
            _cupRewardsView = uiSystem.GetView<CupRewardsWindow>();
            _cupRewardsProvider = cupRewardsProvider;
        }
    }
}

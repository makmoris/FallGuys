using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PunchCars.UserInterface
{
    public class UiSystem : MonoBehaviour, IUiSystem
    {
        [Header("Views")]
        public Views.ShopWindow shopView;
        public Views.CupRewardsWindow cupRewardsView;
        public Views.AfterBattleStatisticsWindow afterBattleStatisticsView;

        public event Action<IUiView> OnViewShown;
        public event Action<IUiView> OnViewClosed;

        public List<IUiView> ActiveViews => throw new NotImplementedException();

        private readonly List<IUiView> _views = new();

        [Inject]
        public void Construct()
        {
            // Здесь на старте заполняем список всех View окон на сцене. Чтобы потом брать отсюда нужный view

            _views.Add(shopView);
            _views.Add(cupRewardsView);
            _views.Add(afterBattleStatisticsView);
        }

        public TView GetView<TView>() where TView : IUiView
        {
            for (var i = 0; i < _views.Count; i++)
            {
                if (_views[i] is TView)
                {
                    return (TView)_views[i];
                }
            }

            throw new Exception("Can`t find view of type " + typeof(TView));
        }

        public void Hide(IUiView uiView)
        {
            throw new NotImplementedException();
        }

        public void Show<TView>() where TView : IUiView
        {
            throw new NotImplementedException();
        }

        public void ShowOver<TView>() where TView : IUiView
        {
            throw new NotImplementedException();
        }
    }
}

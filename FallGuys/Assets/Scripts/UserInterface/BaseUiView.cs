using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PunchCars.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseUiView<TPresenter> : MonoBehaviour, IUiView where TPresenter : IViewPresenter
    {
        public bool IsVisible { get; private set; }

        protected TPresenter Presenter;
        protected IUiSystem UISystem;

        public event Action OnShow;
        public event Action OnHide;

        [Inject]
        public virtual void Construct(TPresenter presenter, IUiSystem uiSystem)
        {
            Presenter = presenter;
            UISystem = uiSystem;
        }

        public void Hide()
        {
            
        }

        public void Show()
        {
            
        }
    }
}

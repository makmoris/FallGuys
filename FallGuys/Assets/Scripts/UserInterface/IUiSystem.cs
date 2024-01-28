using System;
using System.Collections.Generic;

namespace PunchCars.UserInterface
{
    public interface IUiSystem
    {
        public event Action<IUiView> OnViewShown;
        public event Action<IUiView> OnViewClosed;

        public List<IUiView> ActiveViews { get; }

        public void ShowOver<TView>() where TView : IUiView;
        public void Show<TView>() where TView : IUiView;
        public void Hide(IUiView uiView);
        public TView GetView<TView>() where TView : IUiView;
    }
}

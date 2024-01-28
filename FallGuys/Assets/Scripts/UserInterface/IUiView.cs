using System;

namespace PunchCars.UserInterface
{
    public interface IUiView
    {
        public bool IsVisible { get; }

        public event Action OnShow;
        public event Action OnHide;

        public void Show();
        public void Hide();
    }
}

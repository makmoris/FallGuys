using Zenject;
using PunchCars.InAppPurchasing;
using UnityEngine;
using PunchCars.UserInterface.Presenters;
using PunchCars.UserInterface;
using PunchCars.Models;

namespace PunchCars.Installers
{
    public class LobbyMonoInstaller : MonoInstaller
    {
        [SerializeField] private UiSystem _uiSystem;

        public override void InstallBindings()
        {
            // все, кому нужен интерфейс IIAPService будет передаваться объект IAPService в качестве реализатора этого интерфейса
            //Container.Bind<IIAPService>().To<IAPService>().AsSingle().NonLazy();

            BindServices();

            BindModels();
            BindPresenters();
        }

        private void BindServices()
        {
            Container.Bind<IUiSystem>().FromInstance(_uiSystem).AsSingle();
            // нужен IInitializable чтобы инициализировать покупки
            Container.Bind(typeof(IIAPService), typeof(IInitializable)).To<IAPService>().AsSingle().NonLazy();
        }

        private void BindModels()
        {
            Container.Bind<ShopModel>().AsSingle().NonLazy();
            Container.Bind<CupRewardsModel>().AsSingle().NonLazy();
        }

        private void BindPresenters()
        {
            Container.Bind<ShopPresenter>().AsSingle();
            Container.Bind<CupRewardsPresenter>().AsSingle();
        }
    }
}

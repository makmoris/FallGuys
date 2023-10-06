using Zenject;
using PunchCars.ShopMVP;
using PunchCars.InAppPurchasing;
using UnityEngine;

namespace PunchCars.Installers
{
    public class LobbyMonoInstaller : MonoInstaller
    {
        [SerializeField] private ShopView shopView;

        public override void InstallBindings()
        {
            // все, кому нужен интерфейс IIAPService будет передаваться объект IAPService в качестве реализатора этого интерфейса
            //Container.Bind<IIAPService>().To<IAPService>().AsSingle().NonLazy();

            Container.Bind(typeof(IIAPService), typeof(IInitializable)).To<IAPService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ShopPresenter>().AsSingle().NonLazy();
            Container.Bind<IShopView>().To<ShopView>().FromInstance(shopView).AsSingle();
            Container.Bind<ShopModel>().AsSingle().NonLazy();
        }

        
    }
}

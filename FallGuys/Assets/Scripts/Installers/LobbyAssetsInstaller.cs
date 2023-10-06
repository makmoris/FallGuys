using PunchCars.InAppPurchasing;
using UnityEngine;
using Zenject;

namespace PunchCars.Installers
{
    [CreateAssetMenu(fileName = "AssetsInstaller", menuName = "PunchCars/Installers/AssetsInstaller", order = 0)]
    public class LobbyAssetsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ProductsContainer _productsContainer;

        public override void InstallBindings()
        {
            Container.Bind<IProductsProvider>().FromInstance(_productsContainer).AsSingle();
        }
    }
}

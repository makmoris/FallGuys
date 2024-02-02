using PunchCars.CupRewards;
using PunchCars.InAppPurchasing;
using PunchCars.Leagues;
using UnityEngine;
using Zenject;

namespace PunchCars.Installers
{
    [CreateAssetMenu(fileName = "AssetsInstaller", menuName = "PunchCars/Installers/AssetsInstaller", order = 0)]
    public class LobbyAssetsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ProductsContainer _productsContainer;
        [SerializeField] private LeaguesContainer _leaguesContainer;
        [SerializeField] private CupRewardsContainer _cupRewardsContainer;

        public override void InstallBindings()
        {
            Container.Bind<IProductsProvider>().FromInstance(_productsContainer).AsSingle();
            Container.Bind<ILeaguesProvider>().FromInstance(_leaguesContainer).AsSingle();
            Container.Bind<ICupRewardsProvider>().FromInstance(_cupRewardsContainer).AsSingle();
        }
    }
}

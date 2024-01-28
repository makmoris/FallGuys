using PunchCars.UserInterface.Presenters;
using Zenject;

namespace PunchCars.Installers
{
    public class UIInstaller : Installer<UIInstaller>
    {
        [Inject]
        public UIInstaller()
        {

        }

        public override void InstallBindings()
        {
            Container.Bind<ShopPresenter>().AsSingle();
        }
    }
}

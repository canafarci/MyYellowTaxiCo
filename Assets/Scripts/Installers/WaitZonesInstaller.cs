using TaxiGame.Items;
using TaxiGame.WaitZones;
using Zenject;

namespace TaxiGame.Installers
{
    public class WaitZonesInstaller : MonoInstaller<WaitZonesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PayMoneyProcessor>().AsSingle();
            Container.Bind<IWaitingEngine>().FromComponentInChildren().AsTransient();
            Container.Bind<ItemSpawner>().FromComponentInChildren().AsTransient();
        }
    }
}
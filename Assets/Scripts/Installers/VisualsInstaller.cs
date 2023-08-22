using TaxiGame.Items;
using Zenject;

namespace TaxiGame.Installers
{
    public class VisualsInstaller : MonoInstaller<VisualsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFeedbackVisual>()
                        .FromComponentInChildren()
                        .AsTransient();

            Container.Bind<StackPositionCalculator>()
                        .FromComponentInChildren()
                        .AsTransient();

        }
    }
}

using Cinemachine;
using TaxiGame.Items;
using UnityEngine;
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

            Container.Bind<ParticleSystem>()
                        .FromComponentInChildren()
                        .AsTransient();

            Container.Bind<CinemachineVirtualCamera>()
                        .FromComponentInChildren()
                        .AsTransient();

        }
    }
}

using Cinemachine;
using TaxiGame.Items;
using TaxiGame.Resource.Visuals;
using TaxiGame.Visuals;
using UnityEngine;
using Zenject;

namespace TaxiGame.Installers
{
    public class VisualsInstaller : MonoInstaller<VisualsInstaller>
    {
        [SerializeField] private GameObject _collectibleMoneyPrefab;
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

            Container.Bind<TweeningService>()
                        .AsSingle();


            Container.BindMemoryPool<CollectibleMoney, CollectibleMoney.Pool>()
                    .WithInitialSize(100)
                    .FromComponentInNewPrefab(_collectibleMoneyPrefab)
                    .UnderTransformGroup("CollectibleMoneys");

        }
    }
}

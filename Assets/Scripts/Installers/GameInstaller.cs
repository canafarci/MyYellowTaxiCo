using TaxiGame.GameState;
using TaxiGame.GameState.Unlocking;
using TaxiGame.Items;
using TaxiGame.Resource;
using UnityEngine;
using Zenject;

namespace TaxiGame.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private GameConfigSO _gameConfig;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProgressionState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceTracker>().AsSingle();
            Container.Bind<GameConfigSO>().FromInstance(_gameConfig).AsSingle();

            Container.Bind<IUnlockable>().FromComponentInChildren().AsTransient();
            Container.Bind<SequentialUnlockable>().FromComponentInChildren().AsTransient();


            Container.Bind<Rigidbody>().FromComponentInChildren().AsTransient();

            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.BindInterfacesAndSelfTo<InputReader>().AsSingle();

            Container.Bind<Mover>().FromComponentInChildren().AsSingle();

            Container.Bind<Inventory>().FromComponentInChildren().AsTransient();
            Container.Bind<ItemUtility>().AsSingle();
            Container.Bind<ItemRemover>().FromComponentInHierarchy().AsSingle();

            Container.Bind<Collider>()
                .FromComponentInChildren()
                .AsTransient();

            Container.Bind<WandererMoney>()
                .FromComponentInChildren()
                .AsTransient();

            Container.Bind<Animator>()
                .FromComponentInChildren(false)
                .AsTransient();

            Container.BindFactory<UnityEngine.Object, Transform, WandererMoney, WandererMoney.Factory>()
                    .FromFactory<PrefabFactory<Transform, WandererMoney>>();
        }
    }
}

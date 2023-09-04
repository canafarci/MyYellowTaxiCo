using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private References _references;
        [SerializeField] private Joystick _joystick;
        public override void InstallBindings()
        {
            Container.Bind<Rigidbody>().FromComponentInChildren().AsTransient();

            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.BindInterfacesAndSelfTo<InputReader>().AsSingle();

            Container.Bind<References>().FromInstance(_references).AsSingle();

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

            Container.BindFactory<UnityEngine.Object, Transform, WandererMoney, WandererMoney.Factory>()
                    .FromFactory<PrefabFactory<Transform, WandererMoney>>();
        }
    }
}

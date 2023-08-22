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
            Container.Bind<IInputReader>().To<InputReader>().AsSingle();
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<References>().FromInstance(_references).AsSingle();


            Container.Bind<Inventory>().FromComponentInChildren().AsTransient();
            Container.Bind<Mover>().FromComponentInHierarchy().AsSingle();

            Container.Bind<ItemUtility>().AsSingle();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.Installer
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

            Container.Bind<Animator>().FromComponentInChildren().AsTransient();
            Container.Bind<Inventory>().FromComponentInChildren().AsTransient();
        }
    }
}

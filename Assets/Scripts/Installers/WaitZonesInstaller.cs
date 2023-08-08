using System.Collections;
using System.Collections.Generic;
using Taxi.WaitZones;
using UnityEngine;
using Zenject;

namespace Taxi.Installers
{
    public class WaitZonesInstaller : MonoInstaller<WaitZonesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PayMoneyProcessor>().AsSingle();
            Container.Bind<IUnlockable>().FromComponentInChildren().AsTransient();
            Container.Bind<IWaitingEngine>().FromComponentInChildren().AsTransient();
            Container.Bind<ItemSpawner>().FromComponentInChildren().AsTransient();
        }
    }
}
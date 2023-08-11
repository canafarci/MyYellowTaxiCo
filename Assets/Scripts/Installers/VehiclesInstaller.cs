using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.Vehicle
{
    public class VehiclesInstaller : MonoInstaller<VehiclesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Object, CarConfig, CarMover, CarMover.Factory>()
                .FromFactory<PrefabFactory<CarConfig, CarMover>>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Taxi.Vehicle;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehiclesInstaller : MonoInstaller<VehiclesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<VehicleStateManager>()
                    .AsSingle();

            Container.Bind<Vehicle>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<VehicleSpot>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<CarView>()
                    .FromComponentInChildren()
                    .AsTransient();
            Container.Bind<CarFX>()
                    .FromComponentInChildren()
                    .AsTransient();



            Container.BindFactory<Object, CarConfig, Vehicle, Vehicle.Factory>()
                .FromFactory<PrefabFactory<CarConfig, Vehicle>>();


        }
    }
}

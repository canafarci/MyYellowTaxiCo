using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehiclesInstaller : MonoInstaller<VehiclesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Taxi>()
                    .FromComponentInChildren()
                    .AsTransient();
            Container.Bind<TaxiSpot>()
                    .FromComponentInChildren()
                    .AsTransient();



            Container.BindFactory<Object, CarConfig, Taxi, Taxi.Factory>()
                .FromFactory<PrefabFactory<CarConfig, Taxi>>();


        }
    }
}

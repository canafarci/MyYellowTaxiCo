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
            Container.BindFactory<Object, CarConfig, CarView, CarView.Factory>()
                .FromFactory<PrefabFactory<CarConfig, CarView>>();

        }
    }
}

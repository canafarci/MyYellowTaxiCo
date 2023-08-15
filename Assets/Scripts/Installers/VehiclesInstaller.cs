using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
        public class VehiclesInstaller : MonoInstaller<VehiclesInstaller>
        {
                public override void InstallBindings()
                {
                        Container.Bind<VehicleManager>()
                                .AsSingle();

                        Container.Bind<Vehicle>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<VehicleSpot>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<VehicleSpot>()
                                .WithId("MoneyStacker")
                                .FromComponentInParents()
                                .AsTransient();

                        Container.Bind<VehicleController>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<VehicleModel>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<CarFX>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<VehicleAnimator>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<VehicleTweener>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<LevelProgress>()
                                .FromComponentInHierarchy()
                                .AsSingle();

                        Container.Bind<MoneyStacker>()
                                .FromComponentInChildren()
                                .AsSingle();

                        Container.BindFactory<Object, VehicleData, Vehicle, Vehicle.Factory>()
                                .FromFactory<PrefabFactory<VehicleData, Vehicle>>();
                }
        }
}

using System;
using System.Collections.Generic;
using Zenject;
using TaxiGame.Vehicles.Repair;
using TaxiGame.Vehicles;

namespace TaxiGame.Installers
{
        public class VehiclesInstaller : MonoInstaller<VehiclesInstaller>
        {
                public override void InstallBindings()
                {
                        InstallVehicleModel();
                        InstallCarSpawnFactories();

                        InstallBaseVehicle();
                        InstallLowGasVehicle();

                        Container.BindFactory<UnityEngine.Object, VehicleConfiguration, List<Action>, Vehicle, Vehicle.Factory>()
                        .FromFactory<PrefabFactory<VehicleConfiguration, List<Action>, Vehicle>>();
                }

                private void InstallCarSpawnFactories()
                {
                        Container.Bind<RegularCarFactory>()
                        .FromComponentInHierarchy()
                        .AsSingle();

                        Container.Bind<BrokenCarFactory>()
                        .FromComponentInHierarchy()
                        .AsSingle();

                        Container.Bind<SpecialProgressionEventCarFactory>()
                        .FromComponentInHierarchy()
                        .AsSingle();
                }

                private void InstallVehicleModel()
                {
                        Container.Bind<LevelProgress>()
                        .FromComponentInHierarchy()
                        .AsSingle();

                        Container.Bind<VehicleManager>()
                        .AsSingle();

                        Container.Bind<CarSpawnDataProvider>()
                        .AsSingle();

                        Container.Bind<VehicleProgressionModel>()
                        .AsSingle();

                        Container.Bind<GameProgressModel>()
                        .FromComponentInHierarchy().
                        AsSingle();

                        Container.Bind<MoneyStacker>()
                        .FromComponentInChildren()
                        .AsSingle();
                }


                private void InstallBaseVehicle()
                {
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
                }

                private void InstallLowGasVehicle()
                {
                        Container.Bind<GasStation>()
                        .FromComponentInChildren()
                        .AsSingle();

                        Container.Bind<LowGasBrokenCar>()
                        .FromComponentInChildren()
                        .AsSingle();

                        Container.Bind<IHandleHolder>().
                        FromComponentInParents().
                        AsTransient();

                        Container.Bind<Handle>()
                        .FromComponentInChildren()
                        .AsTransient();
                }

        }
}

using System;
using System.Collections.Generic;
using Zenject;
using TaxiGame.Vehicles.Repair;
using TaxiGame.Vehicles;
using TaxiGame.Vehicles.Creation;
using TaxiGame.Vehicles.Visuals;
using TaxiGame.Resource;
using UnityEngine;

namespace TaxiGame.Installers
{
    public class VehiclesInstaller : MonoInstaller<VehiclesInstaller>
    {
        [SerializeField] private RegularCarsSO _regularCarsSO;
        [SerializeField] private BrokenCarsSO _brokenCarsSO;
        [SerializeField] private ProgressionCarsSO _progressionCarsSO;
        public override void InstallBindings()
        {
            InstallVehicleModel();
            InstallCarSpawnFactories();

            InstallBaseVehicle();
            InstallLowGasVehicle();

            Container.Bind<BrokenEngineRepairableVehicle>().
            FromComponentInChildren().
            AsTransient();

            Container.Bind<FlatTireRepairableVehicle>().
            FromComponentInChildren().
            AsTransient();

            Container.BindFactory<UnityEngine.Object, VehicleConfiguration, List<Action>, Vehicle, Vehicle.Factory>()
            .FromFactory<PrefabFactory<VehicleConfiguration, List<Action>, Vehicle>>();
        }

        private void InstallCarSpawnFactories()
        {
            Container.Bind<RegularCarsSO>()
            .FromInstance(_regularCarsSO)
            .AsSingle();

            Container.Bind<BrokenCarsSO>()
            .FromInstance(_brokenCarsSO)
            .AsSingle();

            Container.Bind<ProgressionCarsSO>()
            .FromInstance(_progressionCarsSO)
            .AsSingle();

            Container.Bind<ICarFactory>()
            .WithId(VehicleFactoryID.RegularCarFactory)
            .To<RegularCarFactory>()
            .AsSingle();

            Container.Bind<ICarFactory>()
            .WithId(VehicleFactoryID.BrokenCarFactory)
            .To<BrokenCarFactory>()
            .AsSingle();

            Container.Bind<ICarFactory>()
            .WithId(VehicleFactoryID.ProgressionCarFactory)
            .To<SpecialProgressionEventCarFactory>()
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

            Container.Bind<MoneyStacker>() //TODO move to ResourceInstaller
            .FromComponentInChildren()
            .AsTransient();

            Container.Bind<HeliSpot>()
            .FromComponentInChildren()
            .AsTransient();
        }


        private void InstallBaseVehicle()
        {
            Container.Bind<Vehicle>()
            .FromComponentInChildren()
            .AsTransient();

            Container.Bind<VehicleSpot>()
            .FromComponentInChildren()
            .AsTransient();

            Container.Bind<IVehicleEvents>()
            .FromComponentInParents()
            .AsTransient();

            Container.Bind<VehicleController>()
            .FromComponentInChildren()
            .AsTransient();

            Container.Bind<VehicleModel>()
            .FromComponentInChildren()
            .AsTransient();

            Container.Bind<VehicleMaterialChange>()
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
            .AsTransient();

            Container.Bind<LowGasBrokenCar>()
            .FromComponentInChildren()
            .AsTransient();

            Container.Bind<IHandleHolder>().
            FromComponentInParents().
            AsTransient();

            Container.Bind<Handle>()
            .FromComponentInChildren()
            .AsTransient();
        }
    }

    public enum VehicleFactoryID
    {
        BrokenCarFactory,
        RegularCarFactory,
        ProgressionCarFactory
    }
}

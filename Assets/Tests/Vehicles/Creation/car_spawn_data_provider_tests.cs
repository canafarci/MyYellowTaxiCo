using NUnit.Framework;
using Zenject;
using Moq;
using UnityEngine;
using TaxiGame.Vehicles.Creation;
using TaxiGame.Installers;
using TaxiGame.GameState;

namespace TaxiGame.Vehicles.Tests
{
    public class car_spawn_data_provider_tests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            // Arrange
            Container.Bind<ISpawnDataFactory>()
            .WithId(VehicleFactoryID.RegularCarFactory)
            .To<RegularCarSpawnDataFactory>()
            .AsSingle();

            Container.Bind<ISpawnDataFactory>()
            .WithId(VehicleFactoryID.BrokenCarFactory)
            .To<BrokenCarSpawnDataFactory>()
            .AsSingle();

            Container.Bind<ISpawnDataFactory>()
            .WithId(VehicleFactoryID.ProgressionCarFactory)
            .To<ProgressionCarSpawnDataFactory>()
            .AsSingle();

            Container.Bind<RegularCarsSO>()
            .FromScriptableObjectResource("ScriptableObjects/RegularCarsSO")
            .AsSingle();

            Container.Bind<BrokenCarsSO>()
            .FromScriptableObjectResource("ScriptableObjects/BrokenCarsSO")
            .AsSingle();

            Container.Bind<ProgressionCarsSO>()
            .FromScriptableObjectResource("ScriptableObjects/ProgressionCarSO")
            .AsSingle();

            Container.Bind<GameProgressModel>().FromInstance(Mock.Of<GameProgressModel>());


            Container.Bind<CarSpawnDataProvider>()
            .AsSingle();

            Container.BindInterfacesAndSelfTo<ProgressionState>().AsSingle();
            Container.Resolve<ProgressionState>().Initialize();

            var gameProgressModel = Container.Resolve<GameProgressModel>();
            VehicleProgressionModel model = new Mock<VehicleProgressionModel>(gameProgressModel).Object;
            model.Initialize();


            Container.Bind<VehicleProgressionModel>().FromInstance(model);
        }

        [Test]
        public void get_initial_car_data_returns_correct_cars()
        {
            // Arrange
            var carSpawnDataProvider = Container.Resolve<CarSpawnDataProvider>();
            var regularCarSO = Container.Resolve<RegularCarsSO>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID taxiID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID suberID = CarSpawnerID.SuberSpawner;
            CarSpawnerID limoID = CarSpawnerID.LimoSpawner;

            // Act: Call GetInitialCarSpawnData with the test CarSpawnerID
            var taxiData = carSpawnDataProvider.GetInitialCarSpawnData(taxiID);
            var suberData = carSpawnDataProvider.GetInitialCarSpawnData(suberID);
            var limoData = carSpawnDataProvider.GetInitialCarSpawnData(limoID);

            // Assert: Ensure that the spawned car data is not null
            Assert.IsTrue(taxiData.Prefab == regularCarSO.RegularTaxi);
            Assert.IsTrue(suberData.Prefab == regularCarSO.RegularSuber);
            Assert.IsTrue(limoData.Prefab == regularCarSO.RegularLimo);
        }

        [Test]
        public void provider_returns_progression_cars_on_first_call()
        {
            // Arrange
            var carSpawnDataProvider = Container.Resolve<CarSpawnDataProvider>();
            var progressModel = Container.Resolve<GameProgressModel>();
            //Delete saved keys
            DeleteKeys();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID firstID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID secondID = CarSpawnerID.SecondYellowSpawner;
            CarSpawnerID thirdID = CarSpawnerID.ThirdYellowSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var firstData = carSpawnDataProvider.GetCarSpawnData(firstID);
            var secondData = carSpawnDataProvider.GetCarSpawnData(secondID);
            var thirdData = carSpawnDataProvider.GetCarSpawnData(thirdID);

            // Assert: Ensure that the spawned car data prefab is correct
            Assert.IsTrue(firstData.VehicleInPlaceCallbacks.Contains(progressModel.FirstReturnCarWithoutCharger));
            Assert.IsTrue(secondData.VehicleInPlaceCallbacks.Contains(progressModel.SecondReturnBroken));
            Assert.IsTrue(thirdData.VehicleInPlaceCallbacks.Contains(progressModel.ThirdReturnBroken));
        }

        //Helper function
        private void DeleteKeys()
        {
            if (PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
            {
                PlayerPrefs.DeleteKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE);
            }
            if (PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
            {
                PlayerPrefs.DeleteKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE);
            }
            if (PlayerPrefs.HasKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE))
            {
                PlayerPrefs.DeleteKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE);
            }
        }

    }
}

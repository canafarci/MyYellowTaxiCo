using Zenject;
using NUnit.Framework;
using TaxiGame.Vehicles.Creation;
using UnityEngine;
using System;
using System.Linq;
using TaxiGame.GameState;

namespace TaxiGame.Vehicles.Tests
{
    [TestFixture]
    public class broken_vehicle_factory_tests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            // Arrange
            Container.Bind<BrokenCarSpawnDataFactory>().AsSingle();
            Container.Bind<GameProgressModel>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressionState>().AsSingle();
            Container.Bind<BrokenCarsSO>()
                .FromScriptableObjectResource("ScriptableObjects/BrokenCarsSO")
                .AsSingle();

            Container.Resolve<ProgressionState>().Initialize();
        }

        [Test]
        public void constructor_test()
        {
            // Act: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();

            // Assert: Ensure that the _brokenVehiclesSO field is properly initialized
            Assert.NotNull(factory);
        }

        [Test]
        public void creates_car_spawn_data_when_called()
        {
            // Act: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID yellowCarSpawnerID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID suberCarSpawnerID = CarSpawnerID.SuberSpawner;
            CarSpawnerID limoCarSpawnerID = CarSpawnerID.LimoSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var yellowSpawnData = factory.CreateCarSpawnData(yellowCarSpawnerID);
            var suberSpawnData = factory.CreateCarSpawnData(suberCarSpawnerID);
            var limoSpawnData = factory.CreateCarSpawnData(limoCarSpawnerID);

            // Assert: Ensure that the spawned car data is not null
            Assert.NotNull(yellowSpawnData);
            Assert.NotNull(suberSpawnData);
            Assert.NotNull(limoSpawnData);
        }

        [Test]
        public void creates_correct_taxi_for_first_progression_state()
        {
            // Arrange: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();
            var brokenCarSO = Container.Resolve<BrokenCarsSO>();

            // Arrange: Set up PlayerPrefs keys for testing
            if (PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
            {
                PlayerPrefs.DeleteKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE);
            }
            if (PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
            {
                PlayerPrefs.DeleteKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE);
            }


            PlayerPrefs.SetInt(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE, 1);

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            CarSpawnerID yellowCarSpawnerID = CarSpawnerID.FirstYellowSpawner;
            var yellowSpawnData = factory.CreateCarSpawnData(yellowCarSpawnerID);

            // Assert: Ensure that returned car correct
            GameObject createdTaxi = yellowSpawnData.Prefab;
            UnityEngine.Debug.Log(createdTaxi);
            GameObject noGasTaxi = brokenCarSO.BrokenTaxis[0];
            Assert.IsTrue(createdTaxi == noGasTaxi);
        }
        [Test]
        public void creates_correct_taxi_for_second_progression_state()
        {
            // Arrange: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();
            var brokenCarSO = Container.Resolve<BrokenCarsSO>();

            // Arrange: Set up PlayerPrefs keys for testing
            PlayerPrefs.DeleteKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE);
            PlayerPrefs.SetInt(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE, 1);

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            CarSpawnerID yellowCarSpawnerID = CarSpawnerID.FirstYellowSpawner;
            var yellowSpawnData = factory.CreateCarSpawnData(yellowCarSpawnerID);

            // Assert: Ensure that returned car correct
            GameObject createdTaxi = yellowSpawnData.Prefab;
            GameObject brokenTireTaxi = brokenCarSO.BrokenTaxis[2];
            Assert.IsFalse(createdTaxi == brokenTireTaxi);
        }
        [Test]
        public void creates_correct_taxi_for_third_progression_state()
        {
            // Arrange: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();
            var brokenCarSO = Container.Resolve<BrokenCarsSO>();

            // Arrange: Set up PlayerPrefs keys for testing
            PlayerPrefs.SetInt(Globals.THIRD_TIRE_TUTORIAL_COMPLETE, 1);

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            CarSpawnerID yellowCarSpawnerID = CarSpawnerID.FirstYellowSpawner;
            var yellowSpawnData = factory.CreateCarSpawnData(yellowCarSpawnerID);

            // Assert: Ensure that returned car correct
            GameObject createdTaxi = yellowSpawnData.Prefab;
            Assert.IsTrue(brokenCarSO.BrokenTaxis.Contains(createdTaxi));
        }
        [Test]
        public void creates_correct_subers()
        {
            // Arrange: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();
            var brokenCarSO = Container.Resolve<BrokenCarsSO>();

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            CarSpawnerID suberSpawnerID = CarSpawnerID.SuberSpawner;
            var suberSpawnData = factory.CreateCarSpawnData(suberSpawnerID);

            // Assert: Ensure that returned car correct
            GameObject createdCar = suberSpawnData.Prefab;
            Assert.IsTrue(brokenCarSO.BrokenSubers.Contains(createdCar));
        }
        [Test]
        public void creates_correct_limos()
        {
            // Arrange: Create an instance of BrokenCarSpawnDataFactory
            var factory = Container.Resolve<BrokenCarSpawnDataFactory>();
            var brokenCarSO = Container.Resolve<BrokenCarsSO>();

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            CarSpawnerID limoSpawnerID = CarSpawnerID.LimoSpawner;
            var limoSpawnData = factory.CreateCarSpawnData(limoSpawnerID);

            // Assert: Ensure that returned car correct
            GameObject createdCar = limoSpawnData.Prefab;
            Assert.IsTrue(brokenCarSO.BrokenLimos.Contains(createdCar));
        }
    }
}

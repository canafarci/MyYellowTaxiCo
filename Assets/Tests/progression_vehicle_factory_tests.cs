using NUnit.Framework;
using TaxiGame.Vehicles.Creation;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Tests
{
    [TestFixture]
    public class progression_vehicle_factory_tests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            // Arrange
            Container.Bind<ProgressionCarSpawnDataFactory>().AsSingle();
            Container.Bind<GameProgressModel>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<ProgressionCarsSO>()
                .FromScriptableObjectResource("ScriptableObjects/ProgressionCarSO")
                .AsSingle();
        }

        [Test]
        public void constructor_test()
        {
            // Act: Create an instance of ProgressionCarSpawnDataFactory
            var factory = Container.Resolve<ProgressionCarSpawnDataFactory>();

            Assert.NotNull(factory);
        }


        [Test]
        public void creates_car_spawn_data_when_called()
        {
            // Act: Create an instance of ProgressionCarSpawnDataFactory
            var factory = Container.Resolve<ProgressionCarSpawnDataFactory>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID firstID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID secondID = CarSpawnerID.SecondYellowSpawner;
            CarSpawnerID thirdID = CarSpawnerID.ThirdYellowSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var firstData = factory.CreateCarSpawnData(firstID);
            var secondData = factory.CreateCarSpawnData(secondID);
            var thirdData = factory.CreateCarSpawnData(thirdID);

            // Assert: Ensure that the spawned car data is not null
            Assert.NotNull(firstData);
            Assert.NotNull(secondData);
            Assert.NotNull(thirdData);
        }

        [Test]
        public void creates_correct_car_for_spawner_ids()
        {
            // Act: Create an instance of ProgressionCarSpawnDataFactory
            var factory = Container.Resolve<ProgressionCarSpawnDataFactory>();
            var progressModel = Container.Resolve<GameProgressModel>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID firstID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID secondID = CarSpawnerID.SecondYellowSpawner;
            CarSpawnerID thirdID = CarSpawnerID.ThirdYellowSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var firstData = factory.CreateCarSpawnData(firstID);
            var secondData = factory.CreateCarSpawnData(secondID);
            var thirdData = factory.CreateCarSpawnData(thirdID);

            // Assert: Ensure that the spawned car data prefab is correct
            Assert.IsTrue(firstData.VehicleInPlaceCallbacks.Contains(progressModel.FirstReturnCarWithoutCharger));
            Assert.IsTrue(secondData.VehicleInPlaceCallbacks.Contains(progressModel.SecondReturnBroken));
            Assert.IsTrue(thirdData.VehicleInPlaceCallbacks.Contains(progressModel.ThirdReturnBroken));
        }
        [Test]
        public void creates_correct_event_for_spawner_ids()
        {
            // Act: Create an instance of ProgressionCarSpawnDataFactory
            var factory = Container.Resolve<ProgressionCarSpawnDataFactory>();
            var progressionCarSO = Container.Resolve<ProgressionCarsSO>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID firstID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID secondID = CarSpawnerID.SecondYellowSpawner;
            CarSpawnerID thirdID = CarSpawnerID.ThirdYellowSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var firstData = factory.CreateCarSpawnData(firstID);
            var secondData = factory.CreateCarSpawnData(secondID);
            var thirdData = factory.CreateCarSpawnData(thirdID);

            // Assert: Ensure that the spawned car data prefab is correct
            Assert.IsTrue(firstData.Prefab == progressionCarSO.NoChargeTaxi);
            Assert.IsTrue(secondData.Prefab == progressionCarSO.BrokenEngineTaxi);
            Assert.IsTrue(thirdData.Prefab == progressionCarSO.FlatTireTaxi);
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

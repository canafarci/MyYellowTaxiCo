using NUnit.Framework;
using TaxiGame.Vehicles.Creation;
using Zenject;

namespace TaxiGame.Vehicles.Tests
{
    [TestFixture]
    public class regular_vehicle_factory_tests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            // Arrange
            Container.Bind<RegularCarSpawnDataFactory>().AsSingle();
            Container.Bind<RegularCarsSO>()
                .FromScriptableObjectResource("ScriptableObjects/RegularCarsSO")
                .AsSingle();
        }

        [Test]
        public void constructor_test()
        {
            // Act: Create an instance of RegularCarSpawnDataFactory
            var factory = Container.Resolve<RegularCarSpawnDataFactory>();

            Assert.NotNull(factory);
        }

        [Test]
        public void creates_car_spawn_data_when_called()
        {
            // Act: Create an instance of RegularCarSpawnDataFactory
            var factory = Container.Resolve<RegularCarSpawnDataFactory>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID taxiID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID suberID = CarSpawnerID.SuberSpawner;
            CarSpawnerID limoID = CarSpawnerID.LimoSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var taxiData = factory.CreateCarSpawnData(taxiID);
            var suberData = factory.CreateCarSpawnData(suberID);
            var limoData = factory.CreateCarSpawnData(limoID);

            // Assert: Ensure that the spawned car data is not null
            Assert.NotNull(taxiData);
            Assert.NotNull(suberData);
            Assert.NotNull(limoData);
        }
        [Test]
        public void creates_correct_car_spawn_data_when_called()
        {
            // Act: Create an instance of RegularCarSpawnDataFactory
            var factory = Container.Resolve<RegularCarSpawnDataFactory>();
            var regularCarSO = Container.Resolve<RegularCarsSO>();

            // Arrange: Define CarSpawnerID for testing
            CarSpawnerID taxiID = CarSpawnerID.FirstYellowSpawner;
            CarSpawnerID suberID = CarSpawnerID.SuberSpawner;
            CarSpawnerID limoID = CarSpawnerID.LimoSpawner;

            // Act: Call CreateCarSpawnData with the test CarSpawnerID
            var taxiData = factory.CreateCarSpawnData(taxiID);
            var suberData = factory.CreateCarSpawnData(suberID);
            var limoData = factory.CreateCarSpawnData(limoID);

            // Assert: Ensure that the spawned car data is not null
            Assert.IsTrue(taxiData.Prefab == regularCarSO.RegularTaxi);
            Assert.IsTrue(suberData.Prefab == regularCarSO.RegularSuber);
            Assert.IsTrue(limoData.Prefab == regularCarSO.RegularLimo);
        }
    }
}

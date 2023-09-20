using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TaxiGame.Vehicles.Creation;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Tests
{
    public class car_spawner_tests : ZenjectUnitTestFixture
    {
        private CarSpawner _carSpawner;
        private Mock<Vehicle.Factory> _mockVehicleFactory;
        private Mock<VehicleSpot> _mockVehicleSpot;
        private Mock<CarSpawnDataProvider> _mockDataProvider;
        private SpawnedCarData _expectedSpawnedCarData;

        [SetUp]
        public void CommonInstall()
        {
            // Create mock instances for dependencies
            _mockVehicleFactory = new Mock<Vehicle.Factory>();
            _mockVehicleSpot = new Mock<VehicleSpot>();

            // Set up behavior for the _mockDataProvider to return the expected SpawnedCarData
            var mockFactory = new Mock<ISpawnDataFactory>();
            // Define expected SpawnedCarData here based on your test case
            _expectedSpawnedCarData = new SpawnedCarData(new GameObject("TEST"));

            mockFactory.Setup(provider => provider.CreateCarSpawnData(CarSpawnerID.FirstYellowSpawner))
                            .Returns(_expectedSpawnedCarData);

            Container.Bind<GameProgressModel>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesAndSelfTo<VehicleProgressionModel>().AsSingle();
            VehicleProgressionModel model = Container.Resolve<VehicleProgressionModel>();
            model.Initialize();


            _mockDataProvider = new Mock<CarSpawnDataProvider>(mockFactory.Object,
                                                               mockFactory.Object,
                                                               mockFactory.Object,
                                                               model);

            // Bind mock dependencies to the Zenject container
            Container.BindInstance(_mockVehicleFactory.Object);
            Container.BindInstance(_mockVehicleSpot.Object);
            Container.BindInstance(_mockDataProvider.Object);

            // Create the CarSpawner instance
            Container.Bind<CarSpawner>().FromNewComponentOnNewGameObject().AsSingle();
            _carSpawner = Container.Resolve<CarSpawner>();
        }

        [Test]
        public void car_spawner_initializes_correctly()
        {
            // Assert that the spawner is correctly created
            Assert.IsNotNull(_carSpawner);
        }

        [Test]
        public void spawn_car_creates_vehicles_correctly()
        {
            // Act
            _carSpawner.SpawnCar();

            // Assert
            // Verify that the _mockVehicleFactory.Create method was called with the expected parameters
            _mockVehicleFactory.Verify(factory => factory.Create(
                _expectedSpawnedCarData.Prefab,
                It.IsAny<VehicleConfiguration>(),
                _expectedSpawnedCarData.VehicleInPlaceCallbacks),
                Times.Once);
        }
    }
}

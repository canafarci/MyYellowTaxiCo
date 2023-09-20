using System.Collections;
using Moq;
using NUnit.Framework;
using TaxiGame.Items;
using TaxiGame.NPC;
using TaxiGame.NPC.Command;
using TaxiGame.Vehicles.Visuals;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace TaxiGame.Vehicles.Tests
{
    public class customer_drop_area_tests : ZenjectIntegrationTestFixture
    {
        private Mock<VehicleProgressionModel> _mockVehicleProgressionModel;
        private GameObject _dropAreaObject;
        private GameObject _playerObject;
        private Vehicle _vehicle;
        private Customer _customer;

        public void CommonInstall()
        {
            // Setup initial state by creating game objects, loading prefabs/scenes, etc
            _dropAreaObject = new GameObject("DropArea");
            _playerObject = new GameObject("Player");

            PreInstall();

            // Call Container.Bind methods on Area

            Container.Bind<CustomerDropArea>().FromNewComponentOn(_dropAreaObject).AsSingle();

            Container.Bind<VehicleSpot>().FromNewComponentOn(_dropAreaObject).AsSingle();

            Container.Bind<VehicleManager>().AsSingle();

            Container.Bind<GameProgressModel>().FromNewComponentOnNewGameObject().AsSingle();

            _mockVehicleProgressionModel = new Mock<VehicleProgressionModel>(Container.Resolve<GameProgressModel>());

            Container.BindInterfacesAndSelfTo<VehicleProgressionModel>().FromInstance(_mockVehicleProgressionModel.Object);

            var collider = _dropAreaObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            collider.isTrigger = true;

            // Call Container.Bind methods on Player
            Container.BindInterfacesAndSelfTo<ItemUtility>().AsSingle();
            Container.Bind<Inventory>().FromNewComponentOn(_playerObject).AsSingle();
            _playerObject.tag = Globals.PLAYER_TAG;
            _playerObject.AddComponent(typeof(BoxCollider));
            var rb = _playerObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.useGravity = false;
            _playerObject.transform.position = Vector3.one * 100f;
            _vehicle = new GameObject("vehicle", typeof(Vehicle)).GetComponent<Vehicle>();
            _customer = new GameObject("customer", typeof(Customer)).GetComponent<Customer>();

            Container.Bind<Customer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Vehicle>().FromComponentInHierarchy().AsSingle();
            PostInstall();

        }

        [UnityTest]
        public IEnumerator drops_customer_when_enters_drop_area()
        {


            yield return new WaitForSeconds(2f);

            CommonInstall();

            var inventory = Container.Resolve<Inventory>();
            var dropArea = Container.Resolve<CustomerDropArea>();
            var vehicleSpot = Container.Resolve<VehicleSpot>();

            yield return new WaitForSeconds(2f);

            vehicleSpot.SetVehicle(_vehicle);

            inventory.AddObjectToInventory(_customer);
            // Ensure that the customer's inventory is full before  being dropped
            Assert.IsTrue(inventory.HasInventoryObjectType(InventoryObjectType.Customer));
            //move objects
            _playerObject.transform.position = Vector3.zero;
            yield return new WaitForSeconds(4f);

            // Ensure that the customer's inventory is empty after being dropped
            Assert.IsFalse(inventory.HasInventoryObjectType(InventoryObjectType.Customer));

            yield break;
        }
    }
}

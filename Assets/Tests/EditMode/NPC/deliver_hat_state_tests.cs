using Moq;
using NUnit.Framework;
using TaxiGame.Items;
using TaxiGame.NPC;
using TaxiGame.NPC.Command;
using Zenject;

namespace TaxiGame.NPC.Tests
{
    public class deliver_hat_state_tests : ZenjectUnitTestFixture
    {
        private DeliverHatState deliverHatState;
        private Mock<HelperNPCLocationReferences> mockLocationReferences;
        private Mock<Inventory> mockInventory;
        private Mock<NavMeshMover> mockMover;

        [SetUp]
        public void SetUp()
        {
            //Arrange

            // Create mock dependencies
            mockLocationReferences = new Mock<HelperNPCLocationReferences>();
            mockInventory = new Mock<Inventory>();
            mockMover = new Mock<NavMeshMover>();

            // Create the DeliverHatState instance and inject mock dependencies
            Container.Bind<DeliverHatState>().FromNewComponentOnNewGameObject().AsSingle();

            Container.Bind<HelperNPCLocationReferences>().FromInstance(mockLocationReferences.Object).AsSingle();
            Container.Bind<Inventory>().FromInstance(mockInventory.Object).AsSingle();
            Container.Bind<NavMeshMover>().FromInstance(mockMover.Object).AsSingle();
        }

        [Test]
        public void init_correctly_injects_dependencies()
        {
            // Verify that dependencies were injected correctly
            var deliverState = Container.Resolve<DeliverHatState>();
            Assert.IsNotNull(deliverState);
        }

    }
}

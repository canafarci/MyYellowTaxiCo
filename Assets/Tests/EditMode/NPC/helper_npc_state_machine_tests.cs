using UnityEngine;
using Moq;
using NUnit.Framework;
using TaxiGame.Installers;
using TaxiGame.Scripts;
using Zenject;

namespace TaxiGame.NPC.Tests
{
    public class helper_npc_state_machine_tests : ZenjectUnitTestFixture
    {
        //should call enter and exit on other states are switched
        //should tick on each update

        private Mock<IHelperNPCState> mockState;
        private Mock<IHelperNPCState> mockNextState;

        [SetUp]
        public void CommonInstall()
        {
            //Arrange
            mockState = new Mock<IHelperNPCState>();
            mockNextState = new Mock<IHelperNPCState>();

            Container.Bind<IHelperNPCState>()
            .WithId(HelperNPCStates.DeliverHat)
            .FromInstance(mockState.Object).
            AsSingle();

            Container.Bind<HelperNPCStateMachine>().FromNewComponentOnNewGameObject().AsSingle();
        }


        [Test]
        public void should_call_enter_on_next_state_when_current_state_changes()
        {
            //Arrange
            HelperNPCStateMachine stateMachine = Container.Resolve<HelperNPCStateMachine>();

            mockState.Setup(state => state.Tick())
                     .Callback(() =>
                     {
                         stateMachine.TransitionTo(mockNextState.Object);
                         UnityEngine.Debug.Log("Ticking first state!");
                     });

            mockNextState.Setup(state => state.Enter())
            .Callback(() => UnityEngine.Debug.Log("Entered second state!"));

            //Act
            mockState.Object.Tick();
            //Assert
            mockNextState.Verify(state => state.Enter(), Times.Once);
        }
        [Test]
        public void should_call_exit_on_first_state_when_first_state_changes()
        {
            //Arrange
            HelperNPCStateMachine stateMachine = Container.Resolve<HelperNPCStateMachine>();

            mockState.Setup(state => state.Tick())
                     .Callback(() =>
                     {
                         stateMachine.TransitionTo(mockNextState.Object);
                         UnityEngine.Debug.Log("Ticking first state!");
                     });

            //Act
            mockState.Object.Tick();
            //Assert
            mockState.Verify(state => state.Exit(), Times.Once);
        }


    }
}

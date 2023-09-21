using System.Collections;
using Moq;
using TaxiGame.Installers;
using TaxiGame.NPC;
using TaxiGame.Scripts;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace TaxiGame.NPC.Tests
{
    public class helper_npc_state_machine_tests : ZenjectIntegrationTestFixture
    {
        private Mock<IHelperNPCState> mockState;

        public void CommonInstall()
        {
            //Arrange
            mockState = new Mock<IHelperNPCState>();
            PreInstall();

            Container.Bind<IHelperNPCState>()
                .WithId(HelperNPCStates.DeliverHat)
                .FromInstance(mockState.Object).
                AsSingle();

            Container.Bind<HelperNPCStateMachine>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();
        }

        [UnityTest]
        public IEnumerator first_state_should_be_set_when_initialized()
        {
            CommonInstall();
            yield return new WaitForSeconds(2f);
            //Arrange
            mockState.Setup(state => state.Enter())
            .Callback(() => Debug.Log("Set this state to be the first state!"));
            //Act (When contructed, should call enter on first state on start )
            HelperNPCStateMachine stateMachine = Container.Resolve<HelperNPCStateMachine>();
            //Assert
            yield return new WaitForSeconds(2f);
            mockState.Verify(state => state.Enter(), Times.Once);
        }

        [UnityTest]
        public IEnumerator calls_tick_when_game_starts()
        {

            CommonInstall();
            yield return new WaitForSeconds(2f);
            //Act (When contructed, should call tick after game starts )
            var stateMachine = Container.Resolve<HelperNPCStateMachine>();

            yield return new WaitForSeconds(2f);
            //Assert
            mockState.Verify(state => state.Tick(), Times.AtLeastOnce());

            yield break;
        }
    }
}

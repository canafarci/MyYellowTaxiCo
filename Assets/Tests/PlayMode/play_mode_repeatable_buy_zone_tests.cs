using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using TaxiGame.Resource;
using Zenject;
using TaxiGame.Upgrades;

namespace TaxiGame.WaitZones.Tests
{
    public class play_mode_repeatable_buy_zone_tests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<PayMoneyProcessor>().AsSingle();
            Container.Bind<ResourceTracker>().AsSingle();
            Container.Bind<UpgradesFacade>().AsSingle();
            Container.Bind<GameConfigSO>()
                .FromScriptableObjectResource("ScriptableObjects/GameConfigSO")
                .AsSingle();
        }

        [UnityTest]
        public IEnumerator Iterate_FailsWhenTimeIsRemainingAndNoMoney()
        {
            //Arrange
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();
            PayMoneyProcessor payCalculator = Container.Resolve<PayMoneyProcessor>();
            ResourceTracker resourceTracker = Container.Resolve<ResourceTracker>();
            waitingZone.SetPayProcessor(payCalculator);
            float remainingTime = 10f;
            float remainingMoney = 20f;
            waitingZone.SetRemainingMoney(remainingMoney);
            waitingZone.SetRemainingTime(remainingTime);
            // WaitZoneConfig config = ScriptableObject.CreateInstance<WaitZoneConfig>();
            // config.OnSuccess = () => { };
            resourceTracker.ZeroMoney();
            GameObject instigator = new GameObject("instigator");

            //Act
            waitingZone.Begin(() => { }, instigator);
            yield return new WaitForSeconds(Globals.TIME_STEP * 2f);
            float remainingMoneyAfterIteration = waitingZone.GetRemainingMoney();
            TestContext.WriteLine($"remainingTime : {remainingTime}, remainingMoney: {remainingMoneyAfterIteration}");            //Assert

            //Assert
            Assert.AreEqual(remainingMoney, remainingMoneyAfterIteration);
            Assert.AreEqual(10f, remainingTime);
        }

    }

    // Test-specific derived class for RepeatableBuyingWaitingZone
    public class TestableRepeatableBuyingWaitingZone : RepeatableBuyingWaitingZone
    {
        public bool CheckCanContinue_Public(float remainingTime, float remainingMoney)
        {
            return CheckCanContinue(remainingTime);
        }
        public void Iterate_Public(ref float remainingTime, GameObject instigator)
        {
            Iterate(ref remainingTime, instigator);
        }
    }

}

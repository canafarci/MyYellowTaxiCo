using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using TaxiGame.Resource;
using Zenject;
using TaxiGame.Upgrades;

namespace TaxiGame.WaitZones.Tests
{
    public class RepeatableBuyingWaitingZoneTests : ZenjectUnitTestFixture
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
        [Test]
        public void SetPrice_SetsCorrectPrice()
        {
            float expectedCost = 100f;
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();

            waitingZone.SetPrice(expectedCost);

            Assert.AreEqual(expectedCost, waitingZone.GetCost());
        }

        [Test]
        public void CheckCanContinue_ReturnsFalseWhenRemainingTimeIsZero()
        {
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();
            float remainingTime = 0f;
            float remainingMoney = 20f;
            waitingZone.SetRemainingMoney(remainingMoney);

            bool canContinue = waitingZone.CheckCanContinue_Public(remainingTime, remainingMoney);

            Assert.IsFalse(canContinue);
        }

        [Test]
        public void CheckCanContinue_ReturnsFalseWhenRemainingMoneyIsZero()
        {
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();
            float remainingTime = 10f;
            float remainingMoney = 0f;
            waitingZone.SetRemainingMoney(remainingMoney);

            bool canContinue = waitingZone.CheckCanContinue_Public(remainingTime, remainingMoney);

            Assert.IsFalse(canContinue);
        }

        [Test]
        public void Iterate_CallsProcessPayMethod()
        {
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();
            PayMoneyProcessor payCalculator = Container.Resolve<PayMoneyProcessor>();
            ResourceTracker resourceTracker = Container.Resolve<ResourceTracker>();

            float remainingMoney = 20f;

            resourceTracker.OnCheatMoneyGain(100);
            waitingZone.SetPayProcessor(payCalculator);
            waitingZone.SetRemainingMoney(remainingMoney);


            float remainingTime = 10f;

            waitingZone.Iterate_Public(ref remainingTime, new GameObject());

            float remainingMoneyAfterIteration = waitingZone.GetRemainingMoney();

            Assert.IsTrue(20f > remainingMoneyAfterIteration);
            Assert.IsTrue(10f > remainingTime);
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
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Taxi.WaitZones;
using Taxi.Upgrades;
using UnityEditor;
using System;
using System.Collections;
using Taxi.UI;

namespace Taxi.WaitZones.Tests
{
    public class RepeatableBuyingWaitingZoneTests
    {
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
            PayMoneyProcessor payCalculator = new GameObject("PM", typeof(PayMoneyProcessor)).GetComponent<PayMoneyProcessor>();
            ResourceTracker resourceTracker = new GameObject("res", typeof(ResourceTracker)).GetComponent<ResourceTracker>();

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

        [UnityTest]
        public IEnumerator Iterate_FailsWhenTimeIsRemainingAndNoMoney()
        {
            //Arrange
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();
            PayMoneyProcessor payCalculator = new GameObject("PM", typeof(PayMoneyProcessor)).GetComponent<PayMoneyProcessor>();
            ResourceTracker resourceTracker = new GameObject("res", typeof(ResourceTracker)).GetComponent<ResourceTracker>();
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
            yield return new WaitForSeconds(Globals.WAIT_ZONES_TIME_STEP * 2f);
            float remainingMoneyAfterIteration = waitingZone.GetRemainingMoney();
            TestContext.WriteLine($"remainingTime : {remainingTime}, remainingMoney: {remainingMoneyAfterIteration}");            //Assert

            //Assert
            Assert.AreEqual(remainingMoney, remainingMoneyAfterIteration);
            Assert.AreEqual(10f, remainingTime);
        }

        [UnityTest]
        public IEnumerator RepeatableZone_UpgradesWhenSuccessful()
        {
            //Arrange
            TestableRepeatableBuyingWaitingZone waitingZone = new GameObject("BWZ", typeof(TestableRepeatableBuyingWaitingZone)).GetComponent<TestableRepeatableBuyingWaitingZone>();
            RepeatableBuyableWaitingZoneVisual visual = waitingZone.gameObject.AddComponent<RepeatableBuyableWaitingZoneVisual>();
            UpgradeReceiver receiver = new GameObject("UpgradeReceiver", typeof(UpgradeReceiver)).GetComponent<UpgradeReceiver>();
            UpgradeUtility utility = new GameObject("UpgradeUtility", typeof(UpgradeUtility)).GetComponent<UpgradeUtility>();
            ItemGenerator ItemGenerator = new GameObject("ItemGenerator", typeof(ItemGenerator)).GetComponent<ItemGenerator>();
            ResourceTracker resourceTracker = new GameObject("res", typeof(ResourceTracker)).GetComponent<ResourceTracker>();
            waitingZone.SetRemainingTime(0.00001f);
            waitingZone.SetRemainingMoney(0.00001f);
            resourceTracker.OnCheatMoneyGain(1000f);


            string[] assetNames = AssetDatabase.FindAssets("UpgradeData", new[] { "Assets/Settings" });
            foreach (string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var soData = AssetDatabase.LoadAssetAtPath<UpgradeDataSO>(SOpath);
                utility.SetUpgradeData(soData);
            }
            GameObject instigator = new GameObject("instigator");

            float spawnRateBefore = ItemGenerator.GetSpawnRate();
            IUpgradeCommand _upgradeCommand = new WaitZoneUpgradeCommand(waitingZone, visual, false);
            //Act
            waitingZone.Begin(() => { _upgradeCommand.Execute(); }, instigator);
            yield return new WaitForSeconds(Globals.WAIT_ZONES_TIME_STEP * 2f);

            float spawnRateAfter = ItemGenerator.GetSpawnRate();

            //Assert
            Assert.IsTrue(spawnRateAfter < spawnRateBefore);
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
        public void OnSuccess_Public(GameObject instigator)
        {
            OnSuccess(instigator);
        }
    }
}

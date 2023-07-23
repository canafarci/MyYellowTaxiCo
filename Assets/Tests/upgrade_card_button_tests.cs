using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;

namespace Taxi.Upgrades.Tests
{
    public class UpgradeCardButtonTests
    {
        private UpgradeCardButton upgradeCardButton;
        private bool upgradeCommandExecuted;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            GameObject upgradeCardButtonObject = new GameObject();
            upgradeCardButtonObject.SetActive(false);
            upgradeCardButton = upgradeCardButtonObject.AddComponent<UpgradeCardButton>();
            GameObject tracker = new GameObject("tracker", typeof(ResourceTracker));

            // Create and set the mock upgrade command.
            var upgradeCommandMock = new UpgradeCommandMock();
            upgradeCommandMock.OnExecute += () => upgradeCommandExecuted = true;
            upgradeCardButton.SetUpgradeCommand(upgradeCommandMock);
            upgradeCardButton.SetCheckCanUpgradeCommand(upgradeCommandMock);
            upgradeCardButtonObject.SetActive(true);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(upgradeCardButton.gameObject);
        }

        [Test]
        public void OnButtonClicked_Should_ExecuteUpgradeCommand()
        {
            // Arrange
            Button button = upgradeCardButton.gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => upgradeCardButton.OnButtonClicked());

            // Act
            button.onClick.Invoke();
            // Assert
            Assert.IsTrue(upgradeCommandExecuted, "Upgrade command was not executed.");
        }
    }

    public class UpgradeCommandMock : IUpgradeCommand
    {
        public event System.Action OnExecute;

        public void Execute()
        {
            OnExecute?.Invoke();
        }
    }
}

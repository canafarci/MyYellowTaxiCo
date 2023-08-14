using Moq;
using NUnit.Framework;
using TaxiGame.Upgrades;

namespace TaxiGame.Tests
{
    public class button_upgrade_command_tests
    {
        [Test]
        public void Execute_UpdatesIndexAndGameState()
        {
            // Arrange
            int initialIndex = 0;
            var mockUpgradeVisual = new Mock<UpgradeCardVisual>();
            var mockUpgradeReceiver = new Mock<IUpgradeReceiver>();

            // Create a real instance of UpgradeUtility
            var upgradeUtility = new UpgradeUtility();

            var buttonUpgradeCommand = new ButtonUpgradeCommand(
                mockUpgradeVisual.Object,
                UpgradeType.PlayerSpeed,
                upgradeUtility,  // Provide the real instance of UpgradeUtility
                mockUpgradeReceiver.Object
            );

            // Act
            buttonUpgradeCommand.Execute();

            // Assert
            mockUpgradeVisual.Verify(v => v.UpdateDotUI(initialIndex + 1), Times.Once);
            mockUpgradeReceiver.Verify(r => r.ReceiveUpgradeCommand(UpgradeType.PlayerSpeed, initialIndex + 1), Times.Once);
        }
    }
}

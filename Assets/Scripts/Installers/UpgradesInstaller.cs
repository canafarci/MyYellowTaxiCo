using System.Collections;
using System.Collections.Generic;
using Taxi.Upgrades;
using UnityEngine;
using Zenject;

namespace Taxi.Scripts
{
    public class UpgradesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.Bind<IUpgradeCommand>()
                .To<ButtonUpgradeCommand>().AsTransient();

            Container.Bind<UpgradeCardVisual>().FromComponentInHierarchy().AsTransient();

            Container.Bind<Enums.UpgradeType>()
                .FromMethod(GetUpgradeTypeFromButtonUpgrade).AsTransient();
        }

        private Enums.UpgradeType GetUpgradeTypeFromButtonUpgrade(InjectContext context)
        {
            // Get the UpgradeCardButton component from the context
            var upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            if (upgradeButton != null)
            {
                // Return the upgradeType obtained from the UpgradeCardButton
                return upgradeButton.GetUpgradeType();
            }
            else
            {
                // Handle the case when the context.ObjectInstance is not of the correct type
                // You can throw an exception or return a default value depending on your needs
                throw new System.Exception(
                    "Unable to resolve Enums.UpgradeType. The context.ObjectInstance is not of type UpgradeCardButton.");
            }
        }
    }
}

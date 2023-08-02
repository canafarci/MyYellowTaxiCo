using System.Collections;
using System.Collections.Generic;
using Taxi.Upgrades;
using UnityEngine;
using Zenject;

namespace Taxi.Installers
{
    public class UpgradesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.Bind<IUpgradeCommand>()
                .To<ButtonUpgradeCommand>().AsTransient();

            Container.Bind<UpgradeCardVisual>().FromMethod(GetVisualFromButtonUpgrade).AsTransient();

            Container.Bind<Enums.UpgradeType>()
                .FromMethod(GetUpgradeTypeFromButtonUpgrade).AsTransient();
        }

        private UpgradeCardVisual GetVisualFromButtonUpgrade(InjectContext context)
        {
            // Get the UpgradeCardButton component from the context
            UpgradeCardButton upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            // Return the upgradeType obtained from the UpgradeCardButton
            print(upgradeButton.gameObject.name);
            return upgradeButton.transform.GetComponent<UpgradeCardVisual>();
        }
        private Enums.UpgradeType GetUpgradeTypeFromButtonUpgrade(InjectContext context)
        {
            // Get the UpgradeCardButton component from the context
            UpgradeCardButton upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            return upgradeButton.GetUpgradeType();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Taxi.Upgrades;
using UnityEngine;
using Zenject;

namespace Taxi.Installers
{
    public class UpgradesInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeDataSO _upgradeData;
        [SerializeField] GameObject _upgradeReceiver;
        public override void InstallBindings()
        {
            Container.Bind<UpgradeDataSO>().FromInstance(_upgradeData).AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeUtility>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradesFacade>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeReceiver>()
            .FromComponentInNewPrefab(_upgradeReceiver)
            .AsSingle().Lazy();


            Container.Bind<Enums.UpgradeType>()
                .WithId(Enums.UpgradeCommandType.ButtonUpgrade)
                .FromMethod(GetUpgradeTypeFromButtonUpgrade)
                .AsTransient();

            Container.Bind<UpgradeCardVisual>()
                .WithId(Enums.UpgradeCommandType.ButtonUpgrade)
                .FromMethod(GetVisualFromButtonUpgrade)
                .AsTransient();

            Container.Bind<IUpgradeCommand>()
                .WithId(Enums.UpgradeCommandType.ButtonUpgrade)
                .To<ButtonUpgradeCommand>()
                .AsTransient();

            Container.Bind<Enums.UpgradeType>()
                .WithId(Enums.UpgradeCommandType.CheckCanUpgrade)
                .FromMethod(GetUpgradeTypeFromButtonUpgrade)
                .AsTransient();

            Container.Bind<UpgradeCardVisual>()
                .WithId(Enums.UpgradeCommandType.CheckCanUpgrade)
                .FromMethod(GetVisualFromButtonUpgrade)
                .AsTransient();

            Container.Bind<IUpgradeCommand>()
                .WithId(Enums.UpgradeCommandType.CheckCanUpgrade)
                .To<CheckCanUpgradeCommand>()
                .AsTransient();

            Container.Bind<IUpgradeCommand>()
                .WithId(Enums.UpgradeCommandType.LoadUpgrade)
                .To<LoadUpgradeCommand>()
                .AsTransient();
        }

        private UpgradeCardVisual GetVisualFromButtonUpgrade(InjectContext context)
        {
            // Get the UpgradeCardButton component from the context
            UpgradeCardButton upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            // Return the upgradeType obtained from the UpgradeCardButton
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

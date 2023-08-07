using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.NPC;
using Taxi.UI;
using Taxi.Upgrades;
using Taxi.WaitZones;
using UnityEngine;
using Zenject;

namespace Taxi.Installers
{
    public class UpgradesInstaller : MonoInstaller<UpgradesInstaller>
    {
        [SerializeField] private UpgradeDataSO _upgradeData;
        [SerializeField] GameObject _npcPrefab;

        public override void InstallBindings()
        {
            SetUpUtilities();
            SetUpReceivers();
            SetUpButtonUpgrades();
            SetUpLoadUpgrades();
            SetUpStackerSpeedUpgrade();

            Container.BindFactory<Vector3, HatHelperNPC, HatHelperNPC.Factory>().FromComponentInNewPrefab(_npcPrefab);
        }

        private void SetUpStackerSpeedUpgrade()
        {
            Container.Bind<ItemGenerator>()
                            .FromComponentInHierarchy()
                            .AsSingle();

            Container.Bind<StackerSpeedUpgradeReceiver>()
                .AsSingle();

            Container.Bind<RepeatableBuyingWaitingZone>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<RepeatableBuyableWaitingZoneVisual>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<IUpgradeCommand>()
                    .WithId(UpgradeCommandType.StackerSpeedUpgrade)
                    .To<StackerSpeedUpgradeCommand>()
                    .AsTransient();

            Container.Bind<bool>()
                    .WithId(UpgradeCommandType.StackerSpeedUpgrade)
                    .FromMethod(GetIsLoadingStackerUpgrade).
                    AsTransient();
        }

        private void SetUpLoadUpgrades()
        {
            Container.Bind<IUpgradeCommand>()
                .WithId(UpgradeCommandType.LoadUpgrade)
                .To<LoadUpgradeCommand>()
                .AsTransient();

            Container.Bind<IUpgradeReceiver>()
                .WithId(UpgradeReceiverType.Modifier)
                .To<ModifierUpgradesReceiver>()
                .FromResolve();

            Container.Bind<IUpgradeReceiver>()
                .WithId(UpgradeReceiverType.NPCSpawner)
                .To<HelperNPCSpawner>()
                .FromResolve();
        }

        private void SetUpButtonUpgrades()
        {
            Container.Bind<UpgradeCardVisual>()
                            .FromMethod(GetVisualFromButtonUpgrade)
                            .AsTransient();

            Container.Bind<UpgradeType>()
                .FromMethod(GetUpgradeTypeFromButtonUpgrade)
                .AsTransient();

            Container.Bind<IUpgradeReceiver>().
                FromMethod(GetUpgradeReceiver).
                AsTransient();

            Container.Bind<IUpgradeCommand>()
                .WithId(UpgradeCommandType.ButtonUpgrade)
                .To<ButtonUpgradeCommand>()
                .AsTransient();

            Container.Bind<IUpgradeCommand>()
                .WithId(UpgradeCommandType.CheckCanUpgrade)
                .To<CheckCanUpgradeCommand>()
                .AsTransient();
        }

        private void SetUpReceivers()
        {
            Container.Bind<ModifierUpgradesReceiver>()
            .FromComponentInHierarchy()
            .AsSingle();

            Container.Bind<HelperNPCSpawner>()
            .FromComponentInHierarchy()
            .AsSingle();
        }

        private void SetUpUtilities()
        {
            Container.Bind<UpgradeDataSO>().FromInstance(_upgradeData).AsSingle();
            Container.Bind<UpgradeUtility>().AsSingle();
            Container.Bind<UpgradesFacade>().AsSingle();
        }

        private IUpgradeReceiver GetUpgradeReceiver(InjectContext context)
        {
            UpgradeCardButton upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            if (upgradeButton.GetUpgradeType() == UpgradeType.HelperNPCCount)
            {
                return Container.Resolve<HelperNPCSpawner>();
            }
            else
            {
                return Container.Resolve<ModifierUpgradesReceiver>();
            }
        }

        private bool GetIsLoadingStackerUpgrade(InjectContext context)
        {
            UpgradeLoader loader = context.ParentContext.ObjectInstance as UpgradeLoader;

            if (loader)
                return true;
            else
                return false;
        }

        private UpgradeCardVisual GetVisualFromButtonUpgrade(InjectContext context)
        {
            // Get the UpgradeCardButton component from the context
            UpgradeCardButton upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            // Return the upgradeType obtained from the UpgradeCardButton
            return upgradeButton.transform.GetComponent<UpgradeCardVisual>();
        }
        private UpgradeType GetUpgradeTypeFromButtonUpgrade(InjectContext context)
        {
            // Get the UpgradeCardButton component from the context
            UpgradeCardButton upgradeButton = context.ParentContext.ObjectInstance as UpgradeCardButton;

            return upgradeButton.GetUpgradeType();
        }
    }
}

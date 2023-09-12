using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Installers
{
    public class UIInstaller : MonoInstaller<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Inventory>()
            .WithId("UI")
            .FromComponentInParents()
            .AsTransient();

            Container.Bind<CanvasGroup>()
            .FromComponentInChildren().
            AsSingle();
        }
    }
}

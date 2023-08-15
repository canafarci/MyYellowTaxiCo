using System.Collections;
using System.Collections.Generic;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Installers
{
    public class VisualsInstaller : MonoInstaller<VisualsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFeedbackVisual>()
                        .FromComponentInChildren()
                        .AsTransient();

        }
    }
}

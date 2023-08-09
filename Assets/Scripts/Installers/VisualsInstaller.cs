using System.Collections;
using System.Collections.Generic;
using Taxi.NPC;
using UnityEngine;
using Zenject;

namespace Taxi.Installers
{
    public class VisualsInstaller : MonoInstaller<VisualsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<HatDistributor>().
                FromComponentInChildren().
                AsTransient();
        }
    }
}

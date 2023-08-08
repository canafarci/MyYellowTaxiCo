using System;
using System.Collections.Generic;
using System.Linq;
using Taxi.NPC;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.Installers
{
        public class NPCInstaller : MonoInstaller<NPCInstaller>
        {
                [SerializeField] private GameObject _driverPrefab;
                [SerializeField] private GameObject _helperPrefab;
                [SerializeField] private DriverQueue[] _queues;

                public override void InstallBindings()
                {
                        Container.Bind<Animator>()
                                .FromComponentInChildren(false)
                                .AsTransient();

                        Container.Bind<NavMeshNPC>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<NavMeshAgent>()
                                .FromComponentInChildren(false)
                                .AsTransient();

                        Container.Bind<DriverQueue>()
                                .FromMethod(GetDriverQueue)
                                .AsTransient();

                        Container.BindFactory<Vector3, Quaternion, NavMeshNPC, NavMeshNPC.Factory>()
                                .WithId(NPCType.Driver)
                                .FromComponentInNewPrefab(_driverPrefab);

                        Container.BindFactory<Vector3, Quaternion, NavMeshNPC, NavMeshNPC.Factory>()
                                .WithId(NPCType.Helper)
                                .FromComponentInNewPrefab(_helperPrefab);
                }

                private DriverQueue GetDriverQueue(InjectContext context)
                {
                        DriverSpawner spawner = context.ObjectInstance as DriverSpawner;
                        return _queues.FirstOrDefault(x => x.HatType == spawner.HatType);
                }
        }
}
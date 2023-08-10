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
                [SerializeField] private GameObject _followerPrefab;
                [SerializeField] private GameObject _helperPrefab;
                [SerializeField] private DriverQueue[] _queues;

                public override void InstallBindings()
                {
                        Container.Bind<Animator>()
                                .FromComponentInChildren(false)
                                .AsTransient();

                        Container.Bind<NPCActionScheduler>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<NavMeshAgent>()
                                .FromComponentInChildren(false)
                                .AsTransient();

                        Container.Bind<NavMeshMover>()
                                .FromComponentInChildren()
                                .AsTransient();



                        BindDriverModel();
                        BindFactories();

                }

                private void BindDriverModel()
                {
                        Container.Bind<INPCQueue>()
                                .WithId(NPCType.Driver)
                                .FromMethod(GetDriverQueue)
                                .AsTransient();

                        Container.Bind<Stacker>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<DriverQueue>()
                                .WithId(ModelType.Distributor)
                                .FromComponentInParents()
                                .AsTransient();
                }

                private void BindFactories()
                {
                        Container.BindFactory<Vector3, Quaternion, NPCActionScheduler, NPCActionScheduler.Factory>()
                                .WithId(NPCType.Driver)
                                .FromComponentInNewPrefab(_driverPrefab);

                        Container.BindFactory<Vector3, Quaternion, NPCActionScheduler, NPCActionScheduler.Factory>()
                                .WithId(NPCType.Helper)
                                .FromComponentInNewPrefab(_helperPrefab);

                        Container.BindFactory<Vector3, Quaternion, NPCActionScheduler, NPCActionScheduler.Factory>()
                                .WithId(NPCType.Follower)
                                .FromComponentInNewPrefab(_followerPrefab);
                }

                private DriverQueue GetDriverQueue(InjectContext context)
                {
                        DriverSpawner spawner = context.ObjectInstance as DriverSpawner;
                        return _queues.FirstOrDefault(x => x.HatType == spawner.HatType);
                }
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TaxiGame.NPC;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.Installers
{
        public class NPCInstaller : MonoInstaller<NPCInstaller>
        {
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

                        Container.Bind<CustomerQueue>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<RiderNPCView>()
                                .FromComponentInChildren()
                                .AsTransient();
                        Container.Bind<Follower>()
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
                        Container.BindFactory<UnityEngine.Object, Vector3, Quaternion, RiderNPC, RiderNPC.Factory>()
                                .FromFactory<PrefabFactory<Vector3, Quaternion, RiderNPC>>();

                        Container.BindFactory<Vector3, Quaternion, HatHelperNPC, HatHelperNPC.Factory>()
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
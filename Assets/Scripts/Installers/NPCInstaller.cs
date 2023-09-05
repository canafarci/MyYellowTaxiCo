using System.Linq;
using TaxiGame.NPC;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.Installers
{
        public class NPCInstaller : MonoInstaller<NPCInstaller>
        {
                [SerializeField] private GameObject _helperPrefab;
                [SerializeField] private DriverQueueCoordinator[] _queues;

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

                        Container.Bind<RiderNPCController>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<Follower>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<DriverHatDistributor>().
                                FromComponentInChildren().
                                AsTransient();

                        Container.Bind<Wanderer>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<WandererSpawner>()
                                .FromComponentInHierarchy()
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

                        Container.Bind<DriverDispatcher>()
                                .FromComponentInChildren()
                                .AsTransient();

                        Container.Bind<DriverLookup>()
                                .FromComponentInParents()
                                .AsTransient();

                        Container.Bind<DriverQueueCoordinator>()
                                .WithId(ModelType.Distributor)
                                .FromComponentInParents()
                                .AsTransient();
                }

                private void BindFactories()
                {
                        Container.BindFactory<UnityEngine.Object, Vector3, Quaternion, RiderNPC, RiderNPC.Factory>()
                                .FromFactory<PrefabFactory<Vector3, Quaternion, RiderNPC>>();

                        Container.BindFactory<UnityEngine.Object, Transform, Waypoint[], Wanderer, Wanderer.Factory>()
                                .FromFactory<PrefabFactory<Transform, Waypoint[], Wanderer>>();

                        Container.BindFactory<Vector3, Quaternion, HatHelperNPC, HatHelperNPC.Factory>()
                                .WithId(NPCType.Helper)
                                .FromComponentInNewPrefab(_helperPrefab);
                }

                private DriverQueueCoordinator GetDriverQueue(InjectContext context)
                {
                        DriverSpawner spawner = context.ObjectInstance as DriverSpawner;
                        return _queues.FirstOrDefault(x => x.GetHatType() == spawner.HatType);
                }
        }
}
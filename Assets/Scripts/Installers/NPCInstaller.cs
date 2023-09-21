using System.Linq;
using TaxiGame.NPC;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using TaxiGame.NPC.Command;

namespace TaxiGame.Installers
{
    public class NPCInstaller : MonoInstaller<NPCInstaller>
    {
        [SerializeField] private GameObject _helperPrefab;
        [SerializeField] private DriverQueueCoordinator[] _queues;

        public override void InstallBindings()
        {
            BindCommandModule();
            BindCustomer();
            BindVIP();
            BindDriver();

            BindHelperNPC();

            BindFactories();
        }

        private void BindHelperNPC()
        {

            Container.Bind<NavMeshMover>()
                .FromComponentInChildren()
                .AsTransient();

            Container.Bind<HelperNPCLocationReferences>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<IHelperNPCState>()
                .WithId(HelperNPCStates.DeliverHat)
                .To<DeliverHatState>()
                .FromComponentInChildren()
                .AsTransient();
        }

        private void BindCommandModule()
        {
            Container.Bind<NPCCommandInvoker>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<RiderNPCController>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<NavMeshAgent>()
                    .FromComponentInChildren(false)
                    .AsTransient();
        }

        private void BindCustomer()
        {
            Container.Bind<CustomerQueue>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<Follower>()
                    .FromComponentInChildren()
                    .AsTransient();
        }

        private void BindVIP()
        {
            Container.Bind<Wanderer>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<WandererSpawner>()
                    .FromComponentInHierarchy()
                    .AsTransient();

            Container.BindFactory<UnityEngine.Object, Transform, WandererMoney, WandererMoney.Factory>()
                    .FromFactory<PrefabFactory<Transform, WandererMoney>>();
        }

        private void BindDriver()
        {
            Container.Bind<INPCQueue>()
                    .WithId(NPCType.Driver)
                    .FromMethod(GetDriverQueue)
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

            Container.Bind<DriverHatDistributor>()
                    .FromComponentInChildren()
                    .AsTransient();

            Container.Bind<QueueHatDropTrigger>()
                    .FromComponentInHierarchy()
                    .AsSingle();
        }

        private void BindFactories()
        {
            Container.BindFactory<UnityEngine.Object, Vector3, Quaternion, RiderNPC, RiderNPC.Factory>()
                    .FromFactory<PrefabFactory<Vector3, Quaternion, RiderNPC>>();

            Container.BindFactory<UnityEngine.Object, Transform, Waypoint[], Wanderer, Wanderer.Factory>()
                    .FromFactory<PrefabFactory<Transform, Waypoint[], Wanderer>>();


            Container.BindFactory<Transform, HelperNPC, HelperNPC.Factory>()
                    .WithId(NPCType.Helper)
                    .FromComponentInNewPrefab(_helperPrefab)
                    .UnderTransformGroup("HelperNPCs");
        }

        private DriverQueueCoordinator GetDriverQueue(InjectContext context)
        {
            DriverSpawner spawner = context.ObjectInstance as DriverSpawner;
            return _queues.FirstOrDefault(x => x.GetHatType() == spawner.HatType);
        }
    }
    public enum HelperNPCStates
    {
        DeliverHat
    }
}
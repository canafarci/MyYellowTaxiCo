using System;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class CarSpawner : MonoBehaviour
    {
        public GameObject[] BrokenCars { set { _brokenCars = value; } }
        [SerializeField] private Transform _enterNode, _exitNode;
        [SerializeField] private GameObject _item;
        [SerializeField] private GameObject[] _brokenCars;
        [SerializeField] private Animator _parkAnimator;
        [SerializeField] private bool _specialChargerSpawn, _specialBrokenSpawn, _specialTireSpawn;
        private MoneyStacker _stacker;

        //* NEWWW
        [SerializeField] private Enums.StackableItemType _hatType;
        [SerializeField] private CarSpawnerID _carSpawnerID;
        private Vehicle.Factory _factory;
        private VehicleSpot _spot;
        private VehicleManager _manager;

        public static event EventHandler<OnNewSpawnerActivatedEventArgs> OnNewSpawnerActivated;
        public event Action OnCarSpawned;

        [Inject]
        private void Init(Vehicle.Factory factory, VehicleSpot spot, MoneyStacker stacker, VehicleManager manager)
        {
            _factory = factory;
            _spot = spot;
            _stacker = stacker;
            _manager = manager;
        }

        private void Awake()
        {
            OnNewSpawnerActivated?.Invoke(this, new OnNewSpawnerActivatedEventArgs { HatType = _hatType });
        }

        private void Start()
        {
            SpawnCar(true);
        }

        public void SpawnCar(bool isInitialSpawn = false)
        {
            CarSpawnData spawnData = GetSpawnData(isInitialSpawn);


            VehicleConfig config = new VehicleConfig(_parkAnimator,
                                            _enterNode,
                                            _exitNode,
                                            _spot,
                                            this);

            _factory.Create(spawnData.Prefab, config, spawnData.VehicleInPlaceCallback);

            OnCarSpawned?.Invoke();
        }
        private CarSpawnData GetSpawnData(bool isInitialSpawn)
        {
            if (isInitialSpawn)
                return _manager.GetInitialCarSpawnData(_hatType);
            else
                return _manager.GetCarSpawnData(_carSpawnerID, _hatType);
        }

        //Getters-Setters
        ////////////////////////////public Enums.StackableItemType GetHatType() => _hatType;
        // public void SpawnCar()
        // {
        //     Car car;

        //     if (_specialChargerSpawn && !PlayerPrefs.HasKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE))
        //     {
        //         car = GameObject.Instantiate(_brokenCars[0],
        //                              _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
        //                              GetComponent<Car>();

        //         car.CarInPlaceHandler += FirstChargerReturn;

        //     }
        //     else if (_specialBrokenSpawn && !PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
        //     {
        //         car = GameObject.Instantiate(_brokenCars[0],
        //                              _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
        //                              GetComponent<Car>();

        //         car.CarInPlaceHandler += FirstBrokenReturn;

        //     }
        //     else if (_specialTireSpawn && !PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
        //     {
        //         car = GameObject.Instantiate(_brokenCars[0],
        //                              _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
        //                              GetComponent<Car>();

        //         car.CarInPlaceHandler += FirstTireReturn;
        //     }
        //     else
        //     {
        //         car = GameObject.Instantiate(_spawnIndex % _everyNthIsBroken == 0 || _spawnIndex == 0 ?
        //                              _brokenCars[UnityEngine.Random.Range(0, _brokenCars.Length)] :
        //                              _item,
        //                              _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
        //                              GetComponent<Car>();
        //         car.CarInPlaceHandler += OnCarInPlace;
        //     }

        //     //Start
        //     car.EnterNode = _enterNode;
        //     car.ExitNode = _nodes.EndNode;
        //     car.parkAnimator = _parkAnimator;
        //     car.Stacker = _stacker;

        //     //sub to actions
        //     car.CarRepairedHandler += OnCarRepaired;
        //     car.CarExitedHandler += OnCarExited;
        // }



        // void FirstChargerReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        // {
        //     // OnCarInPlace(car, IsBrokenCar, hatType);
        //     // FindObjectOfType<ConditionalTutorial>().FirstReturnCarWithoutCharger();
        // }
        // void FirstBrokenReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        // {
        //     // OnCarInPlace(car, IsBrokenCar, hatType);
        //     // FindObjectOfType<ConditionalTutorial>().SecondReturnBroken();
        // }
        // void FirstTireReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        // {
        //     // OnCarInPlace(car, IsBrokenCar, hatType);
        //     // FindObjectOfType<ConditionalTutorial>().ThirdReturnBroken();
        // }
    }

    public class OnNewSpawnerActivatedEventArgs : EventArgs
    {
        public Enums.StackableItemType HatType;
    }

}
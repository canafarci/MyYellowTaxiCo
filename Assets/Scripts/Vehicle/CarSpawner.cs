using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class CarSpawner : MonoBehaviour
    {
        public bool CarIsReady { get { return _currentCar != null; } }
        public bool DriverIsComing = false;
        public Enums.StackableItemType HatType;
        //public Action OnMovedAway;
        public GameObject[] BrokenCars { set { _brokenCars = value; } }
        [SerializeField] private Transform _enterNode, _exitNode;
        [SerializeField] private GameObject _item;
        [SerializeField] private GameObject[] _brokenCars;
        [SerializeField] private Animator _parkAnimator;
        [SerializeField] private int _everyNthIsBroken = 2;
        [SerializeField] private MoneyStacker _stacker;
        [SerializeField] private bool _specialChargerSpawn, _specialBrokenSpawn, _specialTireSpawn;
        private Car _currentCar = null;
        private RoadNode _nodes;
        private SpawnerUI _ui;
        private int _spawnIndex = 0;


        //* NEWWW

        private Vehicle.Factory _factory;
        private VehicleSpot _spot;

        public static event EventHandler<OnNewSpawnerActivatedEventArgs> OnNewSpawnerActivated;

        [Inject]
        private void Init(Vehicle.Factory factory, VehicleSpot spot)
        {
            _factory = factory;
            _spot = spot;
        }

        private void Awake()
        {
            _ui = GetComponent<SpawnerUI>();
            OnNewSpawnerActivated?.Invoke(this, new OnNewSpawnerActivatedEventArgs { HatType = HatType });
        }

        private void Start()
        {
            SpawnCar();
        }

        public void SpawnCar()
        {
            CarConfig config = new CarConfig(_parkAnimator,
                                            _enterNode,
                                            _exitNode,
                                            _spot,
                                            _stacker,
                                            this);
            _factory.Create(_item, config);
        }

        void OnCarInPlace(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        {
            if (!IsBrokenCar)
                _currentCar = car;
            if (_spawnIndex > 0)
            {

            }
        }

        void OnCarRepaired(Car car) => _currentCar = car;

        void OnCarExited(Car car)
        {
            car.CarInPlaceHandler -= OnCarInPlace;
            car.CarExitedHandler -= OnCarExited;
            car.CarExitedHandler -= OnCarRepaired;
            SpawnCar();
        }

        public void StartMove()
        {
            StartCoroutine(_ui.WaitLoop(false));
            _currentCar.TakeOffFX();
            _parkAnimator.Play("ParkOut");
            _spawnIndex += 1;
        }

        public void CallMove()
        {
            _currentCar.MoveAway();
            _currentCar = null;
            DriverIsComing = false;
            //OnMovedAway();
            _parkAnimator.enabled = false;
        }

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

        void InitialSpawn()
        {
            // Car car = GameObject.Instantiate(_item, _nodes.StartNode.position, _nodes.StartNode.rotation).GetComponent<Car>();

            // car.EnterNode = _enterNode;
            // car.ExitNode = _nodes.EndNode;
            // car.parkAnimator = _parkAnimator;
            // car.Stacker = _stacker;

            // car.GetComponent<Animator>().enabled = false;

            // car.CarInPlaceHandler += OnCarInPlace;
            // car.CarExitedHandler += OnCarExited;

            // car.GetToParkPosition();
        }

        void FirstChargerReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        {
            OnCarInPlace(car, IsBrokenCar, hatType);
            FindObjectOfType<ConditionalTutorial>().FirstReturnCarWithoutCharger();
        }
        void FirstBrokenReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        {
            OnCarInPlace(car, IsBrokenCar, hatType);
            FindObjectOfType<ConditionalTutorial>().SecondReturnBroken();
        }
        void FirstTireReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
        {
            OnCarInPlace(car, IsBrokenCar, hatType);
            FindObjectOfType<ConditionalTutorial>().ThirdReturnBroken();
        }
    }

    public class OnNewSpawnerActivatedEventArgs : EventArgs
    {
        public Enums.StackableItemType HatType;
    }

}
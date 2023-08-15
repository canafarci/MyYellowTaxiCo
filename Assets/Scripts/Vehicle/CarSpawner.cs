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
        public bool DriverIsComing = false;
        public Enums.StackableItemType HatType;
        //public Action OnMovedAway;
        public GameObject[] BrokenCars { set { _brokenCars = value; } }
        [SerializeField] private Transform _enterNode, _exitNode;
        [SerializeField] private GameObject _item;
        [SerializeField] private GameObject[] _brokenCars;
        [SerializeField] private Animator _parkAnimator;
        private MoneyStacker _stacker;
        [SerializeField] private bool _specialChargerSpawn, _specialBrokenSpawn, _specialTireSpawn;
        private SpawnerUI _ui;
        private int _spawnIndex = 0;


        //* NEWWW

        private Vehicle.Factory _factory;
        private VehicleSpot _spot;

        public static event EventHandler<OnNewSpawnerActivatedEventArgs> OnNewSpawnerActivated;

        [Inject]
        private void Init(Vehicle.Factory factory, VehicleSpot spot, MoneyStacker stacker)
        {
            _factory = factory;
            _spot = spot;
            _stacker = stacker;
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

        public void CallMove()
        {
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
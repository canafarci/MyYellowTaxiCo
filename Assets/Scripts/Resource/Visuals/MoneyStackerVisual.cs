using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Visuals;
using UnityEngine;
using Zenject;

namespace TaxiGame.Resource.Visuals
{
    public class MoneyStackerVisual : MonoBehaviour
    {
        //Dependencies
        private CollectibleMoney.Pool _collectibleMoneyPool;
        private TweeningService _tweeningService;
        private MoneyStackerTrigger _moneyStackerTrigger;
        private MoneyStacker _stacker;
        //Variables
        [SerializeField] private Transform _spawnPos;
        private int _currentRow = 0;
        private int _currentColumn = 0;
        private int _currentAisle = 0;
        private Stack<CollectibleMoney> _collectibleMoneyStack = new();
        //Constants
        private const int MAX_ROWS = 2;
        private const int MAX_COLUMNS = 2;
        private readonly Vector3 MONEY_DIMENSIONS = new Vector3(0.55f, 0.2f, 1.1f);

        [Inject]
        private void Init(MoneyStacker stacker,
                          MoneyStackerTrigger moneyStackerTrigger,
                          CollectibleMoney.Pool collectibleMoneyPool,
                          TweeningService tweeningService)
        {
            _stacker = stacker;
            _collectibleMoneyPool = collectibleMoneyPool;
            _tweeningService = tweeningService;
            _moneyStackerTrigger = moneyStackerTrigger;
        }

        private void Start()
        {
            _stacker.OnMoneyAddedToStack += MoneyStacker_MoneyAddedToStackHandler;
            _moneyStackerTrigger.OnMoneyPickedUpFromStack += MoneyStackerTrigger_MoneyPickedUpFromStackHandler;
        }

        private void MoneyStackerTrigger_MoneyPickedUpFromStackHandler(object sender, OnMoneyPickedUpFromStackArgs e)
        {
            CollectibleMoney collectibleMoney = _collectibleMoneyStack.Pop();
            Transform money = collectibleMoney.transform;
            money.parent = e.Target;

            Sequence moveSequence = _tweeningService.GenerateMoveSequence(money, Vector3.zero, 0.2f);
            moveSequence.onComplete = () => _collectibleMoneyPool.Despawn(collectibleMoney);

            DecreaseStackDimensions();
        }

        private void MoneyStacker_MoneyAddedToStackHandler()
        {
            CollectibleMoney collectibleMoney = _collectibleMoneyPool.Spawn(_spawnPos.position);
            _collectibleMoneyStack.Push(collectibleMoney);

            Transform money = collectibleMoney.transform;
            money.parent = transform;

            Vector3 endPos = CalculatePosition(_currentRow, _currentColumn, _currentAisle);

            _tweeningService.GenerateMoveSequence(money, endPos, 0.2f);

            IncreaseStackDimensions();
        }

        private Vector3 CalculatePosition(int row, int column, int aisle)
        {
            return new Vector3((MONEY_DIMENSIONS.x / 2) + ((row - 1) * MONEY_DIMENSIONS.x),
                                MONEY_DIMENSIONS.y * (aisle - 1),
                               (MONEY_DIMENSIONS.z / 2) + ((column - 1) * MONEY_DIMENSIONS.z)
                              );
        }

        private void IncreaseStackDimensions()
        {
            if (_currentColumn < MAX_COLUMNS - 1)
            {
                _currentColumn++;
            }
            else if (_currentRow < MAX_ROWS - 1)
            {
                _currentRow++;
                _currentColumn = 0;
            }
            else
            {
                _currentAisle++;
                _currentRow = 0;
                _currentColumn = 0;
            }

        }
        private void DecreaseStackDimensions()
        {
            if (_currentColumn > 0)
            {
                _currentColumn--;
            }
            else if (_currentRow > 0)
            {
                _currentRow--;
                _currentColumn = MAX_COLUMNS - 1;
            }
            else if (_currentAisle > 0)
            {
                _currentAisle--;
                _currentRow = MAX_ROWS - 1;
                _currentColumn = MAX_COLUMNS - 1;
            }
        }
    }
}

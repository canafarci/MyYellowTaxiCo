using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingItemGenerator : ItemGenerator
{
    [HideInInspector] public Enums.StackableItemType ItemType { get => _itemType; }
    public bool IsActive = false;
    [SerializeField] GameObject _objectToSwitch, _activator;
    RemoveItem _remover;
    [SerializeField] Enums.StackableItemType _itemType;
    static Dictionary<Collider, GameObject> _sliders;
    static Dictionary<Collider, Inventory> _inventories;
    [SerializeField] Transform _nextTutorialObjective;
    [SerializeField] private GameObject[] _itemsToActivate, _itemsToDeactivate;
    [SerializeField] string _identifier;
    IWaitLoop _ui;

    Dictionary<Collider, Coroutine> _spawnRoutines;
    void Awake()
    {
        _remover = GetComponent<RemoveItem>();
        _sliders = new Dictionary<Collider, GameObject>();
        _inventories = new Dictionary<Collider, Inventory>();
        _spawnRoutines = new Dictionary<Collider, Coroutine>();
        _ui = GetComponent<IWaitLoop>();
        _stacker = GetComponent<Stacker>();
    }

    private void Start()
    {
        if (_identifier == null) { return; }
        else if (!PlayerPrefs.HasKey(_identifier)) { return; }
        else if (PlayerPrefs.HasKey(_identifier))
        {
            if (_identifier.Contains("tuto"))
            {
                float delay = (int.Parse(_identifier[^2..])) / 200f;
                Invoke(nameof(TutorialActivate), delay);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            StartCoroutine(CheckActive(other));
            _sliders[other] = other.GetComponent<ComponentReference>().Slider;
            _inventories[other] = other.GetComponent<Inventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            if (_objectToSwitch != null)
                _objectToSwitch.SetActive(false);

            if (_spawnRoutines.ContainsKey(other))
            {
                Coroutine routine = _spawnRoutines[other];
                if (routine != null)
                    StopCoroutine(routine);
                _spawnRoutines.Remove(other);
            }

            if (_sliders.ContainsKey(other))
            {
                _sliders[other].SetActive(false);
            }
        }
    }

    private void TutorialActivate()
    {
        if (_nextTutorialObjective == null)
            GameManager.Instance.References.ObjectiveArrow.DisableArrow();
        else
            GameManager.Instance.References.ObjectiveArrow.ChangeObjective(_nextTutorialObjective);

        foreach (var item in _itemsToActivate)
            item.SetActive(true);

        foreach (var item in _itemsToDeactivate)
            item.SetActive(false);
    }

    IEnumerator CheckActive(Collider other)
    {
        while (true)
        {
            if (IsActive)
            {
                Coroutine spawnRoutine = StartCoroutine(SpawnLoop(other));
                _spawnRoutines[other] = spawnRoutine;
                yield break;
            }
            else
                yield return new WaitForSeconds(0.25f);
        }
    }

    protected override IEnumerator SpawnLoop(Collider other)
    {
        if (!_inventories.ContainsKey(other) || !_sliders.ContainsKey(other))
        {
            yield return new WaitForSeconds(0.25f);
            print(other);
            StartCoroutine(CheckActive(other));
            yield break;
        }

        Inventory inventory = _inventories[other];
        GameObject slider = _sliders[other];

        if ((_itemType == Enums.StackableItemType.Tire && inventory.GetItem(Enums.StackableItemType.Tire) != null && inventory.ItemCount == 1) ||
            (_itemType == Enums.StackableItemType.RepairBox && inventory.GetItem(Enums.StackableItemType.RepairBox) != null && inventory.ItemCount == 1)) { yield break; }

        if (inventory.MaxStackSize <= inventory.ItemCount && (
                                                            _itemType == Enums.StackableItemType.YellowHat ||
                                                            _itemType == Enums.StackableItemType.PurpleHat ||
                                                            _itemType == Enums.StackableItemType.BlackHat
                                                            )) { yield break; }
        if (_objectToSwitch != null)
            _objectToSwitch.SetActive(true);

        yield return StartCoroutine(_ui.WaitLoop(_spawnRate, null, null, null, slider));

        StackableItem item = GameObject.Instantiate(_stackableItem[0], _startTransform.position, _startTransform.rotation).GetComponent<StackableItem>();
        inventory.StackItem(item);
        Coroutine spawnRoutine = StartCoroutine(SpawnLoop(other));
        _spawnRoutines[other] = spawnRoutine;


        if (_identifier == null) { yield break; }
        else if (PlayerPrefs.HasKey(_identifier)) { yield break; }
        TutorialActivate();
        PlayerPrefs.SetInt(_identifier, 1);
    }

}

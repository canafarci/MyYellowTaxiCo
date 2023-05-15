using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StackCapacityIncrease : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text, _capacityText;
    [SerializeField] float _buyRate;
    [SerializeField] int _maxCapacity = 0;
    int _currentCapacity = 0;
    GeneratorUI _ui;
    Coroutine _routine;
    [SerializeField] string _identifier;

    private void Awake() => _ui = GetComponent<GeneratorUI>();
    private void Start()
    {
        if (_identifier == null) { return; }
        else if (!PlayerPrefs.HasKey(_identifier)) { return; }
        else if (PlayerPrefs.HasKey(_identifier))
        {
            int count = PlayerPrefs.GetInt(_identifier);

            for (int i = 0; i < count; i++)
            {
                Invoke("LoadIncreaseCapacity", i * 0.5f);
                CheckMaxCapacity();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(IncreaseCapacityLoop());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StopAllCoroutines();
    }
    void LoadIncreaseCapacity() => CapacityIncrease();
    public void CapacityIncrease()
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        inventory.MaxStackSize += 1;
        _capacityText.text = "LEVEL " + (_currentCapacity + 2).ToString() + "/3";

        _currentCapacity += 1;
        CheckMaxCapacity();
    }
    IEnumerator IncreaseCapacityLoop()
    {
        _routine = StartCoroutine(MoneyTween());
        yield return StartCoroutine(_ui.WaitLoop(_buyRate, _text, () => StopAllCoroutines()));

        StopCoroutine(_routine);

        CapacityIncrease();
        PlayerPrefs.SetInt(_identifier, _currentCapacity);


        yield break;
    }

    private void CheckMaxCapacity()
    {
        if (_currentCapacity == _maxCapacity)
            OnMaxCapacity();
    }
    public void OnMaxCapacity()
    {
        _text.text = "MAX";
        _text.DOColor(Color.white, 0.1f);
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator MoneyTween()
    {
        GameObject moneyPrefab = GameManager.Instance.References.GameConfig.MoneyPrefab;
        while (true)
        {
            Transform item = GameObject.Instantiate(moneyPrefab, GameManager.Instance.References.PlayerHand.position, moneyPrefab.transform.rotation).transform;
            item.transform.parent = null;
            Vector3 endPos = transform.position;
            Vector3 intermediatePos = new Vector3((endPos.x + item.transform.position.x) / 2f, endPos.y + 1f, (endPos.z + item.transform.position.z) / 2f);
            Vector3[] path = { item.transform.position, intermediatePos, endPos };
            Tween tween = item.transform.DOPath(path, .20f, PathType.CatmullRom, PathMode.Full3D);
            tween.onComplete = () => Destroy(item.gameObject);

            float targetRotation = Random.Range(0f, 360f);
            item.transform.DOLocalRotate(new Vector3(0, targetRotation, 0f), 0.20f);

            yield return new WaitForSeconds(0.11f);
        }
    }
}

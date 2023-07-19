using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSpawnLoop : MonoBehaviour
{
    [SerializeField] protected GameObject _stackableItem;
    [SerializeField] protected Transform _startTransform;
    [SerializeField] float _timeToUnlock;
    float _remainingTime;
    public Dictionary<Collider, GameObject> Sliders = new Dictionary<Collider, GameObject>();
    private void Awake()
    {
        _remainingTime = _timeToUnlock;
    }
    public IEnumerator SpawnLoop(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory.ItemCount > 0) { yield break; }

        GameObject slider = other.GetComponent<ComponentReference>().Slider;
        Sliders[other] = slider;
        Material mat = slider.transform.GetChild(1).GetComponent<Renderer>().material;
        float step = Globals.WAIT_ZONES_TIME_STEP;

        mat.SetFloat("_ClipUvUp", 1f);
        slider.SetActive(true);

        while (_remainingTime > 0f)
        {
            _remainingTime -= step;
            yield return new WaitForSeconds(step);

            mat.SetFloat("_ClipUvUp", (_remainingTime / _timeToUnlock));
        }
        slider.SetActive(false);

        StackableItem item = GameObject.Instantiate(_stackableItem, _startTransform.position, _startTransform.rotation).GetComponent<StackableItem>();
        inventory.StackItem(item);

        IUnlockable unlock = GetComponent<IUnlockable>();
        if (unlock == null || unlock.HasUnlockedBefore()) yield break;
        unlock.UnlockObject();
    }
}

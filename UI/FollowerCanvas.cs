using System.Collections;
using UnityEngine;

public class FollowerCanvas : MonoBehaviour
{
    [SerializeField] bool _isWanderer;
    Vector3 _baseOffset;
    Transform _npc;
    public void Remove()
    {
        this.enabled = false;
        StopAllCoroutines();
        Destroy(gameObject);
    }
    public void Initialize()
    {
        _npc = transform.parent;
        _baseOffset = _npc.position - transform.position;
        transform.SetParent(null);

        if (_isWanderer) return;
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            SwitchActivation(false);
            yield return new WaitForSeconds(6f);
            SwitchActivation(true);
        }
    }

    private void LateUpdate()
    {
        transform.position = _npc.position + _baseOffset;
    }

    void SwitchActivation(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(enable);
        }
    }
}
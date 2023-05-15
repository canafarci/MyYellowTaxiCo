using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNPC : MonoBehaviour
{
    public Enums.StackableItemType Hat;
    protected NavMeshAgent _agent;
    Coroutine _getToPosCoroutine = null;
    virtual protected void Awake() => _agent = GetComponent<NavMeshAgent>();
    virtual public IEnumerator OpenDoorAndGetIn(Vector3 pos)
    {
        _agent.radius = 0.01f;
        _agent.stoppingDistance = 0.001f;
        yield return StartCoroutine(GetToPosCoroutine(pos + new Vector3(0f, 0f, 1.5f)));

        GetComponentInChildren<Animator>().Play("Car Door Open");

        transform.DOScale(Vector3.one * 0.0001f, .5f);

        Destroy(gameObject, 0.6f);
    }

    public Coroutine GetToPos(Vector3 pos)
    {
        if (_getToPosCoroutine != null)
            StopCoroutine(_getToPosCoroutine);

        _getToPosCoroutine = StartCoroutine(GetToPosCoroutine(pos));
        return _getToPosCoroutine;
    }


    public IEnumerator GetToPosCoroutine(Vector3 pos)
    {
        yield return new WaitForSeconds(.1f);

        Vector3 tarxz = new Vector3(pos.x, 0f, pos.z);
        _agent.destination = tarxz;

        for (int i = 0; i < Mathf.Infinity; i++)
        {
            yield return new WaitForSeconds(.25f);
            if (_agent == null) { yield break; }

            Vector3 posxz = new Vector3(transform.position.x, 0f, transform.position.z);

            if (Vector3.Distance(posxz, tarxz) < 0.25f)
                break;
        }
    }
}

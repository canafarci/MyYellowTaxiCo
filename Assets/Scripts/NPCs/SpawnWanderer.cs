using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWanderer : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnTime;
    private float _currentTime;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(Globals.FIFTH_WANDERER_TUTORIAL_COMPLETE))
        {
            _currentTime = 10f;
        }
        else
            _currentTime = _spawnTime;
    }

    private void Start() => StartCoroutine(SpawnTimer());
    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            _currentTime -= 0.25f;
            if (_currentTime > 0f) { continue; }

            _currentTime = _spawnTime;
            Spawn();

        }
    }

    private void Spawn()
    {
        Wanderer wanderer = GameObject.Instantiate(_prefab, _spawnTransform.position, _prefab.transform.rotation).GetComponent<Wanderer>();
        wanderer.Initialize(_waypoints);
        if (!PlayerPrefs.HasKey(Globals.FIFTH_WANDERER_TUTORIAL_COMPLETE))
            FindObjectOfType<ConditionalTutorial>().OnFirstWandererSpawned(wanderer);
    }
}

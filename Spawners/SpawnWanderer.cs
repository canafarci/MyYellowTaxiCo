using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWanderer : MonoBehaviour
{
    [SerializeField] Waypoint[] _waypoints;
    [SerializeField] Transform _spawnTransform;
    [SerializeField] GameObject _prefab;
    [SerializeField] float _spawnTime;
    float _currentTime;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(StaticVariables.FIFTH_WANDERER_TUTORIAL_COMPLETE))
        {
            _currentTime = 10f;

        }
        else
            _currentTime = _spawnTime;
    }

    void Start() => StartCoroutine(SpawnTimer());
    IEnumerator SpawnTimer()
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

    void Spawn()
    {
        Wanderer wanderer = GameObject.Instantiate(_prefab, _spawnTransform.position, _prefab.transform.rotation).GetComponent<Wanderer>();
        wanderer.Initialize(_waypoints);
        if (!PlayerPrefs.HasKey(StaticVariables.FIFTH_WANDERER_TUTORIAL_COMPLETE))
            FindObjectOfType<ConditionalTutorial>().OnFirstWandererSpawned(wanderer);
    }
}

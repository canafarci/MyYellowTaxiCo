using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Follower
{
    List<Waypoint> _waypoints = new List<Waypoint>();
    int _currentWaypointIndex = 0;
    Coroutine _wanderRoutine, _moveRoutine;

    public void Initialize(Waypoint[] waypoints)
    {

        foreach (Waypoint wp in waypoints)
            _waypoints.Add(wp);

        _wanderRoutine = StartCoroutine(WandererLoop());
    }

    public override void FollowPlayer(Inventory inventory, bool isInQueue = false)
    {
        StopCoroutine(_wanderRoutine);
        StopCoroutine(_moveRoutine);

        inventory.AddFollowerToList(this);
        Target = inventory.transform;
        _followLoop = StartCoroutine(FollowLoop());

        if (!PlayerPrefs.HasKey(Globals.FIFTH_WANDERER_TUTORIAL_COMPLETE))
        {
            FindObjectOfType<ConditionalTutorial>().WandererTriggered();
        }

        if (_followerCanvas != null)
            _followerCanvas.Remove();
    }
    IEnumerator WandererLoop()
    {
        while (true)
        {
            Waypoint wp = _waypoints[_currentWaypointIndex];
            _moveRoutine = StartCoroutine(GetToPosCoroutine(wp.transform.position));
            yield return _moveRoutine;

            if (wp.StopWaypoint)
                yield return new WaitForSeconds(10f);

            else if (wp.EndWaypoint)
            {
                Destroy(gameObject, 0.3f);
                yield break;
            }

            _currentWaypointIndex++;
        }
    }
}

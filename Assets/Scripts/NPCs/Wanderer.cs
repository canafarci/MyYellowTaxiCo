using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class Wanderer : Follower
    {
        private List<Waypoint> _waypoints = new List<Waypoint>();
        private int _currentWaypointIndex = 0;
        private Coroutine _wanderCoroutine, _moveCoroutine;
        public void Initialize(Waypoint[] waypoints)
        {
            foreach (Waypoint wp in waypoints)
                _waypoints.Add(wp);

            _wanderCoroutine = StartCoroutine(WandererLoop());
        }
        public void FollowPlayer(Inventory inventory, bool isInQueue = false)
        {
            StopCoroutine(_wanderCoroutine);
            StopCoroutine(_moveCoroutine);

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
        private IEnumerator WandererLoop()
        {
            while (true)
            {
                // Waypoint wp = _waypoints[_currentWaypointIndex];
                // _moveCoroutine = StartCoroutine(MoveToPosition(wp.transform.position));
                // yield return _moveCoroutine;

                // if (wp.StopWaypoint)
                //     yield return new WaitForSeconds(10f);

                // else if (wp.EndWaypoint)
                // {
                //     Destroy(gameObject, 0.3f);
                //     yield break;
                // }

                _currentWaypointIndex++;
            }
        }
    }
}
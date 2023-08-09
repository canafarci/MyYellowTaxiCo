using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Taxi.NPC
{
    public class FollowerQueue : MonoBehaviour, INPCQueue
    {
        public bool OnSpawnCooldown = false;
        public int CurrentSitIndex = 0;
        private LinkedList<FollowerQueueSpot> _sitSpots = new LinkedList<FollowerQueueSpot>();
        [SerializeField] private Transform[] _sitTransforms;
        private void Awake()
        {
            int sitSpotsStartIndex = 3; // Add this line to calculate the starting index for sit spots

            for (int i = 0; i < _sitTransforms.Length; i++)
            {
                Transform tr = _sitTransforms[i];

                // Modify this line to check if the current index is greater than or equal to the sitSpotsStartIndex
                bool isSitSpot = i < sitSpotsStartIndex;

                _sitSpots.AddFirst(new FollowerQueueSpot(tr, null, isSitSpot));
            }
        }
        public bool IsFull()
        {
            return _sitSpots.First.Value.Follower != null;
        }
        public Follower TryUnload()
        {
            LinkedListNode<FollowerQueueSpot> spot = _sitSpots.Last;
            for (int i = 0; i < _sitSpots.Count; i++)
            {
                if (i > 3)
                    break;
                if (spot.Value.Follower != null && spot.Value.NPCIsSeated)
                {
                    Follower follower = spot.Value.Follower;
                    spot.Value.Follower = null;
                    spot.Value.NPCIsSeated = false;
                    MoveQueue();
                    return follower;
                }
                else
                    spot = spot.Previous;
            }
            return null;
        }
        public void AddToQueue(NavMeshNPC npc)
        {
            Follower follower = npc as Follower;
            LinkedListNode<FollowerQueueSpot> spot = _sitSpots.First;

            for (int i = 0; i < _sitSpots.Count - 1; i++)
            {
                if (spot.Next.Value.Follower == null)
                    spot = spot.Next;
                else
                    break;
            }

            GetToQueuePosition(follower, spot.Value);
        }
        private void MoveQueue()
        {
            LinkedListNode<FollowerQueueSpot> currentNode = _sitSpots.Last;

            while (currentNode != null)
            {
                if (currentNode.Value.Follower == null)
                {
                    LinkedListNode<FollowerQueueSpot> previousNode = currentNode.Previous;

                    while (previousNode != null)
                    {
                        if (previousNode.Value.Follower != null)
                        {
                            Follower follower = previousNode.Value.Follower;
                            currentNode.Value.Follower = follower;
                            previousNode.Value.Follower = null;
                            previousNode.Value.NPCIsSeated = false;

                            GetToQueuePosition(follower, currentNode.Value);
                            break;
                        }

                        previousNode = previousNode.Previous;
                    }
                }

                currentNode = currentNode.Previous;
            }
        }
        private void GetToQueuePosition(Follower follower, FollowerQueueSpot spot)
        {
            // if (!spot.IsSitSpot)
            //     //follower.GetToPos(spot.Transform.position);
            // else
            //     StartCoroutine(GetToPosAndSit(follower, spot));

            // spot.Follower = follower;
        }
        private IEnumerator GetToPosAndSit(Follower follower, FollowerQueueSpot spot)
        {
            //follower.GetComponentInChildren<Animator>().SetTrigger("MoveEmpty");

            //yield return follower.GetToPos(spot.Transform.position);

            Tween move = follower.transform.DOMove(spot.Transform.position, .2f);
            Tween rot = follower.transform.DORotate(spot.Transform.rotation.eulerAngles, .2f);

            yield return move.WaitForCompletion();
            follower.GetComponentInChildren<Animator>().SetBool("IsSitting", true);
            follower.GetComponentInChildren<Animator>().Play("NPC Standing to Sit");
            yield return new WaitForSeconds(0.25f);
            spot.NPCIsSeated = true;
        }
    }
    class FollowerQueueSpot
    {
        public Transform Transform;
        public bool IsSitSpot = false;
        public bool NPCIsSeated = false;
        public Follower Follower;
        public FollowerQueueSpot(Transform transform, Follower follower = null, bool IsSitSpot = false)
        {
            Transform = transform;
            this.IsSitSpot = IsSitSpot;
            this.Follower = follower;
        }
    }
}
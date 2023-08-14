using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Linq;

namespace TaxiGame.NPC
{
    public class HeliDropZone : MonoBehaviour
    {
        public Enums.StackableItemType HatType;
        [SerializeField] Transform _getInLocation;
        [SerializeField] Animator _animator;
        Coroutine _heliCoroutine;
        [SerializeField] MoneyStacker _stacker;
        [SerializeField] GameObject _npc;
        [SerializeField] int _money;
        private void Start()
        {
            _animator.Play("HeliAnim", 0, 0.5f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _heliCoroutine = StartCoroutine(MoveHeli());
        }
        IEnumerator MoveHeli()
        {
            Inventory inventory = GameManager.Instance.References.PlayerInventory;

            //NPCActionScheduler[] followers = inventory.GetFollowers(HatType);
            //if (followers.Length < 1) { yield break; }
            //yield return StartCoroutine(followers[0].OpenDoorAndGetIn(_getInLocation.position)); //TODO refactor
            _animator.Play("HeliAnim", 0, 0f);
            _stacker.StackItem(_money);

            _npc.SetActive(true);
            Vector3 baseScale = _npc.transform.localScale;
            _npc.transform.localScale = Vector3.one * 0.00001f;
            _npc.transform.DOScale(baseScale, .4f);

            IUnlockable unlock = GetComponent<IUnlockable>();
            if (unlock != null && !unlock.HasUnlockedBefore())
            {
                unlock.UnlockObject();
                PlayerPrefs.SetInt(Globals.FIFTH_WANDERER_TUTORIAL_COMPLETE, 1);
            }
            yield break;
        }
    }
}
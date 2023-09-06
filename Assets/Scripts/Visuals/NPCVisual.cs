using System.Collections;
using DG.Tweening;
using TaxiGame.Animations;
using TaxiGame.NPC.Command;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class NPCVisual : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
        private NPCActionInvoker _npc;

        [Inject]
        private void Init(NPCActionInvoker npc)
        {
            _npc = npc;
        }
        void Start()
        {
            _npc.OnNPCAnimationStateChanged += NavMeshNPC_NPCAnimationStateChangedHandler;

            OnSpawnFX();
        }

        private void NavMeshNPC_NPCAnimationStateChangedHandler(object sender, OnNPCAnimationStateChangedArgs e)
        {
            if (e.AnimationStateHash == AnimationValues.CAR_ENTER)
            {
                StartCoroutine(ShrinkDriver());
            }
        }

        private IEnumerator ShrinkDriver()
        {
            yield return new WaitForSeconds(AnimationValues.CAR_ENTER_ANIM_LENGTH);
            _visual.DOScale(Vector3.one * 0.0001f, AnimationValues.CAR_ENTER_ANIM_LENGTH);
        }

        private void OnSpawnFX()
        {
            Vector3 baseScale = _visual.lossyScale;
            _visual.localScale = Vector3.one * 0.0001f;
            _visual.DOScale(baseScale, 0.5f);
        }
    }
}

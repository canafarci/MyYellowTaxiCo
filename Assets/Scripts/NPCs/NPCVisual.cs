using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class NPCVisual : MonoBehaviour
    {
        private NPCActionScheduler _npc;

        [Inject]
        private void Init(NPCActionScheduler npc)
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
            if (e.AnimationStateHash == AnimationValues.ENTERING_CAR)
            {
                transform.DOScale(Vector3.one * 0.0001f, .5f);
            }
        }

        private void OnSpawnFX()
        {
            Vector3 baseScale = transform.lossyScale;
            transform.localScale = Vector3.one * 0.0001f;
            transform.DOScale(baseScale, 0.5f);
        }
    }
}

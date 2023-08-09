using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Taxi.Animations;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class NPCVisual : MonoBehaviour
    {
        private NavMeshNPC _npc;

        [Inject]
        private void Init(NavMeshNPC npc)
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
            if (e.AnimationStateString == AnimationValues.ENTERING_CAR)
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

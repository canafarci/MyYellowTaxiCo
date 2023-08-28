using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class NPCVisual : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
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
            if (e.AnimationStateHash == AnimationValues.CAR_ENTER)
            {
                _visual.DOScale(Vector3.one * 0.0001f, .5f);
            }
        }

        private void OnSpawnFX()
        {
            Vector3 baseScale = _visual.lossyScale;
            _visual.localScale = Vector3.one * 0.0001f;
            _visual.DOScale(baseScale, 0.5f);
        }
    }
}

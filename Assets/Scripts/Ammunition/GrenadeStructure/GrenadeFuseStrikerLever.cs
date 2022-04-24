﻿using System;
using StructureComponents;
using UnityEngine;
using Valve.VR;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseStrikerLever : MonoBehaviour
    {
        public event Action OnRelease;

        [SerializeField] private SteamVR_Action_Single _grip;

        private Attachment _attachment;
        private bool _isInit;

        public void Init(Attachment attachment)
        {
            _attachment = attachment;
            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit) return;
            if (!_attachment.TryGetHand(out var hand)) return;

            var type = hand.handType;

            if (_grip[type].axis > 0.75f) return;

            Debug.Log("Grip released.");
            OnRelease?.Invoke();
        }
    }
}
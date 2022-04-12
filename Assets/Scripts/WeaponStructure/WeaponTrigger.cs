using System;
using StructureComponents;
using UnityEngine;
using Valve.VR;

namespace WeaponStructure
{
    public class WeaponTrigger : MonoBehaviour
    {
        public event Action OnTriggerPressed;

        [SerializeField] private SteamVR_Action_Single _trigger;
        [SerializeField] private SteamVR_Action_Vibration _vibration;

        private Attachment _attachment;
        private bool _isPressed;
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

            if (_trigger[type].axis > 0.9f && !_isPressed)
            {
                _isPressed = true;
                Pulse();
                OnTriggerPressed?.Invoke();
            }
            else if (_trigger[type].axis < 0.5f && _isPressed)
            {
                _isPressed = false;
            }
        }

        private void Pulse()
        {
            if (!_attachment.TryGetHand(out var hand)) return;
            var type = hand.handType;

            _vibration.Execute(0f, 0.3f, 75, 1f, type);
        }
    }
}
using System;
using UnityEngine;
using Valve.VR;

namespace StructureComponents
{
    public class Popper : MonoBehaviour
    {
        public event Action OnButtonPressed;

        [SerializeField] private SteamVR_Action_Boolean _popperButton;

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

            if (!_popperButton[type].lastStateDown) return;

            OnButtonPressed?.Invoke();
        }
    }
}
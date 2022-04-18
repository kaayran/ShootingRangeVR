using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace StructureComponents
{
    [RequireComponent(typeof(Throwable))]
    public class Attachment : MonoBehaviour
    {
        public event Action OnDrop;

        private Hand _attachedHand;

        public void Init()
        {
            _attachedHand = null;
        }

        public bool TryGetHand(out Hand hand)
        {
            hand = _attachedHand;

            return _attachedHand != null;
        }

        private void OnAttachedToHand(Hand hand)
        {
            _attachedHand = hand;
        }

        private void OnDetachedFromHand(Hand hand)
        {
            OnDrop?.Invoke();
            _attachedHand = null;
        }
    }
}
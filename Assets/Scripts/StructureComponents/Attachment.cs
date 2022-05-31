using System;
using JetBrains.Annotations;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace StructureComponents
{
    public class Attachment : MonoBehaviour
    {
        public event Action OnDrop;
        public event Action OnTake;

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

        [UsedImplicitly]
        private void OnAttachedToHand(Hand hand)
        {
            OnTake?.Invoke();
            _attachedHand = hand;
        }

        [UsedImplicitly]
        private void OnDetachedFromHand(Hand hand)
        {
            OnDrop?.Invoke();
            _attachedHand = null;
        }

        private void OnDestroy()
        {
            _attachedHand.DetachObject(gameObject);
            _attachedHand.ResetTemporarySkeletonRangeOfMotion();
        }
    }
}
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Utilities.Logger
{
    [RequireComponent(typeof(Throwable))]
    public class DummyNotAppend : MonoBehaviour
    {
        private void OnAttachedToHand(Hand hand)
        {
            InGameLogger.Log($"{gameObject.name}", false);
        }

        private void OnDetachedFromHand(Hand hand)
        {
            InGameLogger.Clear();
        }
    }
}
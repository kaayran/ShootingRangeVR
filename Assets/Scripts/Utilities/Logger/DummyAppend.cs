using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Utilities.Logger
{
    [RequireComponent(typeof(Throwable))]
    public class DummyAppend : MonoBehaviour
    {
        private void OnAttachedToHand(Hand hand)
        {
            InGameLogger.Log($"{gameObject.name}", true);
        }

        private void OnDetachedFromHand(Hand hand)
        {
            InGameLogger.Clear();
        }
    }
}
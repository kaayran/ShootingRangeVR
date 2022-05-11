using JetBrains.Annotations;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Utilities.Logger
{
    [RequireComponent(typeof(Throwable))]
    public class DummyNotAppend : MonoBehaviour
    {
        [UsedImplicitly]
        private void OnAttachedToHand(Hand hand)
        {
            InGameLogger.Log($"{gameObject.name}", false);
        }

        [UsedImplicitly]
        private void OnDetachedFromHand(Hand hand)
        {
            InGameLogger.Clear();
        }
    }
}
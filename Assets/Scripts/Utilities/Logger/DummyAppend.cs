using JetBrains.Annotations;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Utilities.Logger
{
    [RequireComponent(typeof(Throwable))]
    public class DummyAppend : MonoBehaviour
    {
        [UsedImplicitly]
        private void OnAttachedToHand(Hand hand)
        {
            InGameLogger.Log($"{gameObject.name}", true);
        }

        [UsedImplicitly]
        private void OnDetachedFromHand(Hand hand)
        {
            InGameLogger.Clear();
        }
    }
}
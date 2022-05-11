using Valve.VR.InteractionSystem;

namespace Utilities
{
    public class GripThrowable : Throwable
    {
        protected override void OnHandHoverBegin(Hand hand)
        {
            if (attached || catchingSpeedThreshold == -1) return;

            var catchingThreshold = catchingSpeedThreshold *
                                    SteamVR_Utils.GetLossyScale(Player.instance.trackingOriginTransform);

            var bestGrabType = hand.GetBestGrabbingType();

            if (bestGrabType != GrabTypes.Grip) return;

            if (rigidbody.velocity.magnitude >= catchingThreshold)
                hand.AttachObject(gameObject, bestGrabType, attachmentFlags);
        }

        protected override void HandHoverUpdate(Hand hand)
        {
            var startingGrabType = hand.GetGrabStarting();

            if (startingGrabType != GrabTypes.Grip) return;
            
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            hand.HideGrabHint();
        }
    }
}
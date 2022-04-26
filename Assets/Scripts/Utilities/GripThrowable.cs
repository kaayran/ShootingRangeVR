using Valve.VR.InteractionSystem;

namespace Utilities
{
    public class GripThrowable : Throwable
    {
        protected override void OnHandHoverBegin(Hand hand)
        {
            bool showHint = false;

            // "Catch" the throwable by holding down the interaction button instead of pressing it.
            // Only do this if the throwable is moving faster than the prescribed threshold speed,
            // and if it isn't attached to another hand
            if (!attached && catchingSpeedThreshold != -1)
            {
                float catchingThreshold = catchingSpeedThreshold *
                                          SteamVR_Utils.GetLossyScale(Player.instance.trackingOriginTransform);

                GrabTypes bestGrabType = hand.GetBestGrabbingType();

                if (bestGrabType == GrabTypes.Grip)
                {
                    if (rigidbody.velocity.magnitude >= catchingThreshold)
                    {
                        hand.AttachObject(gameObject, bestGrabType, attachmentFlags);
                        showHint = false;
                    }
                }
            }
        }

        protected override void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();

            if (startingGrabType == GrabTypes.Grip)
            {
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
                hand.HideGrabHint();
            }
        }
    }
}
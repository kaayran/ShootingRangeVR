using System;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(Throwable))]
    public class GrenadeFuseRing : MonoBehaviour
    {
        public event Action OnDrag;

        private Attachment _attachment;
        private Joint _joint;

        public void Init(Attachment attachment)
        {
            _joint = GetComponent<Joint>();
            FreezeJoint();

            _attachment = attachment;
            _attachment.OnDrop += OnDrop;
            _attachment.OnTake += OnTake;
        }

        private void OnTake()
        {
            UnFreezeJoint();
        }

        private void OnDrop()
        {
            FreezeJoint();
        }

        public void FreezeJoint()
        {
            _joint.breakForce = 100000;
            _joint.breakTorque = 100000;
        }

        public void UnFreezeJoint()
        {
            _joint.breakForce = 750;
            _joint.breakTorque = 750;
        }

        private void OnJointBreak(float breakForce)
        {
            OnDrag?.Invoke();

            gameObject.transform.parent = null;
        }
    }
}
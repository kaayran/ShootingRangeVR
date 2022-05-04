using System;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(Throwable))]
    [RequireComponent(typeof(Attachment))]
    public class GrenadeFuseRing : MonoBehaviour
    {
        public event Action OnDrag;

        private Attachment _attachment;
        private Joint _joint;

        public void Init()
        {
            _attachment = GetComponent<Attachment>();
            _joint = GetComponent<Joint>();
            FreezeJoint();

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

        private void FreezeJoint()
        {
            if (_joint == null) return;
            _joint.breakForce = 100000;
            _joint.breakTorque = 100000;
        }

        private void UnFreezeJoint()
        {
            if (_joint == null) return;
            _joint.breakForce = 500;
            _joint.breakTorque = 500;
        }

        private void OnJointBreak(float breakForce)
        {
            OnDrag?.Invoke();

            gameObject.transform.parent = null;
        }
    }
}
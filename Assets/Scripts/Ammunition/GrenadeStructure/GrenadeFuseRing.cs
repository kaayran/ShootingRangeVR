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
        private Interactable _interactable;
        private Joint _joint;
        private GrenadeFuseAudio _fuseAudio;
        private Throwable _throwable;

        public void Init(GrenadeFuseAudio fuseAudio)
        {
            _fuseAudio = fuseAudio;
            _attachment = GetComponent<Attachment>();
            _joint = GetComponent<Joint>();
            _interactable = GetComponent<Interactable>();
            _throwable = GetComponent<Throwable>();

            _interactable.enabled = false;
            _throwable.enabled = false;
            FreezeJoint();

            _attachment.OnDrop += OnDrop;
            _attachment.OnTake += OnTake;
        }

        private void OnTake()
        {
            UnFreezeJoint();
            _interactable.enabled = true;
            _throwable.enabled = true;
        }

        private void OnDrop()
        {
            FreezeJoint();
            _interactable.enabled = false;
            _throwable.enabled = false;
        }

        private void FreezeJoint()
        {
            if (_joint == null) return;
            _joint.breakForce = Mathf.Infinity;
            _joint.breakTorque = Mathf.Infinity;
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

            _fuseAudio.PlayRingSound();

            gameObject.transform.parent = null;
        }
    }
}
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(Throwable))]
    public class GrenadeSafetyRing : MonoBehaviour
    {
        public event Action OnDrag;

        private Vector3 _startPos;
        private const float _DistanceToDrag = 0.15f;

        public void Init()
        {
            _startPos = transform.position;
        }

        private void HandAttachedUpdate(Hand hand)
        {
            Debug.Log("UPDATED");

            var updatedDistance = Vector3.Distance(_startPos, transform.position);

            if (!(updatedDistance > _DistanceToDrag)) return;

            OnDrag?.Invoke();
            Debug.Log("Ring Dragged!");
        }
    }
}
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Nade
{
    public class FuseRing : MonoBehaviour
    {
        public event Action OnDrag;

        private Vector3 _startPos;
        private float _distanceToDrag = 0.15f;

        public void Init()
        {
            _startPos = transform.position;
        }

        private void HandAttachedUpdate(Hand hand)
        {
            Debug.Log("UPDATED");

            var updatedDistance = Vector3.Distance(_startPos, transform.position);

            if (!(updatedDistance > _distanceToDrag)) return;

            OnDrag?.Invoke();
            Debug.Log("Ring Dropped");
        }
    }
}
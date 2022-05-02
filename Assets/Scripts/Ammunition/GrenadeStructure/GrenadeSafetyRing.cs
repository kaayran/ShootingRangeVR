using System;
using UnityEngine;
using Utilities.Logger;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(Throwable))]
    public class GrenadeSafetyRing : MonoBehaviour
    {
        public event Action OnDrag;

        public void Init()
        {
        }

        private void OnJointBreak(float breakForce)
        {
            InGameLogger.Log($"Ring Broke: {breakForce}", true);
            OnDrag?.Invoke();

            gameObject.transform.parent = null;
        }
    }
}
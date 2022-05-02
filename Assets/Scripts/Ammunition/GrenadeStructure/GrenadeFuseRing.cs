using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(Throwable))]
    public class GrenadeFuseRing : MonoBehaviour
    {
        public event Action OnDrag;

        public void Init()
        {
        }

        private void OnJointBreak(float breakForce)
        {
            OnDrag?.Invoke();

            gameObject.transform.parent = null;
        }
    }
}
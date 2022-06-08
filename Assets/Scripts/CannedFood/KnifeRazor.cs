using System;
using JetBrains.Annotations;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CannedFood
{
    public class KnifeRazor : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_rb.velocity.magnitude < 25f) return;
            var contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, transform.forward) > -0.5f) return;

            _rb.isKinematic = true;
        }

        [UsedImplicitly]
        private void OnAttachedToHand(Hand hand)
        {
            _rb.isKinematic = false;
        }
    }
}
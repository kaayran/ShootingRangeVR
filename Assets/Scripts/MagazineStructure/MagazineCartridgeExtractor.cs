﻿using Ammunition.CartridgeStructure;
using StructureComponents;
using UnityEngine;

namespace MagazineStructure
{
    internal class MagazineCartridgeExtractor : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;

        private Container<Cartridge, CartridgeType> _container;
        private Attachment _attachment;
        private Popper _popper;

        public void Init(Container<Cartridge, CartridgeType> container, Popper popper, Attachment attachment)
        {
            _attachment = attachment;
            _container = container;
            _popper = popper;
            _popper.OnButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            if (!_container.TryPop(out var cartridge)) return;
            _attachment.TryGetHand(out var hand);

            var cartridgeTransform = cartridge.transform;
            cartridgeTransform.position = _extractTransform.position;
            cartridgeTransform.rotation = _extractTransform.rotation;

            var rb = cartridge.GetComponent<Rigidbody>();
            rb.velocity = hand.GetTrackedObjectVelocity();
            rb.angularVelocity = hand.GetTrackedObjectAngularVelocity();

            cartridge.Activate();
        }
    }
}
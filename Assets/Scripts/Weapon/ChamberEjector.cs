using Ammo;
using UnityEngine;

namespace Weapon
{
    public class ChamberEjector : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;
        [SerializeField] private float _force;
        [SerializeField] private float _torque;

        private Cartridge _cartridge;
        private Chamber _chamber;

        public void Init(Slide slide, Chamber chamber)
        {
            _chamber = chamber;

            slide.OnBackward += Backward;
            slide.OnBackward += BackwardDropPatron;

            _cartridge = null;
        }

        public void SetCartridge(Cartridge cartridge)
        {
            _cartridge = cartridge;
            _cartridge.Deactivate();
        }

        private void BackwardDropPatron()
        {
            if (!_chamber.TryPopPatron(out var patron)) return;
            patron.Activate();

            var patronTransform = patron.GetTransform();
            patronTransform.position = _extractTransform.position;
            patronTransform.rotation = _extractTransform.rotation;
            patronTransform.parent = null;

            var patronRigidbody = patron.GetRigidbody();
            patronRigidbody.AddForce(patronTransform.forward * _force, ForceMode.Impulse);

            var torque = ForceTorque();
            patronRigidbody.AddTorque(torque, ForceMode.Impulse);
        }

        private void Backward()
        {
            if (_cartridge == null) return;
            _cartridge.Activate();

            var cartridgeTransform = _cartridge.GetTransform();
            cartridgeTransform.position = _extractTransform.position;
            cartridgeTransform.rotation = _extractTransform.rotation;
            cartridgeTransform.parent = null;

            var cartridgeRigidbody = _cartridge.GetRigidbody();
            cartridgeRigidbody.AddForce(cartridgeTransform.forward * _force, ForceMode.Impulse);

            var torque = ForceTorque();
            cartridgeRigidbody.AddTorque(torque, ForceMode.Impulse);

            _cartridge = null;
        }

        private Vector3 ForceTorque()
        {
            var xTorque = Random.Range(0f, _torque);
            var yTorque = Random.Range(0f, _torque);
            var zTorque = Random.Range(0f, _torque);
            var torque = new Vector3(xTorque, yTorque, zTorque);
            return torque;
        }
    }
}
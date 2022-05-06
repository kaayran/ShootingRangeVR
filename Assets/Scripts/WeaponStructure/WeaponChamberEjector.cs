using Ammunition.CartridgeStructure;
using Particle;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponChamberEjector : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;
        [SerializeField] private ParticleComponent _particle;
        [SerializeField] private float _force;
        [SerializeField] private float _torque;

        private CartridgeCase _cartridgeCase;
        private WeaponChamber _weaponChamber;

        public void Init(WeaponSlide weaponSlide, WeaponChamber weaponChamber)
        {
            _weaponChamber = weaponChamber;

            weaponSlide.OnBackward += Backward;
            weaponSlide.OnBackward += BackwardDropCartridge;

            _cartridgeCase = null;
        }

        public void SetCartridge(CartridgeCase cartridgeCase)
        {
            _cartridgeCase = cartridgeCase;
            _cartridgeCase.Deactivate();
        }

        private void BackwardDropCartridge()
        {
            if (!_weaponChamber.TryPopCartridge(out var cartridge)) return;
            cartridge.Activate();

            var cartridgeTransform = cartridge.GetTransform();
            cartridgeTransform.position = _extractTransform.position;
            cartridgeTransform.rotation = _extractTransform.rotation;
            cartridgeTransform.parent = null;

            var cartridgeRigidbody = cartridge.GetRigidbody();
            cartridgeRigidbody.AddForce(cartridgeTransform.forward * _force, ForceMode.Impulse);

            var torque = ForceTorque();
            cartridgeRigidbody.AddTorque(torque, ForceMode.Impulse);
        }

        private void Backward()
        {
            if (_cartridgeCase == null) return;
            _cartridgeCase.Activate();

            var position = _extractTransform.position;
            var rotation = _extractTransform.rotation;
            var particle = Instantiate(_particle, position, rotation);
            particle.Play();

            var cartridgeTransform = _cartridgeCase.GetTransform();
            cartridgeTransform.position = position;
            cartridgeTransform.rotation = rotation;
            cartridgeTransform.parent = null;

            var cartridgeRigidbody = _cartridgeCase.GetRigidbody();
            cartridgeRigidbody.AddForce(cartridgeTransform.forward * _force, ForceMode.Impulse);

            var torque = ForceTorque();
            cartridgeRigidbody.AddTorque(torque, ForceMode.Impulse);

            _cartridgeCase = null;
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
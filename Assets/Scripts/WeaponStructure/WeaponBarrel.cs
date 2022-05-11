using System;
using Ammunition.CartridgeStructure;
using Particle;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponBarrel : MonoBehaviour
    {
        public event Action OnBulletFired;

        [SerializeField] private Transform _fireTransform;
        [SerializeField] private ParticleComponent _particle;
        [SerializeField] private float _recoil;

        private Rigidbody _rb;

        public void Init()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Fire(Cartridge cartridge)
        {
            var bullet = cartridge.GetBullet();
            var bulletTransform = bullet.GetTransform();

            var position = _fireTransform.position;
            var rotation = _fireTransform.rotation;

            bulletTransform.position = position;
            bulletTransform.rotation = rotation;

            // Take cartridge velocity and deploy it with it speed
            var speed = cartridge.GetBulletSpeed();

            bullet.Deploy(speed);
            _rb.AddTorque(transform.right * -_recoil / 100, ForceMode.Impulse);

            var particle = Instantiate(_particle, position, rotation);
            particle.Play();


            OnBulletFired?.Invoke();

            cartridge.DestroyCartridge();
        }
    }
}
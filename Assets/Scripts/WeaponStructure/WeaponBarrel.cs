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

        public void Init()
        {
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

            var particle = Instantiate(_particle, position, rotation);
            particle.Play();


            OnBulletFired?.Invoke();

            cartridge.DestroyCartridge();
        }
    }
}
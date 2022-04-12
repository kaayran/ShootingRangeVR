using System;
using Ammunition.CartridgeStructure;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponBarrel : MonoBehaviour
    {
        public event Action OnBulletFired;

        [SerializeField] private Transform _fireTransform;

        public void Init()
        {
        }

        public void Fire(Cartridge cartridge)
        {
            var bullet = cartridge.GetBullet();
            var bulletTransform = bullet.GetTransform();
            bulletTransform.position = _fireTransform.position;
            bulletTransform.rotation = _fireTransform.rotation;

            // Take cartridge velocity and deploy it with it speed
            var speed = cartridge.GetBulletSpeed();

            bullet.Deploy(speed);
            OnBulletFired?.Invoke();

            cartridge.DestroyCartridge();
        }
    }
}
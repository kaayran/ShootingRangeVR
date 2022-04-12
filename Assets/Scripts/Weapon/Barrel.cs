using System;
using Ammunition;
using Ammunition.Cartridge;
using UnityEngine;

namespace Weapon
{
    public class Barrel : MonoBehaviour
    {
        public event Action OnBulletFired;

        [SerializeField] private Transform _fireTransform;
        [SerializeField] private float _speed;
        
        public void Init()
        {
            
        }

        public void Fire(Cartridge cartridge)
        {
            var bullet = cartridge.GetBullet();
            var bulletTransform = bullet.GetTransform();
            bulletTransform.position = _fireTransform.position;
            bulletTransform.rotation = _fireTransform.rotation;

            bullet.Deploy(_speed);
            OnBulletFired?.Invoke();

            cartridge.DestroyPatron();
        }
    }
}
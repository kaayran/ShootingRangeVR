using System;
using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    [CreateAssetMenu(fileName = "CARTRIDGE_NAME", menuName = "Cartridge Type", order = 0)]
    public class CartridgeType : ScriptableObject, ICloneable
    {
        public string CartridgeName;
        public float BulletSpeed;
        public string Caliber;

        public CartridgeType(string cartridgeName, float bulletSpeed, string caliber)
        {
            CartridgeName = cartridgeName;
            BulletSpeed = bulletSpeed;
            Caliber = caliber;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
using System;
using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    [CreateAssetMenu(fileName = "CARTRIDGE_NAME", menuName = "Cartridge Type", order = 0)]
    public class CartridgeType : ScriptableObject, ICloneable
    {
        public string CartridgeName;
        public float BulletSpeed;

        public CartridgeType(string cartridgeName, float bulletSpeed)
        {
            CartridgeName = cartridgeName;
            BulletSpeed = bulletSpeed;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
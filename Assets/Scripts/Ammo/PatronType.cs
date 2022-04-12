using System;
using UnityEngine;

namespace Ammo
{
    [CreateAssetMenu(fileName = "PATRON_TYPE", menuName = "Patron Type", order = 0)]
    public class PatronType : ScriptableObject, ICloneable
    {
        public string PatronName;
        public float BulletSpeed;

        public PatronType(string patronName, float bulletSpeed)
        {
            PatronName = patronName;
            BulletSpeed = bulletSpeed;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
using UnityEngine;

namespace Resources
{
    public struct DamageData
    {
        public Vector3 contactPoint { get; set; }
        public Quaternion normalPoint { get; set; }
        public Vector3 force { get; set; }
    }
}
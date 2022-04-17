using System;
using Interfaces;
using UnityEngine;

namespace Detections
{
    public class BulletHitDetection : MonoBehaviour, IDamageable
    {
        public void Damage(Vector3 contactPoint)
        {
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), contactPoint, transform.rotation);
        }
    }
}
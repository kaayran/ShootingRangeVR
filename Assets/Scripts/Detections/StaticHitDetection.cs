using Interfaces;
using Resources;
using UnityEngine;

namespace Detections
{
    [RequireComponent(typeof(Collider))]
    public class StaticHitDetection : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject _damageMark;

        public void Damage(DamageData damageData)
        {
            var obj = Instantiate(_damageMark, damageData.contactPoint, damageData.normalPoint);
            obj.transform.SetParent(transform, true);
            
            Debug.Log("I have damaged by static impact!");
        }
    }
}
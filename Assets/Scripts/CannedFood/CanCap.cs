using System;
using UnityEngine;

namespace CannedFood
{
    public class CanCap : MonoBehaviour
    {
        [SerializeField] private int _takesToOpen = 3;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<KnifeRazor>(out _)) return;
            if (_takesToOpen-- == 1) return;
            
            Destroy(gameObject);
        }
    }
}
using System;
using Interfaces;
using Resources;
using UnityEngine;

namespace Targets
{
    public class HumanTarget : MonoBehaviour, IDamageable
    {
        public event Action<int, int> OnHit;

        [SerializeField] private Transform _forwardPos;
        [SerializeField] private Transform _backwardPos;
        [SerializeField] private Transform _center;

        private Collider _collider;
        private float _radius;
        private float _speed;
        private int _count;

        public void Init(float speed)
        {
            _collider = GetComponent<Collider>();
            _radius = _collider.bounds.extents.x;
            _speed = speed;
        }

        public void MoveForward()
        {
            if (transform.position.z >= _forwardPos.position.z)
            {
                transform.position = _forwardPos.position;
                return;
            }

            transform.Translate(transform.forward * (_speed * Time.deltaTime));
        }

        public void MoveBackward()
        {
            if (transform.position.z <= _backwardPos.position.z)
            {
                transform.position = _backwardPos.position;
                return;
            }

            transform.Translate(transform.forward * (-_speed * Time.deltaTime));
        }

        public void Damage(DamageData damageData)
        {
            var hitPosition = damageData.contactPoint;
            var distance = Vector3.Distance(_center.position, hitPosition);

            if (distance > _radius) distance = _radius;
            if (distance < 0) distance = 0;

            var accuracy = (int) ((_radius - distance) / _radius * 100);
            _count++;
            
            OnHit?.Invoke(accuracy, _count);
        }
    }
}
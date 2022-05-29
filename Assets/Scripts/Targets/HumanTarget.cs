using System;
using Interfaces;
using Particle;
using Resources;
using UnityEngine;

namespace Targets
{
    public class HumanTarget : MonoBehaviour, IDamageable
    {
        public event Action<int, int> OnHit;

        [SerializeField] private ParticleComponent _particle;
        [SerializeField] private Transform _startPos;
        [SerializeField] private Transform _endPos;
        [SerializeField] private Transform _center;
        
        private float _radius;
        private Collider _collider;
        private float _offset;
        private float _distance;
        private float _speed;
        private int _count;

        public void Init(float speed)
        {
            _collider = GetComponent<Collider>();
            _radius = Math.Abs(transform.InverseTransformVector(_collider.bounds.extents).x);
            _speed = speed;
            _distance = Vector3.Distance(_startPos.position, _endPos.position);
            _offset = _distance / 1000;
        }

        public void MoveForward()
        {
            var currDistance = Vector3.Distance(transform.position, _endPos.position);
            if (currDistance + _offset > _distance)
            {
                transform.position = _startPos.position;
                return;
            }

            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        public void MoveBackward()
        {
            var currDistance = Vector3.Distance(_startPos.position, transform.position);
            if (currDistance + _offset > _distance)
            {
                transform.position = _endPos.position;
                return;
            }

            transform.Translate(Vector3.forward * (-_speed * Time.deltaTime));
        }

        public void Damage(DamageData damageData)
        {
            var hitPosition = damageData.contactPoint;
            var distance = Vector3.Distance(_center.position, hitPosition);

            if (distance > _radius) distance = _radius;

            var accuracy = (int) ((_radius - distance) / _radius * 100f);
            _count++;
            
            var particle = Instantiate(_particle, hitPosition, damageData.normalPoint);
            particle.Play();
            
            OnHit?.Invoke(accuracy, _count);
        }
    }
}
using System;
using StructureComponents;
using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosion : MonoBehaviour
    {
        public event Action OnExplosion;

        [SerializeField] private float _radius;
        [SerializeField] private float _force;

        private Container<GrenadeFuse, GrenadeFuseType> _container;
        private GrenadeFuseExploder _exploder;
        private GrenadeFuse _fuse;

        public void Init(Container<GrenadeFuse, GrenadeFuseType> container)
        {
            _container = container;

            _container.OnEntered += Entered;
            _container.OnPopped += Popped;
        }

        private void Popped()
        {
            _fuse = null;
            _exploder.OnDetonate -= Detonate;
            _exploder = null;
        }

        private void Entered()
        {
            _fuse = _container.GetStored();
            _exploder = _fuse.GetExploder();
            _exploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            var hits = Physics.OverlapSphere(transform.position, _radius);

            ExplosionPaint();
            
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                }
            }
            
            OnExplosion?.Invoke();

            _container.OnEntered -= Entered;
            _container.OnPopped -= Popped;

            _exploder.OnDetonate -= Detonate;
            _exploder = null;

            Destroy(gameObject);
        }
        
        private void ExplosionPaint()
        {
            var ray = new Ray(transform.position, transform.up * -0.1f);

            InGameLogger.Log($"{Physics.Raycast(ray, out var hit1)}", true);
            InGameLogger.Log($"{hit1.transform.name}", true);
            InGameLogger.Log($"{hit1.barycentricCoordinate}", true);
            if (!Physics.Raycast(ray, out var hit)) return;

            var mesh = hit.transform.GetComponent<MeshFilter>().mesh;
            var vertices = mesh.vertices;
            var colors = new Color32[vertices.Length];
            var hitVertex = hit.barycentricCoordinate;

            for (var i = 0; i < vertices.Length; i++)
            {
                var distance = Vector3.Distance(hitVertex.normalized, vertices[i].normalized) - 0.15f;
                colors[i] = Color32.Lerp(Color.black, Color.green, distance);
            }

            mesh.colors32 = colors;
        }
    }
}
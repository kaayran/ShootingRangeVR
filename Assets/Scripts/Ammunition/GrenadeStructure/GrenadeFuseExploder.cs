using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploder : MonoBehaviour
    {
        public event Action OnDetonate;

        [SerializeField] private float _radius;
        [SerializeField] private float _force;

        private GrenadeFuseStriker _fuseStriker;

        public void Init(GrenadeFuseStriker striker)
        {
            _fuseStriker = striker;
            _fuseStriker.OnStrike += Strike;
        }

        private void Strike(float delay)
        {
            StartCoroutine(ExplosionDelay(delay));
            _fuseStriker.OnStrike -= Strike;
        }

        private IEnumerator ExplosionDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            var hits = Physics.OverlapSphere(transform.position, _radius);

            ExplosionPaint();
            
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                }
            }
            
            OnDetonate?.Invoke();

            Destroy(gameObject);
        }

        private void ExplosionPaint()
        {
            var ray = new Ray(transform.position, transform.up * -1f);

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
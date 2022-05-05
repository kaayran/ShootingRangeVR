using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class VertexPainter : MonoBehaviour
    {
        private MeshRenderer _renderer;
        private Mesh _mesh;
        private MeshFilter _filter;

        private void Start()
        {
            _filter = GetComponent<MeshFilter>();
            _mesh = _filter.mesh;

            var vertices = _mesh.vertices;
            var colors = new Color32[vertices.Length];
            var hitVertex = vertices[Random.Range(0, vertices.Length - 1)];


            for (int i = 0; i < vertices.Length; i++)
            {
                var distance = Vector3.Distance(hitVertex.normalized, vertices[i].normalized) - 0.25f;
                colors[i] = Color32.Lerp(Color.black, Color.green, distance);
            }

            _mesh.colors32 = colors;
        }
    }
}
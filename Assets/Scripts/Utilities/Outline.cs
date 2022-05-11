using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Utilities
{
    [DisallowMultipleComponent]
    public class Outline : MonoBehaviour
    {
        private static readonly HashSet<Mesh> RegisteredMeshes = new HashSet<Mesh>();

        [Serializable]
        private class ListVector3
        {
            public List<Vector3> data;
        }

        [SerializeField, HideInInspector] private List<Mesh> bakeKeys = new List<Mesh>();

        [SerializeField, HideInInspector] private List<ListVector3> bakeValues = new List<ListVector3>();

        private Renderer[] _renderers;
        private Material _outlineMaskMaterial;
        private Material _outlineFillMaterial;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();

            _outlineMaskMaterial = Instantiate(UnityEngine.Resources.Load<Material>(@"Materials/OutlineMask"));
            _outlineFillMaterial = Instantiate(UnityEngine.Resources.Load<Material>(@"Materials/OutlineFill"));

            _outlineMaskMaterial.name = "OutlineMask (Instance)";
            _outlineFillMaterial.name = "OutlineFill (Instance)";

            LoadSmoothNormals();
        }

        [UsedImplicitly]
        private void OnHandHoverBegin()
        {
            foreach (var rend in _renderers)
            {
                var materials = rend.sharedMaterials.ToList();

                materials.Add(_outlineMaskMaterial);
                materials.Add(_outlineFillMaterial);

                rend.materials = materials.ToArray();
            }
        }

        [UsedImplicitly]
        private void OnHandHoverEnd()
        {
            foreach (var rend in _renderers)
            {
                var materials = rend.sharedMaterials.ToList();

                materials.Remove(_outlineMaskMaterial);
                materials.Remove(_outlineFillMaterial);

                rend.materials = materials.ToArray();
            }
        }

        private void OnDestroy()
        {
            Destroy(_outlineMaskMaterial);
            Destroy(_outlineFillMaterial);
        }

        private void LoadSmoothNormals()
        {
            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                if (!RegisteredMeshes.Add(meshFilter.sharedMesh))
                {
                    continue;
                }

                var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
                var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

                meshFilter.sharedMesh.SetUVs(3, smoothNormals);

                var rend = meshFilter.GetComponent<Renderer>();

                if (rend != null)
                {
                    CombineSubmeshes(meshFilter.sharedMesh, rend.sharedMaterials);
                }
            }

            foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (!RegisteredMeshes.Add(skinnedMeshRenderer.sharedMesh))
                {
                    continue;
                }

                var sharedMesh = skinnedMeshRenderer.sharedMesh;
                sharedMesh.uv4 = new Vector2[sharedMesh.vertexCount];

                CombineSubmeshes(sharedMesh, skinnedMeshRenderer.sharedMaterials);
            }
        }

        private List<Vector3> SmoothNormals(Mesh mesh)
        {
            var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
                .GroupBy(pair => pair.Key);

            var smoothNormals = new List<Vector3>(mesh.normals);

            foreach (var group in groups)
            {
                if (group.Count() == 1)
                {
                    continue;
                }

                var smoothNormal = Vector3.zero;

                foreach (var pair in group)
                {
                    smoothNormal += smoothNormals[pair.Value];
                }

                smoothNormal.Normalize();

                foreach (var pair in group)
                {
                    smoothNormals[pair.Value] = smoothNormal;
                }
            }

            return smoothNormals;
        }

        private void CombineSubmeshes(Mesh mesh, IReadOnlyCollection<Material> materials)
        {
            if (mesh.subMeshCount == 1)
            {
                return;
            }

            if (mesh.subMeshCount > materials.Count)
            {
                return;
            }

            mesh.subMeshCount++;
            mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
        }
    }
}
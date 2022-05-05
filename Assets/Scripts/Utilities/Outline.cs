using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    [DisallowMultipleComponent]
    public class Outline : MonoBehaviour
    {
        private static readonly HashSet<Mesh> RegisteredMeshes = new HashSet<Mesh>();

        public enum Mode
        {
            OutlineAll,
            OutlineVisible,
            OutlineHidden,
            OutlineAndSilhouette,
            SilhouetteOnly
        }

        public Mode outlineMode
        {
            get => _outlineMode;
            set
            {
                _outlineMode = value;
                _needsUpdate = true;
            }
        }

        public Color outlineColor
        {
            get => _outlineColor;
            set
            {
                _outlineColor = value;
                _needsUpdate = true;
            }
        }

        public float outlineWidth
        {
            get => _outlineWidth;
            set
            {
                _outlineWidth = value;
                _needsUpdate = true;
            }
        }

        [Serializable]
        private class ListVector3
        {
            public List<Vector3> data;
        }

        [SerializeField] private Mode _outlineMode;

        [SerializeField] private Color _outlineColor = Color.white;

        [SerializeField, Range(0f, 10f)] private float _outlineWidth = 2f;

        [Header("Optional")]
        [SerializeField, Tooltip(
             "Precompute enabled: Per-vertex calculations are performed in the editor and serialized with the object. "
             + "Precompute disabled: Per-vertex calculations are performed at runtime in Awake(). This may cause a pause for large meshes.")]
        private bool precomputeOutline;

        [SerializeField, HideInInspector] private List<Mesh> bakeKeys = new List<Mesh>();

        [SerializeField, HideInInspector] private List<ListVector3> bakeValues = new List<ListVector3>();

        private Renderer[] _renderers;
        private Material _outlineMaskMaterial;
        private Material _outlineFillMaterial;

        private bool _needsUpdate;

        private void Awake()
        {
            // Cache renderers
            _renderers = GetComponentsInChildren<Renderer>();

            // Instantiate outline materials
            _outlineMaskMaterial = Instantiate(UnityEngine.Resources.Load<Material>(@"Materials/OutlineMask"));
            _outlineFillMaterial = Instantiate(UnityEngine.Resources.Load<Material>(@"Materials/OutlineFill"));

            _outlineMaskMaterial.name = "OutlineMask (Instance)";
            _outlineFillMaterial.name = "OutlineFill (Instance)";

            // Retrieve or generate smooth normals
            LoadSmoothNormals();

            // Apply material properties immediately
            _needsUpdate = true;
        }

        private void OnHandHoverBegin()
        {
            foreach (var rend in _renderers)
            {
                // Append outline shaders
                var materials = rend.sharedMaterials.ToList();

                materials.Add(_outlineMaskMaterial);
                materials.Add(_outlineFillMaterial);

                rend.materials = materials.ToArray();
            }
        }

        private void OnHandHoverEnd()
        {
            foreach (var rend in _renderers)
            {
                // Remove outline shaders
                var materials = rend.sharedMaterials.ToList();

                materials.Remove(_outlineMaskMaterial);
                materials.Remove(_outlineFillMaterial);

                rend.materials = materials.ToArray();
            }
        }

        private void OnDestroy()
        {
            // Destroy material instances
            Destroy(_outlineMaskMaterial);
            Destroy(_outlineFillMaterial);
        }

        private void Bake()
        {
            // Generate smooth normals for each mesh
            var bakedMeshes = new HashSet<Mesh>();

            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                // Skip duplicates
                if (!bakedMeshes.Add(meshFilter.sharedMesh))
                {
                    continue;
                }

                // Serialize smooth normals
                var smoothNormals = SmoothNormals(meshFilter.sharedMesh);

                bakeKeys.Add(meshFilter.sharedMesh);
                bakeValues.Add(new ListVector3() {data = smoothNormals});
            }
        }

        private void LoadSmoothNormals()
        {
            // Retrieve or generate smooth normals
            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                // Skip if smooth normals have already been adopted
                if (!RegisteredMeshes.Add(meshFilter.sharedMesh))
                {
                    continue;
                }

                // Retrieve or generate smooth normals
                var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
                var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

                // Store smooth normals in UV3
                meshFilter.sharedMesh.SetUVs(3, smoothNormals);

                // Combine submeshes
                var rend = meshFilter.GetComponent<Renderer>();

                if (rend != null)
                {
                    CombineSubmeshes(meshFilter.sharedMesh, rend.sharedMaterials);
                }
            }

            // Clear UV3 on skinned mesh renderers
            foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                // Skip if UV3 has already been reset
                if (!RegisteredMeshes.Add(skinnedMeshRenderer.sharedMesh))
                {
                    continue;
                }

                // Clear UV3
                var sharedMesh = skinnedMeshRenderer.sharedMesh;
                sharedMesh.uv4 = new Vector2[sharedMesh.vertexCount];

                // Combine submeshes
                CombineSubmeshes(sharedMesh, skinnedMeshRenderer.sharedMaterials);
            }
        }

        private List<Vector3> SmoothNormals(Mesh mesh)
        {
            // Group vertices by location
            var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
                .GroupBy(pair => pair.Key);

            // Copy normals to a new list
            var smoothNormals = new List<Vector3>(mesh.normals);

            // Average normals for grouped vertices
            foreach (var group in groups)
            {
                // Skip single vertices
                if (group.Count() == 1)
                {
                    continue;
                }

                // Calculate the average normal
                var smoothNormal = Vector3.zero;

                foreach (var pair in group)
                {
                    smoothNormal += smoothNormals[pair.Value];
                }

                smoothNormal.Normalize();

                // Assign smooth normal to each vertex
                foreach (var pair in group)
                {
                    smoothNormals[pair.Value] = smoothNormal;
                }
            }

            return smoothNormals;
        }

        private void CombineSubmeshes(Mesh mesh, Material[] materials)
        {
            // Skip meshes with a single submesh
            if (mesh.subMeshCount == 1)
            {
                return;
            }

            // Skip if submesh count exceeds material count
            if (mesh.subMeshCount > materials.Length)
            {
                return;
            }

            // Append combined submesh
            mesh.subMeshCount++;
            mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
        }

        private void UpdateMaterialProperties()
        {
            // Apply properties according to mode
            _outlineFillMaterial.SetColor("_OutlineColor", _outlineColor);

            switch (_outlineMode)
            {
                case Mode.OutlineAll:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", _outlineWidth);
                    break;

                case Mode.OutlineVisible:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", _outlineWidth);
                    break;

                case Mode.OutlineHidden:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", _outlineWidth);
                    break;

                case Mode.OutlineAndSilhouette:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", _outlineWidth);
                    break;

                case Mode.SilhouetteOnly:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
                    break;
            }
        }
    }
}
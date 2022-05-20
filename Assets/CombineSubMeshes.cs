using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineSubMeshes : MonoBehaviour
{
    [ContextMenu("Combine Meshes")]
    private void CombineMeshes()
    {
        var meshFilters = GetComponentsInChildren<MeshFilter>();
        var combine = new CombineInstance[meshFilters.Length];

        for (var i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        var mesh = new Mesh
        {
            name = "Floor"
        };
        
        mesh.CombineMeshes(combine);
        transform.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.SetActive(true);
    }
}

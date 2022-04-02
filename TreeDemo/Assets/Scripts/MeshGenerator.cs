using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [Title("Foliage Variables"), SerializeReference]
    public FoliageBase[] foliages = null;

    [Title("Debugging Variables")]
    public float profilePointSize = 0.25f;

    private MeshRenderer _meshRenderer;
    public MeshRenderer meshRenderer
    {
        get
        {
            if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
            return _meshRenderer;
        }
        set => _meshRenderer = value;
    }

    private MeshFilter _meshFilter;
    public MeshFilter meshFilter
    {
        get
        {
            if (_meshFilter == null) _meshFilter = GetComponent<MeshFilter>();
            return _meshFilter;
        }
        set => _meshFilter = value;
    }

    void OnDrawGizmos()
    {
        foreach (FoliageBase fol in foliages)
        {
            meshFilter.sharedMesh = fol.GenerateMesh(meshFilter.sharedMesh);
            Vector3[] profilePoints = meshFilter.mesh.vertices;
            foreach (var point in profilePoints)
            {
                Gizmos.DrawSphere(point, profilePointSize);
            }


        }

    }
}
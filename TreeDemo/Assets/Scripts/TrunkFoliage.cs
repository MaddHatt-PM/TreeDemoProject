using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class TrunkFoliage : FoliageBase
{
    [Range(0f, 1f)] public float lowerTaper = 0f;
    [Range(0f, 1f)] public float upperTaper = 0f;

    public override Vector3[] GenerateProfile()
    {
        Vector3[] profile = new Vector3[]
        {
            new Vector3(0f, 0f),
            new Vector3(radius * (1f - lowerTaper), 0f),
            new Vector3(radius * (1f - upperTaper), height),
            new Vector3(0f, height)
        };

        return profile;
    }

    // public override Mesh GenerateMesh(Mesh mesh)
    // {

    //     return null;
    // }
}
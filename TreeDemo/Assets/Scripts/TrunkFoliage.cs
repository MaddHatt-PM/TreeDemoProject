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
            new Vector3(0f, offset),
            new Vector3(radius * (1f - lowerTaper), offset),
            // new Vector3(radius, height * 0.5f),
            new Vector3(radius * (1f - upperTaper), offset + height),
            new Vector3(0f, offset + height)
        };

        return profile;
    }

    public override bool IsInCollider(Vector3 point)
    {
        if (point.y >= offset && point.y <= offset + height)
        {
            // Convert point to polar in respect to 
            Vector3 polarPoint = MathUtilities.CartesianToPolar(point);
            float sectionRadius = Mathf.Lerp(
                radius * (1f - lowerTaper),
                radius * (1f - upperTaper), 
                (point.y - offset) / height
            );
            if (polarPoint.x <= sectionRadius)
                return true;
        }

        return false;
    }

    // public override Mesh GenerateMesh(Mesh mesh)
    // {

    //     return null;
    // }
}
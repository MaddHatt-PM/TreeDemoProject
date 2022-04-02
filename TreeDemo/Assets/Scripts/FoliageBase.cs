using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public abstract class FoliageBase
{
    public float radius;
    public float height;
    [Range(3, 32)] public int revolutionCount = 8;

    public abstract Vector3[] GenerateProfile();

    public virtual Mesh GenerateMesh(Mesh mesh)
    {
        if (mesh == null) mesh = new Mesh();
        mesh.Clear();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        Vector3[] strip = GenerateProfile();
        int stripCt = strip.Length - 2;
        for (int i = 1; i < strip.Length - 1; i++)
        {
            strip[i] = MathUtilities.CartesianToPolar(strip[i]);
            if (i != 0 && i != strip.Length - 1)
                verts.Add(strip[i]);
        }

        // Add and connect non pole vertices
        for (int revID = 1; revID < revolutionCount; revID++)
        {
            // Add to vertices
            for (int pID = 1; pID < strip.Length - 1; pID++)
            {
                strip[pID].z = (Mathf.PI * 2f) / revolutionCount * revID;
                verts.Add(MathUtilities.PolarToCartesian(strip[pID]));
            }

            // Add triangles
            int initPoint = (revID - 1) * stripCt;
            for (int pID = 0; pID < stripCt; pID += 2)
            {
                tris.Add(pID + initPoint + 0); tris.Add(pID + initPoint + 1); tris.Add(pID + initPoint + stripCt);
                tris.Add(pID + initPoint + 1); tris.Add(pID + initPoint + 1 + stripCt); tris.Add(pID + initPoint + stripCt);
            }
        }

        // Connect the start and end loops
        tris.Add(1); tris.Add(0); tris.Add(verts.Count - 2);
        tris.Add(verts.Count - 2); tris.Add(verts.Count - 1); tris.Add(1);

        // Add bottom pole and tris
        verts.Add(strip[0]);
        int btmVertID = verts.Count - 1;
        for (int i = 0; i < revolutionCount - 1; i++)
        {
            tris.Add(i);
            tris.Add(btmVertID);
            tris.Add(i + stripCt);
        }
        

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
}

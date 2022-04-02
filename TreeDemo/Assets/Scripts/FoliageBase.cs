using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public abstract class FoliageBase
{
    public float radius;
    public float height;
    public int revolutionCount = 8;

    public abstract Vector3[] GenerateProfile();

    public virtual Mesh GenerateMesh(Mesh mesh)
    {
        if (mesh == null) mesh = new Mesh();
        mesh.Clear();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        Vector3[] refProfile = GenerateProfile();
        Vector3[] pL = new Vector3[refProfile.Length];
        int polarID = 0;
        foreach (Vector3 p in refProfile)
        {
            // Polar Coordinate System: (r, y, θ)
            refProfile[polarID] = new Vector3(
                Mathf.Sqrt(p.x * p.x + p.z * p.z),
                p.y,
                Mathf.Atan(p.z / p.x)
            );
            polarID++;
        }
        Vector3[] pR = pL;



        for (int revID = 0; revID < revolutionCount; revID++)
        {
            // Add to vertices
            int startIndex = Mathf.Max(verts.Count - 1, 0);
            for (int pID = 0; pID < pR.Length; pID++)
            {
                verts.Add(pL[pID]);



                pR[pID].x += pR[pID].x * Mathf.Cos(360f / revolutionCount);
                pR[pID].z += pR[pID].z * Mathf.Sin(360f / revolutionCount);
                verts.Add(pR[pID]);
            }
            int endIndex = verts.Count;

            // Add to triangles
            for (int pID = startIndex; pID < endIndex - startIndex; pID += 4)
            {
                tris.Add(pID + 0); tris.Add(pID + 1); tris.Add(pID + 2);
                tris.Add(pID + 3); tris.Add(pID + 2); tris.Add(pID + 1);
            }

            // Prepare revolution step for next step
            pL = pR;
            // break;
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
}

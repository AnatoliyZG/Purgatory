using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MegaFiers;
using Sirenix.OdinInspector;

public class BuildFitter : MonoBehaviour
{
    public MeshFilter MapMesh;

    public Vector3 offset;

    public Size size;

    public enum Size : int
    {
        X1 = 0,
        X2 = 1,
        X3 = 2,
    }

    [Button]
    public void Modify()
    {
        StartCoroutine(ModifyCoroutine());
    }

    public IEnumerator ModifyCoroutine()
    {
        int sz = ((int)size + 2);

        Vector3[] pts = new Vector3[(int)Mathf.Pow(sz, sz)];
        Vector3[] vers = MapMesh.sharedMesh.vertices;

        for (int x = 0; x < sz; x++)
        {
            for (int y = 0; y < sz; y++)
            {
                for (int k = 0; k < sz; k++)
                {
                    pts[x * sz * sz + y + k * sz] = vers[vers.Length-1 - y * (int)Mathf.Sqrt(vers.Length) - x] / 2 + Vector3.up * k - offset;
                }
            }
        }

        gameObject.AddComponent<MegaModifyObject>();

        MegaFFD3x3x3 ffd = gameObject.AddComponent<MegaFFD3x3x3>();
        ffd.FitFFDToMesh();
        yield return new WaitForEndOfFrame();
        ffd.pt = pts;
    }
}

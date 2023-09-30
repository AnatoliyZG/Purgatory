using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    public int Seed = 0;

    public GameObject MapPlane;

    private MeshCollider meshCollider;

    private Mesh mesh;

    private List<Vector3> vertices;

    public int MaxWood;

    public int MaxRock;

    public int BaseRadius;

    public int MaxRadius;

    public GameObject Wood;

    public GameObject Rock;

    private void Start()
    {
        meshCollider = MapPlane.GetComponent<MeshCollider>();

        mesh = MapPlane.GetComponent<MeshFilter>().mesh;

        Generate();
    }

    public void Generate()
    {
        vertices = new List<Vector3>();

        mesh.GetVertices(vertices);

        MapOffset mapOffset = new MapOffset(vertices.Count * 2, Seed);

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] += new Vector3(mapOffset.NextF(), 0, mapOffset.NextF());
        }

        mesh.SetVertices(vertices);

        meshCollider.sharedMesh = mesh;

        SpawnResources();
    }

    public void SpawnResources()
    {
        Random random = new Random(Seed);

        int resourceCount = random.Next((int)(MaxWood * .8f), MaxWood);

        for (int i = 0; i < resourceCount; i++)
        {
            Instantiate(Wood, GetRndPosition(random), Quaternion.identity);
        }

        resourceCount = random.Next((int)(MaxRock * .8f), MaxRock);

        for (int i = 0; i < resourceCount; i++)
        {
            Instantiate(Rock, GetRndPosition(random), Quaternion.identity);
        }
    }

    private Vector3 GetRndPosition(Random random)
    {
        float angl = random.Next(0, 720) / 2f * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(angl), 0, Mathf.Cos(angl)) *  random.Next(BaseRadius, MaxRadius);
    }

    //private void SpawnResource(GameObject resource, )

    /*
    public Vector3 GetNearestVertex(RaycastHit hit, )
    {
                
       // begin:
            // Vector3 position = new Vector3((BaseRadius + random.Next(MaxBorder)) * mapOffset.GenerateSign(), 0.5f, (BaseRadius + mapOffset.random.Next(MaxBorderw)) * mapOffset.GenerateSign());

          //  if (Physics.CheckSphere(position, Radius, 1 << 9))
           //     goto begin;

            switch (mapOffset.random.Next(3))
            {
                case 1:
                    Instantiate(Wood, position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Rock, position, Quaternion.identity);
                    break;
            }
            
    }
    */

    private struct MapOffset
    {
        private byte[] offsets;
        private int current;

        public Random random;

        public MapOffset(int count, int seed)
        {
            offsets = new byte[count];
            current = 0;

            random = new Random(seed);
            random.NextBytes(offsets);
        }

        public byte Next() => offsets[current++];

        public float NextF() => (offsets[current++] - 127) / 1280f;

    }
}

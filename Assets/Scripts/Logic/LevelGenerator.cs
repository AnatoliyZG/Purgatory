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

        for (int i = 0; i < 720;)
        {
            float angl = i * Mathf.Deg2Rad;

            Instantiate(Wood, new Vector3(Mathf.Cos(angl + random.Next(-1, 2)), 0, Mathf.Sin(angl + random.Next(-1, 2))) * random.Next(BaseRadius, MaxRadius), Quaternion.identity);

            i += 360 / resourceCount;
        }

        resourceCount = random.Next((int)(MaxRock * .8f), MaxRock);

        for (int i = 0; i < 720;)
        {
            float angl = i * Mathf.Deg2Rad;

            Instantiate(Rock, new Vector3(Mathf.Cos(angl + random.Next(-1, 2)), 0, Mathf.Sin(angl + random.Next(-1, 2))) * random.Next(BaseRadius, MaxRadius), Quaternion.identity);

            i += 360 / resourceCount;
        }
}
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

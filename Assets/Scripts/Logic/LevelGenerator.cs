using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    public ResourcesPlace Wood;

    public ResourcesPlace Rock;

    private void Start()
    {
        meshCollider = MapPlane.GetComponent<MeshCollider>();

        mesh = MapPlane.GetComponent<MeshFilter>().mesh;

        Generate();

        AstarPath.active.Scan();
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

        Spawn(resourceCount, Wood);

        resourceCount = random.Next((int)(MaxRock * .8f), MaxRock);

        Spawn(resourceCount, Rock);

        void Spawn(int resourceCount, ResourcesPlace obj)
        {
            for (int i = 0; i < resourceCount; i++)
            {
                float angl = (360f / resourceCount) * i;

                int distance = random.Next(BaseRadius, MaxRadius);

                var pos = new Vector3Int((int)(Mathf.Cos(angl) * distance), 0, (int)(Mathf.Sin(angl) * distance));

                ref bool cell = ref GameManager.instance.GetCell(pos.x, pos.z);

                if (cell) continue;
                else cell = true;

                var place = Instantiate(obj,new Vector3(.5f,0,.5f) + pos, Quaternion.identity);

                place.gameObject.hideFlags = HideFlags.HideInHierarchy;

                ((IMapObject)place).Setup(pos.x, pos.z);
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Building : MonoBehaviour
{
    private Camera _camera;
    public GameObject Build;

    public Vector2Int Size = Vector2Int.one;
    public Vector2Int PlaneSize = new Vector2Int(10, 10);

    private Building[,] buildings;
    private Building unplacedBuilding;
    //private List<Building> _buildingList;

    private void Awake()
    {
        buildings = new Building[PlaneSize.x, PlaneSize.y];
    }
    private void Start()
    {
        //_camera = GetComponent<Camera>();
        _camera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    public void StartBuildForPlace(Building Build)
    {
        if (unplacedBuilding != null)
        {
            Destroy(unplacedBuilding);
        }
        unplacedBuilding = Instantiate(Build);
    }

    private void Update()
    {
        if (unplacedBuilding != null)
        {
            // Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 100, 1 << 6))
            //{
            //    Build.transform.position = hit.point;
            //}

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray, out float pos))
            {
                Vector3 worldPos = ray.GetPoint(pos);

                unplacedBuilding.transform.position = worldPos;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    unplacedBuilding = null;
                }
            }

        }
    }

     // Как с сеткой взаимодействовать? >_<
}

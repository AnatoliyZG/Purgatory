using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BuildingController : MonoBehaviour
{
    private Camera _camera;
    public GameObject Build;

    public Vector2Int PlaneSize = new Vector2Int(20, 20); //

    private Building[,] buildings;
    private Building unplacedBuilding;

    //private List<Building> _buildingList;

    private void Awake()
    {
        buildings = new Building[PlaneSize.x, PlaneSize.y];
        _camera = Camera.main;
    }

    public void StartBuildForPlace(Building Build)
    {
        if (unplacedBuilding != null)
        {
            Destroy(unplacedBuilding.gameObject);
        }
        unplacedBuilding = Instantiate(Build);
    }

    private void Update()
    {
        if (unplacedBuilding != null)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, 1 << 6))
            {
                Vector3 worldPos = hit.point;

                int x = Mathf.RoundToInt(worldPos.x);
                int y = Mathf.RoundToInt(worldPos.z);


                bool _available = true;

                if (x > PlaneSize.x - unplacedBuilding.Size.x) _available = false;
                if (y > PlaneSize.y - unplacedBuilding.Size.y) _available = false;

                if (_available && IsPlaceBuilding(x, y)) _available = false;

                unplacedBuilding.transform.position = new Vector3(x, 0, y);

                unplacedBuilding.SetStateColor(_available);

                if (_available && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlaceBuilding(x, y);
                }
            }
        }

        /*
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float pos))
        {
            Vector3 worldPos = ray.GetPoint(pos);

            unplacedBuilding.transform.position = worldPos;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                unplacedBuilding = null;
            }
        }
        */

    }

    private bool IsPlaceBuilding(int placeX, int placeY) // код дублируется, упростить?
    {
        for (int x = 0; x < unplacedBuilding.Size.x; x++)
        {
            for (int y = 0; y < unplacedBuilding.Size.y; y++)
            {
                if(buildings[placeX + x, placeY + y] != null) return true;
            }
        }
        return false;
    }
    private void PlaceBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < unplacedBuilding.Size.x; x++)
        {
            for (int y = 0; y < unplacedBuilding.Size.y; y++)
            {
                buildings[placeX + x, placeY + y] = unplacedBuilding;
            }
        }
        unplacedBuilding.SetNormalColor();
        unplacedBuilding = null;
    }
}

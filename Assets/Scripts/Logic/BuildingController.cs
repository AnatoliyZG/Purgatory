using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BuildingController : MonoBehaviour
{
    private Camera _camera;

    private Building[,] buildings;
    private Building unplacedBuilding;

    private void Start()
    {
        buildings = new Building[GameManager.instance.MapSize.x, GameManager.instance.MapSize.y];
        _camera = Camera.main;
    }

    [Button]
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

            if (Physics.Raycast(ray, out hit, 100, 1 << 6 | 1 << 8))
            {
                Vector3 worldPos = hit.point;

                int x = Mathf.RoundToInt(worldPos.x);
                int y = Mathf.RoundToInt(worldPos.z);

                //Debug.Log(x + " " + y);

                bool _available = IsPlaceBuilding(x, y);

                //Debug.Log(_available);

                unplacedBuilding.transform.position = new Vector3(x, 0, y);

                unplacedBuilding.SetStateColor(_available);

                if (_available && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlaceBuilding(x, y);
                }
            }

            float Rotate = Input.GetAxis("Mouse ScrollWheel");

            if(Rotate > 0)
            {
                unplacedBuilding.transform.localEulerAngles += Vector3.up * 90;
            }
            else if(Rotate < 0)
            {
                unplacedBuilding.transform.localEulerAngles -= Vector3.up * 90;
            }
        }
    }

    private bool IsPlaceBuilding(int placeX, int placeY)
    {
        /*
        placeX += buildings.GetLength(0) / 2;
        placeY += buildings.GetLength(1) / 2;

        if (placeX < 0 || placeX >= buildings.GetLength(0) + unplacedBuilding.Size.x 
            || placeY < 0  || placeY >= buildings.GetLength(1) + unplacedBuilding.Size.y)
            return false;

        for (int x = 0; x < unplacedBuilding.Size.x; x++)
        {
            for (int y = 0; y < unplacedBuilding.Size.y; y++)
            {
                if(buildings[placeX + x, placeY + y] != null) return false;
            }
        }
        */
        return true;
    }
    private void PlaceBuilding(int placeX, int placeY)
    {
        unplacedBuilding.OnPlace?.Invoke();

        /*
        placeX += buildings.GetLength(0) / 2;
        placeY += buildings.GetLength(1) / 2;

        for (int x = 0; x < unplacedBuilding.Size.x; x++)
        {
            for (int y = 0; y < unplacedBuilding.Size.y; y++)
            {
                buildings[placeX + x, placeY + y] = unplacedBuilding;
            }
        }
        */
        unplacedBuilding.SetNormalColor();
        unplacedBuilding = null;
    }
}

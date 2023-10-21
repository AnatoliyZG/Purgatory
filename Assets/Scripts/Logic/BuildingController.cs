using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using static GameManager;

public class BuildingController : MonoBehaviour
{
    private Camera _camera;

    private Building unplacedBuilding;

    private int buildAngle { get => _angle; set => _angle = value % 4; }

    private static int _angle;

    private void Start()
    {
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

            if (Physics.Raycast(ray, out hit, 100, 1 << 6))
            {
                Vector3 worldPos = hit.point;

                int x = Mathf.RoundToInt(worldPos.x);
                int y = Mathf.RoundToInt(worldPos.z);

                bool _available = BuildingAction(unplacedBuilding, x, y, IsCellEmpty);

                unplacedBuilding.transform.position = new Vector3(x, 0, y);

                unplacedBuilding.SetStateColor(_available);

                if (_available && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlaceBuilding(x, y, unplacedBuilding);
                    unplacedBuilding = null;
                    return;
                }

                buildAngle += Mathf.CeilToInt(Input.GetAxis("Mouse ScrollWheel"));

                unplacedBuilding.transform.localEulerAngles = Vector3.up * buildAngle * 90;
            }
        }
    }

    private static bool IsCellEmpty(int x, int y)
    {
        if (x > objectMap.GetLength(0) || y > objectMap.GetLength(1) || instance.GetCell(x, y) == true)
            return false;


        return true;
    }

    public static bool BuildingAction(Building building, int x, int y, Func<int, int, bool> func)
    {
        int mx = _angle == 0 || _angle == -1 || _angle == 3 ? 1 : -1;
        int my = _angle == 0 || _angle == 1 || _angle == -3 ? 1 : -1;

        for (int i = 0; i < building.Size; i++)
        {
            for (int j = 0; j < building.Size; j++)
            {
                if (!func(x + (i * mx), y + (j * my)))
                    return false;
            }
        }
        
        return true;
    }
    public static void PlaceBuilding(int placeX, int placeY, Building building)
    {
        building.OnPlace?.Invoke();

        ((IMapObject)building).Setup(placeX, placeY, _angle);

        BuildingAction(building, placeX, placeY, (x, y) => instance.GetCell(x, y) = true);

        instance.buildings.Add(building);

        building.SetNormalColor();

        building = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class CameraController : MonoBehaviour
{
    public static float MoveSpeed = 15;

    public static float ZoomSpeed;

    public Entity selectedEntity;

    public List<Unit> selectedEntities = new();

    private SoulPick _soulPick;

    public float MoveBorder = 40;

    public event Action<Entity> onFocused;

    private Camera _camera;

    public BuildingUI BuildingUI;

    private Vector3 position1;

    public event Action<Building> TapOnBuilding;

    private void Start()
    {
        _camera = Camera.main;
        _soulPick = GetComponent<SoulPick>();
        TapOnBuilding += BuildingUI.OpenUI;
    }

    private void Move(float point, float maxBorder, Vector3 direction)
    {
        if (point < MoveBorder)
        {
            transform.position -= direction * MoveSpeed * Time.deltaTime;
        }
        else if (point > maxBorder - MoveBorder)
        {
            transform.position += direction * MoveSpeed * Time.deltaTime;
        }
    }

    private void Zoom(float scroll)
    {
        transform.position += transform.forward * Time.deltaTime * ZoomSpeed * scroll;
    }

    private void Update()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
            return;

        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 7))
            {
                selectedEntity = hit.transform.GetComponent<Entity>();
                onFocused?.Invoke(selectedEntity);
            }
            else if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 8))
                BuildingUI.OpenUI(hit.collider.GetComponent<Building>());
            else 
            {
                position1 = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            }
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);

            Entity target = hit.transform.GetComponent<Entity>();
    
            if (selectedEntities.Count != 0)
            {
                if (target != null)
                {
                    foreach (var c in selectedEntities)
                        c.inputController.StartPath(target);
                }
                else
                {
                    foreach (var c in selectedEntities)
                        c.inputController.StartPath(hit.point);
                }
            }
            else if(selectedEntity != null && selectedEntity is Unit unit)
            {
                if (target != null)
                {
                    unit.inputController.StartPath(target);
                }
                else
                {
                    unit.inputController.StartPath(hit.point);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Vector3 position2 = _camera.ScreenToWorldPoint(Input.mousePosition);

            Collider[] hits = Physics.OverlapBox(Vector3.Lerp(position1, position2, 0.5f), new Vector3((position2.x - position1.x) * 0.5f, 100, (position2.z - position1.z) * 0.5f), Quaternion.identity, 1 << 7);

            if (hits == null)
                selectedEntities = null;
            else
                foreach (var c in hits)
                    selectedEntities.Add(c.GetComponent<Unit>());
        }
        Move(Input.mousePosition.x, Screen.width, Vector3.right);
        Move(Input.mousePosition.y, Screen.height, Vector3.forward);

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
}

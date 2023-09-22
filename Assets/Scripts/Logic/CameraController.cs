using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public static CameraController controller;

    public static float MoveSpeed = 15;

    public static float ZoomSpeed;

    public Entity selectedEntity;

    public List<Unit> selectedEntities = new();

    private SoulPick _soulPick;

    public float MoveBorder = 40;

    public event Action<Unit> onFocused;

    public event Action<Building> onFocusedBuilding;

    private Camera _camera;

    private Vector3 position1;

    private void Awake()
    {
        controller = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        _soulPick = GetComponent<SoulPick>();
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
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 8 | 1 << 7 | 1 << 6))
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 7:
                        selectedEntity = hit.transform.GetComponent<Entity>();
                        onFocused?.Invoke((Unit)selectedEntity);
                        break;
                    case 8:
                        selectedEntity = hit.transform.GetComponent<Entity>();
                        onFocusedBuilding?.Invoke(hit.collider.GetComponent<Building>());
                        break;
                    case 6:
                        position1 = hit.point;
                        break;
                };
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
            else if (selectedEntity != null && selectedEntity is Unit unit)
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
        else if (Input.GetKeyUp(KeyCode.Mouse0) && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 6))
        {
            Vector3 position2 = hit.point;
            Vector3 half = new Vector3(Mathf.Abs(position2.x - position1.x), 100, Mathf.Abs(position2.z - position1.z)) * .5f;

            Collider[] hits = Physics.OverlapBox(Vector3.Lerp(position1, position2, 0.5f), half, Quaternion.identity, 1 << 7);

            selectedEntities.Clear();

            foreach (var c in hits)
                selectedEntities.Add(c.GetComponent<Unit>());

            selectedEntities = selectedEntities.OrderBy(a => a.unitProperties.AttackRange).ToList();
        }
        Move(Input.mousePosition.x, Screen.width, Vector3.right);
        Move(Input.mousePosition.y, Screen.height, Vector3.forward);

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    public void OnDrawGizmos()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 6))
            {
                Vector3 position2 = hit.point;
                Vector3 half = new Vector3(Mathf.Abs(position2.x - position1.x), 0, Mathf.Abs(position2.z - position1.z));
                Gizmos.color = new Color(0, 1, 0, .2f);
                Gizmos.DrawCube(Vector3.Lerp(position1, position2, 0.5f), half);
            }
        }
    }
}

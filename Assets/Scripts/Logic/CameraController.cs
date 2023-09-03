using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public static float MoveSpeed = 15;

    public static float ZoomSpeed;

    public Entity selectedEntity;

    private SoulPick _soulPick;

    public float MoveBorder = 40;

    public event Action<Entity> onFocused;

    private Camera _camera;

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
        RaycastHit hit;

        if (Input.GetKey(KeyCode.Mouse0) && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 7 << 7))
        {
            selectedEntity = hit.transform.GetComponent<Entity>();
            onFocused?.Invoke(selectedEntity);
        }
        else if (Input.GetKey(KeyCode.Mouse1) && selectedEntity != null && selectedEntity is Unit unit)
        {
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);

            Entity target = hit.transform.GetComponent<Entity>();

            if (target != null)
            {
                unit.inputController.StartPath(target);
            }
            else
            {
                unit.inputController.StartPath(hit.point);
            }
        }

        Move(Input.mousePosition.x, Screen.width, Vector3.right);
        Move(Input.mousePosition.y, Screen.height, Vector3.forward);

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
}

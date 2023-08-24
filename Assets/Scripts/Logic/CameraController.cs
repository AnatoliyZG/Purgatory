using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    Camera _camera;

    public Entity selecteEntity;

    public float MoveSpeed;

    public float ZoomSpeed;

    public event Action<Entity> IsChosen;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void MoveX(float x)
    {
        if (x < 40)
            transform.position -= new Vector3(MoveSpeed / x * Time.deltaTime, 0);
        else if (x > Screen.width - 40)
            transform.position += new Vector3(MoveSpeed / (Screen.width - x) * Time.deltaTime, 0);
    }

    private void MoveZ(float z)
    {
        if (z < 20)
            transform.position -= new Vector3(0, 0, MoveSpeed / z * Time.deltaTime);
        else if (z > Screen.height - 20)
            transform.position += new Vector3(0, 0, MoveSpeed / (Screen.height - z) * Time.deltaTime);
    }

    private void Zoom(float scroll)
    {
        transform.position += transform.forward * Time.deltaTime * ZoomSpeed * scroll;
    }

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetKey(KeyCode.Mouse0) && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit,100, 7 << 7)) 
        {
            selecteEntity = hit.transform.GetComponent<Entity>();
            IsChosen?.Invoke(selecteEntity);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && selecteEntity != null && selecteEntity is Unit unit) 
        {
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);
            StartCoroutine(unit.inputController.StartPath(hit.point));
        }

        MoveX(Input.mousePosition.x);
        MoveZ(Input.mousePosition.y);
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
}

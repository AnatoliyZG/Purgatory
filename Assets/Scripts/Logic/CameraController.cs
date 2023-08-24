using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    Camera _camera;

    PlayerInput _playerInput;

    public float MoveSpeed;

    public float ZoomSpeed;

    public event Action IsChosen;

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
            _playerInput = hit.collider.GetComponent<PlayerInput>();
            IsChosen?.Invoke();
        }
        else if (Input.GetKey(KeyCode.Mouse0) && _playerInput != null) 
        {
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);
            StartCoroutine(_playerInput.StartPath(hit.point));
        }
        else if (Input.GetKey(KeyCode.Mouse1))
            _playerInput = null;

        MoveX(Input.mousePosition.x);
        MoveZ(Input.mousePosition.y);
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
}

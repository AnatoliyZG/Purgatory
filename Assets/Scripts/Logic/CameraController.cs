using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public static float MoveSpeed = 15;

    public static float ZoomSpeed;

    public Entity selectedEntity;

    public Entity selectedEnemy;

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
        else if(point > maxBorder - MoveBorder) {
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

        if (Input.GetKey(KeyCode.Mouse0) && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit,100, 7 << 7)) 
        {
            selectedEntity = hit.transform.GetComponent<Entity>();
            onFocused?.Invoke(selectedEntity);
        }
        else if (Input.GetKey(KeyCode.Mouse1) && selectedEntity != null && selectedEntity is Unit unit &&
                 FogOfWar.fog.GetPixel((int)unit.transform.position.x, (int)unit.transform.position.z) != Color.clear)
        {
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);
            unit.inputController.StartPath(hit.point);
            if (hit.transform.GetComponent<Entity>() != null && hit.transform.GetComponent<Entity>().type == Entity.EntityType.Monster)
            {
                selectedEnemy = hit.transform.GetComponent<Entity>();
                if (selectedEnemy != null) StartCoroutine(IDonKnowName(unit));
                else unit.inputController.StartPath(hit.point);
            }
        }

        Move(Input.mousePosition.x, Screen.width, Vector3.right);
        Move(Input.mousePosition.y, Screen.height, Vector3.forward);

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    public IEnumerator IDonKnowName(Unit unit)
    {
        while (true)
        {
            Vector3 pos1 = selectedEnemy.transform.position;
            Vector3 pos2 = unit.transform.position;
            if (Math.Sqrt((pos2.x - pos1.x) * (pos2.x - pos1.x) + (pos2.y - pos1.y) * (pos2.y - pos1.y)) > unit.properties.visionAttach)
            {
                unit.inputController.StartPath(selectedEnemy.transform.position);
                yield return new WaitForSeconds(1f);
            }
            else break;
        }
        unit.fighting.Attack(selectedEnemy);
    }
}

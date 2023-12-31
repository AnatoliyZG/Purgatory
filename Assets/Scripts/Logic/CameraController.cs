using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public static CameraController controller;

    public static float MoveSpeed = 15;

    public static float ZoomSpeed;

    public Entity selectedEntity;

    public List<Unit> selectedEntities = new();

    public float MoveBorder = 40;

    public event Action<Entity> onFocused;

    public event Action<List<Unit>> onFocusedAlly;

    public event Action onUnfocused;

    private List<Entity> showedUnits = new();

    private Camera _camera;

    private Vector3 BorderStart;

    private void Awake()
    {
        controller = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;

        onFocused += SetProjector;
        onFocusedAlly += SetProjectors;
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
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 8 | 1 << 7 | 1 << 6, QueryTriggerInteraction.Ignore))
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 7 or 8:
                        selectedEntity = hit.transform.GetComponent<Entity>();

                        if(selectedEntity is Unit unit)
                        {
                            selectedEntities = new List<Unit>() { unit };

                            onFocusedAlly?.Invoke(selectedEntities);
                        }
                        else
                        {
                            onFocused?.Invoke(selectedEntity);
                        }
                    
                        break;
                    default:
                        selectedEntities.Clear();
                        onFocusedAlly?.Invoke(selectedEntities);

                        onUnfocused?.Invoke();
                        selectedEntity = null;

                        BorderStart = hit.point;
                        break;
                };
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, ~0, QueryTriggerInteraction.Ignore);

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
                    EntityPosition position = new EntityPosition();

                    Vector3 middlePos = selectedEntities[0].transform.position;

                    foreach(var entity in selectedEntities)
                    {
                        middlePos = Vector3.Lerp(middlePos, entity.transform.position, .5f);
                    }

                    middlePos.y = hit.point.y;

                    Vector3 zDirect = (hit.point - middlePos).normalized;

                    Vector3 xDirect = new Vector3(zDirect.z, 0, zDirect.x);

                    Vector2Int offset = Vector2Int.zero;

                    foreach (var entity in selectedEntities)
                    {
                        entity.inputController.StartPath(hit.point + xDirect * offset.x + zDirect * offset.y);

                        offset = position.Next();
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 6, QueryTriggerInteraction.Ignore))
        {
            Vector3 position2 = hit.point;

            if (Vector3.Distance(BorderStart, position2) > .5f)
            {
                Vector3 half = new Vector3(Mathf.Abs(position2.x - BorderStart.x), 100, Mathf.Abs(position2.z - BorderStart.z)) * .5f;

                Collider[] hits = Physics.OverlapBox(Vector3.Lerp(BorderStart, position2, 0.5f), half, Quaternion.identity, 1 << 7);

                selectedEntities.Clear();

                for (int i = 0; i < Mathf.Min(6, hits.Length); i++)
                    selectedEntities.Add(hits[i].GetComponent<Unit>());

                onFocusedAlly?.Invoke(selectedEntities);
            }
            //selectedEntities = selectedEntities.OrderBy(a => (int)a.properties.rank).ToList();
        }
        Move(Input.mousePosition.x, Screen.width, Vector3.right);
        Move(Input.mousePosition.y, Screen.height, Vector3.forward);
    }

//#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 6, QueryTriggerInteraction.Ignore))
            {
                Vector3 position2 = hit.point;
                Vector3 half = new Vector3(Mathf.Abs(position2.x - BorderStart.x), 0, Mathf.Abs(position2.z - BorderStart.z));
                Gizmos.color = new Color(0, 1, 0, .2f);
                Gizmos.DrawCube(Vector3.Lerp(BorderStart, position2, 0.5f), half);
            }
        }
    }
//#endif

    private struct EntityPosition
    {
        public int x;

        public int y;

        public Vector2Int Next()
        {
            x++;

            if (x == 2)
                x = -1;
            else if (x == 0)
                y++;

            return new Vector2Int(x, y);
        }
    }

    public void SetProjector(Entity entity)
    {
        if (entity != null && showedUnits[0] != entity)
        {
            entity.Projector.SetActive(true);
            showedUnits[0].Projector.SetActive(false);

            showedUnits.Clear();
            showedUnits.Add(entity);
        }
        else if (entity == null)
        {
            showedUnits[0].Projector.SetActive(false);

            showedUnits.Clear();
        }
    }

    public void SetProjectors(List<Unit> units)
    {
        if (units.Count == 0)
        {
            if (showedUnits.Count > 0)
                foreach (var c in showedUnits)
                    c.Projector.SetActive(false);

            showedUnits.Clear();
        }
        else
        {
            if (showedUnits.Count == 0)
            {
                foreach (var c in units)
                    c.Projector.SetActive(true);
            }
            else
            {
                foreach (var c in units)
                    c.Projector.SetActive(true);

                foreach (var c in showedUnits)
                    if (!units.Contains(c))
                        c.Projector.SetActive(false);
            }

            showedUnits = units.ToList<Entity>();
        }
    }
}

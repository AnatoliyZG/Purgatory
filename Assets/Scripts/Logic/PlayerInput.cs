using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class PlayerInput : InputController
{
    Seeker _seeker;

    public override void Start()
    {
        base.Start();
        _seeker = GetComponent<Seeker>();
    }
  
    public IEnumerator StartPath(Vector3 point)
    {
        Path path = _seeker.StartPath(transform.position, point);
        yield return StartCoroutine(path.WaitForPath());
        List<Vector3> points = path.vectorPath;
        while (points.Count > 0) 
        {
            Vector3 _currentPoint = points[0];
            _currentPoint.y = transform.position.y;
            if (Vector3.Distance(transform.position, _currentPoint) < .2f)
                points.RemoveAt(0);
            Move((_currentPoint - transform.position).normalized);
            yield return new WaitForEndOfFrame();
        }
        Move(Vector3.zero);
    }

   /* private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) 
        {
            RaycastHit hit;
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);
            StartCoroutine(StartPath(hit.point));
        }
    }
   */
}

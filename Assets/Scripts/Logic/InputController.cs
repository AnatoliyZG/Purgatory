using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class InputController : EntityFeature<Unit>
{
    public float MoveSpeed => entity.unitProperties.MoveSpeed;
    private Rigidbody rig;
    private Transform transform;
    private Seeker seeker;

    private Coroutine movementCoroutine;

    public void Move(Vector3 direction)
    {
        direction = new Vector3(direction.x * MoveSpeed, rig.velocity.y, direction.z * MoveSpeed);
        rig.velocity = direction;
    }

    public InputController(Unit entity) : base(entity)
    {
        rig = entity.GetComponent<Rigidbody>();
        transform = entity.transform;
        seeker = entity.GetComponent<Seeker>();
    }

    public void StartPath(Vector3 point)
    {
        if(movementCoroutine != null)
        {
            entity.StopCoroutine(movementCoroutine);
        }

        movementCoroutine = entity.StartCoroutine(_pathCoroutine(point));
    }

    private IEnumerator _pathCoroutine(Vector3 point)
    {
        Path path = seeker.StartPath(transform.position, point);

        yield return entity.StartCoroutine(path.WaitForPath());
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
}

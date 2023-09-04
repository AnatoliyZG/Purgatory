using System.Collections;
using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class InputController : EntityFeature<Unit>
{
    public float moveSpeed => entity.unitProperties.MoveSpeed;

    public Action<Entity> OnNear;

    private Rigidbody rig;
    private Transform transform;
    private Seeker seeker;

    private Coroutine movementCoroutine;

    public void Move(Vector3 direction)
    {
        direction = new Vector3(direction.x * moveSpeed, rig.velocity.y, direction.z * moveSpeed);
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
        if (movementCoroutine != null)
        {
            entity.StopCoroutine(movementCoroutine);
        }

        movementCoroutine = entity.StartCoroutine(_pathCoroutine(point));
    }

    public void StartPath(Entity target)
    {
        if (movementCoroutine != null)
        {
            entity.StopCoroutine(movementCoroutine);
        }

        OnNear = (Build) =>
        {
            if (target is Building build)
            {
                //ЕСЛИ ЦЕЛЬ ЗДАНИЕ
            
            }else if(target.type == entity.type)
            {
                //ЕСЛИ ЦЕЛЬ СЮЗНИК
            }
            else
            {
                //ЕСЛИ ЦЕЛЬ ВРАГ
            }
        };

        movementCoroutine = entity.StartCoroutine(_trackCoroutine(target));

    }

    private IEnumerator _trackCoroutine(Entity target)
    {
        while (true)
        {
            Vector3 targetPos = target.transform.position;

            Path path = seeker.StartPath(transform.position, targetPos);

            yield return entity.StartCoroutine(path.WaitForPath());
            List<Vector3> points = path.vectorPath;

            if (target.type == entity.type)
            {
                while (Vector3.Distance(transform.position, target.transform.position) >= 2f)
                {
                    if (DirectMove(points[0]))
                    {
                        points.RemoveAt(0);
                    }

                    if (Vector3.Distance(targetPos, target.transform.position) > .5f) break;
                    

                    yield return new WaitForEndOfFrame();
                }

                //TODO:
                OnNear?.Invoke(target);
            }
            else
            {
                while (Vector3.Distance(transform.position, target.transform.position) >= entity.unitProperties.AttackRange)
                {
                    if (DirectMove(points[0]))
                    {
                        points.RemoveAt(0);
                    }

                    if(Vector3.Distance(targetPos, target.transform.position) > .5f) break;

                    yield return new WaitForEndOfFrame();
                }

                //TODO:
                OnNear?.Invoke(target);
            }

            yield return new WaitWhile(() => Vector3.Distance(targetPos, target.transform.position) < .5f);
        }

    }

    private IEnumerator _pathCoroutine(Vector3 point)
    {
        Path path = seeker.StartPath(transform.position, point);

        yield return entity.StartCoroutine(path.WaitForPath());
        List<Vector3> points = path.vectorPath;
        while (points.Count > 0)
        {
            if (DirectMove(points[0]))
            {
                points.RemoveAt(0);
            }

            yield return new WaitForEndOfFrame();
        }
        Move(Vector3.zero);
    }

    public bool DirectMove(Vector3 point)
    {
        point.y = transform.position.y;

        if (Vector3.Distance(transform.position, point) < .2f)
            return true;

        Move((point - transform.position).normalized);

        return false;
    }
}

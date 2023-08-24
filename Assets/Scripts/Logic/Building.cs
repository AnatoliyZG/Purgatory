using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Building : MonoBehaviour
{
    public Vector2Int Size = Vector2Int.one;

    public Renderer MainRenderer; // GetComponentInChildren<Renderer>() ?

    public void SetStateColor(bool available)
    {
        if (available) MainRenderer.material.color = Color.green;
        else MainRenderer.material.color = Color.red;

    }
    public void SetNormalColor()
    {
        MainRenderer.material.color = Color.white;
    }

    private void OnDrawGizmos() // Эти клетки видны только в редакторе
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0f, 1f, 0.22f, 1f);
                else Gizmos.color = new Color(0f, 0.48f, 0.1f, 1f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}

using Pathfinding.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FogOfWar : MonoBehaviour
{
    private Texture2D tex;

    private GameManager manager => GameManager.instance;

    void Start()
    {
        tex = new Texture2D(manager.width, manager.height);
        GetComponent<Renderer>().material.mainTexture = tex;
        tex.filterMode = FilterMode.Point;

        StartCoroutine(StartFogOfWar());
    }

    IEnumerator StartFogOfWar()
    {
        while (true)
        {
            Color[] colors = new Color[manager.width * manager.height];
            foreach (Entity item in manager.allies.Concat(manager.buildings))
            {
                Vector2Int pos = new Vector2Int(Mathf.Abs(Mathf.RoundToInt(item.transform.position.x) - manager.width / 2), Mathf.Abs(Mathf.RoundToInt(item.transform.position.z) - manager.height / 2));

                int vision = item.properties.visionDistance;
                float rSquared = vision * vision;

                for (int u = pos.x - vision; u < pos.x + vision + 1; u++)
                    for (int v = pos.y - vision; v < pos.y + vision + 1; v++)
                        if ((pos.x - u) * (pos.x - u) + (pos.y - v) * (pos.y - v) < rSquared)
                            colors[Mathf.Clamp(u, 0, manager.width-1) + Mathf.Clamp(v, 0, manager.height) * manager.width] = Color.white;
            }
            tex.SetPixels(colors);
            tex.Apply();
            yield return new WaitForSeconds(0.2f);
        }

    }
}
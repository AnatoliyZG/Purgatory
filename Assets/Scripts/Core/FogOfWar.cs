using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FogOfWar : MonoBehaviour
{
    public static Texture2D fog;

    private static GameManager manager => GameManager.instance;

    void Start()
    {
        fog = new Texture2D(manager.width, manager.height);
        GetComponent<Renderer>().material.mainTexture = fog;
        fog.filterMode = FilterMode.Point;

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

                int vision = item.properties.VisionDistance;
                float rSquared = vision * vision;

                for (int u = pos.x - vision; u < pos.x + vision + 1; u++)
                    for (int v = pos.y - vision; v < pos.y + vision + 1; v++)
                        if ((pos.x - u) * (pos.x - u) + (pos.y - v) * (pos.y - v) < rSquared)
                        {
                            colors[Mathf.Clamp(u, 0, manager.width - 1) + Mathf.Clamp(v, 0, manager.height-1) * manager.width] = Color.white;
                        }
            }

           
            for(int x = 0; x < manager.width; x++)
            {
                for(int y = 0; y < manager.height; y++)
                {
                    if(colors[x + y * manager.width] == Color.clear && fog.GetPixel(x,y).r == 1)
                    {
                        colors[x + y * manager.width] = new Color(1,1,1, .5f);
                    }
                }
            }
            

            fog.SetPixels(colors);
            fog.Apply();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public static bool IsPixelScouted(Vector3 position)
    {
        return IsPixelScouted(new Vector2Int(Mathf.Abs(Mathf.RoundToInt(position.x) - manager.width / 2), Mathf.Abs(Mathf.RoundToInt(position.z) - manager.height / 2)));
    }

    public static bool IsPixelScouted(Vector2 position)
    {
        return IsPixelScouted(new Vector2Int((int)position.x, (int)position.y));
    }

    public static bool IsPixelScouted(Vector2Int position)
    {
        return fog.GetPixel((int)position.x, (int)position.y).r > 0;
    }
}
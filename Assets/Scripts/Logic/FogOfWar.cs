using Pathfinding.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FogOfWar : MonoBehaviour
{
    public Texture2D tex;

    void Start()
    {
        tex = new Texture2D(GameManager.instance.width, GameManager.instance.height);
        GetComponent<Renderer>().material.mainTexture = tex;
        tex.filterMode = FilterMode.Point;

        StartCoroutine(StartFogOfWar());
    }

    IEnumerator StartFogOfWar()
    {

        while (true)
        {
            Color[] colors = new Color[GameManager.instance.width * GameManager.instance.height];
            foreach (Entity item in GameManager.instance.allies.Concat(GameManager.instance.buildings))
            {
                //tex.SetPixels(colors);
                Vector2Int Pos = new Vector2Int((int)item.transform.position.x, (int)item.transform.position.z);
                tex.SetPixel(Pos.x, Pos.x, Color.white);
            }

            tex.Apply();
            yield return new WaitForSeconds(0.2f);
        }

    }
}
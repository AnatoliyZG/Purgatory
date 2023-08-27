using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DemoController : MonoBehaviour
{
    public string[] scenes;

    private Scene loadedScene;

    private void OnGUI()
    {
        float w = Screen.width / 8;
        float f = w / 4;
        float y = 5;

        for (int i = 0; i < scenes.Length; i++)
        {
            if (GUI.Button(new Rect(5, y, w, f), scenes[i]))
            {
                if (loadedScene.isLoaded)
                {
                    string s = scenes[i];
                    SceneManager.UnloadSceneAsync(loadedScene).completed += (a) =>
                    {
                        Load(s);
                    };
                }
                else {
                    Load(scenes[i]);
                }

            }
            y += f + 5;
        }
    }

    public void Load(string s)
    {
        SceneManager.LoadScene(s, LoadSceneMode.Additive);
        loadedScene = SceneManager.GetSceneByName(s);
    }
}

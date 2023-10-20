using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //private Image image;

    private TextMeshProUGUI text;

    private void Start()
    {
        //image = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        while (true)
        {
            uint time = (uint)GameManager.instance._time;
            text.text = $"{(uint)(time / 60)}:{time % 60}";
            yield return new WaitForEndOfFrame();
        }
    }
}

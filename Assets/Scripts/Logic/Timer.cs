using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;

    private GameManager manager => GameManager.instance;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        while (true)
        {
            text.text = $"{manager.CurrentTime.Minutes}:{string.Format("{0:d2}", manager.CurrentTime.Seconds)}";

            yield return new WaitForSeconds(1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ResourceLabel : MonoBehaviour
{
    public ResourceType resourceType;

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        switch (resourceType)
        {
            case ResourceType.Wood:
                RefreshText(ResourceController.wood);
                ResourceController.controller.OnWoodChanged += RefreshText;
                return;
            case ResourceType.Rock:
                RefreshText(ResourceController.rock);
                ResourceController.controller.OnRockChanged += RefreshText;
                return;
            case ResourceType.Soul:
                RefreshText(GameManager.instance.allies.Count);
                GameManager.instance.OnSoulsChanged += (a, b) => RefreshText(GameManager.instance.allies.Count);
                return;
        }
    }

    private void RefreshText(int value)
    {
        text.text = value.ToString();
    }

    public enum ResourceType
    {
        Wood,
        Rock,
        Soul
    }
}

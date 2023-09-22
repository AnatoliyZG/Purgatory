using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ResourceController : MonoBehaviour 
{
    public static ResourceController controller;

    private float Wood = 0;

    private float Rock = 0;

    public Action<float> OnWoodChanged;

    public Action<float> OnRockChanged;

    public static float wood
    {
        get => controller?.Wood ?? 0;
        set
        {
            if (controller == null)
            {
                Debug.LogError("Resource manager is null");
                return;
            }

            controller.OnWoodChanged?.Invoke(value);

            controller.Wood = value;
        }
    }

    public static float rock
    {
        get => controller?.Rock ?? 0;
        set
        {
            if(controller == null)
            {
                Debug.LogError("Resource manager is null");
                return;
            }

            controller.OnRockChanged?.Invoke(value);

            controller.Rock = value;
        }
    }

    private void Awake()
    {
        controller = this;
    }
}

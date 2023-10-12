using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ResourceController : MonoBehaviour 
{
    public static ResourceController controller;

    private float Wood = 200;

    private float Rock = 80;

    private float money = 0;

    public Action<float> OnWoodChanged;

    public Action<float> OnRockChanged;

    public Action<float> OnMoneyChanged;

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

    public static float Money
    {
        get => controller?.money ?? 0;
        set
        {
            if (controller == null)
            {
                Debug.LogError("Resource manager is null");
                return;
            }

            controller.OnMoneyChanged?.Invoke(value);

            controller.money = value;
        }
    }

    [Sirenix.OdinInspector.Button]
    public void AddWood()
    {
        wood += 5;
    }

    private void Awake()
    {
        controller = this;
    }
}

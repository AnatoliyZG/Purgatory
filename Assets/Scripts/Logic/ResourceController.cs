using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ResourceController : MonoBehaviour 
{
    public static ResourceController controller;

    private int Wood = 200;

    private int Rock = 80;

    private int money = 0;

    public Action<int> OnWoodChanged;

    public Action<int> OnRockChanged;

    public Action<int> OnMoneyChanged;

    public static int wood
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

    public static int rock
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

    public static int Money
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

    private void Awake()
    {
        controller = this;
    }
}

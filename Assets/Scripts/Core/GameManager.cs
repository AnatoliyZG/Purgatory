using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DayPhase currentPhase = DayPhase.day;

    public event Action<DayPhase> dayChange;

    public uint Wood;

    public uint Stone;

    public List<Entity> enemies;

    public List<Entity> allies;

    public List<Entity> buildings;

    public Texture2D fog;

    private void Awake()
    {
        instance = this;
    }

    public void ChangePhase()
    {
        currentPhase = currentPhase == DayPhase.day ? DayPhase.night : DayPhase.day;
        dayChange?.Invoke(currentPhase);
    }

    public int height;

    public int width;
}

public enum DayPhase
{
    day,
    night,
}

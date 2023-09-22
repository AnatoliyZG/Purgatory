using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DayPhase currentPhase = DayPhase.day;

    public event Action<DayPhase> dayChange;

    public uint CurrentDay = 0;

    public uint Wood;

    public uint Stone;

    public List<Entity> enemies;

    public List<Entity> allies;

    public List<Entity> buildings;

    public int DayLength = 8;

    [PropertyRange(0, "DayLength")]
    public float Day;

    public float Night => DayLength - Day;

    private void Awake()
    {
        instance = this;

        StartCoroutine(DayPass());
    }

    public Transform Hero;

    public List<Entity> capitans;

    public List<Transform> UnitListUI;


    [Button]
    public void ChangePhase()
    {
        currentPhase = currentPhase == DayPhase.day ? DayPhase.night : DayPhase.day;

        if(currentPhase == DayPhase.day)
        {
            CurrentDay++;
        }

        dayChange?.Invoke(currentPhase);
    }

    public IEnumerator DayPass()
    {
        while (true)
        {

            yield return new WaitForSeconds(DayLength * Day * 60);
            ChangePhase();

            yield return new WaitForSeconds(DayLength * Night * 60);
            ChangePhase();
        }
    }

    public Transform UnitUI;
    
    public Transform canvas;
    public Transform capicansList;
    
    public int height;

    public int width;
}

public enum DayPhase
{
    day,
    night,
}

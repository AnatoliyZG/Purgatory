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

    public Transform Sun;

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

        StartCoroutine(DayPass(0));
        StartCoroutine(SunRotate(0));
    }


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

    public IEnumerator SunRotate(float time)
    {
        float _time = time;

        while (true)
        {
            _time += Time.deltaTime;

            float height = _time / (60 * DayLength);

            Sun.localEulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(360, 0, 0), height);

            if (height > 1)
                _time = 0;

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator DayPass(float time)
    {
        while (true)
        {
            if (time >= Day) 
            {
                yield return new WaitForSeconds((DayLength - time) * 60);
                ChangePhase();

                time = 0;

            }else 
            {
                yield return new WaitForSeconds((Day - time) * 60);
                ChangePhase();

                yield return new WaitForSeconds(Night * 60);
                ChangePhase();

                time = 0;
            }

            
        }
    }

    public void TimeSet(DayPhase dayPhase,uint currentDay,float time)  
    {
        StopAllCoroutines();

        currentPhase = dayPhase;
        CurrentDay = currentDay;

        StartCoroutine(SunRotate(time));
        StartCoroutine(DayPass(time));
    }

    public int height;

    public int width;
}

public enum DayPhase
{
    day,
    night,
}

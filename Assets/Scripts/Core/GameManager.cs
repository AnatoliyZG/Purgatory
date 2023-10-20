using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DayPhase currentPhase = DayPhase.day;

    public event Action<DayPhase> dayChange;

    public uint CurrentDay = 0;

    public Light Sun;

    public ObservableCollection<Entity> enemies;

    public ObservableCollection<Entity> allies;

    public ObservableCollection<Entity> buildings;

    public float _time;

    public uint MaxSoulsCount;

    public int DayLength = 8;

    [PropertyRange(0, "DayLength")]
    public float Day;

    public float Night => DayLength - Day;

    public Action<Entity, bool> OnSoulsChanged;

    public Vector2Int MapSize;

    private void Awake()
    {
        instance = this;

        StartCoroutine(DayPass(0));
        StartCoroutine(SunRotate(0));

        allies.CollectionChanged += (a, b) =>
        {
            switch (b.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnSoulsChanged(b.NewItems[0] as Entity, true);
                    return;
                case NotifyCollectionChangedAction.Remove:
                    OnSoulsChanged(b.OldItems[0] as Entity, false);
                    return;
            }
        };
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
        _time = time;

        while (true)
        {
            _time += Time.deltaTime;

            float height = _time / (60 * DayLength);

            Sun.transform.localEulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(360, 0, 0), height);

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
}

public enum DayPhase
{
    day,
    night,
}

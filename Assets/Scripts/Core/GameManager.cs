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

    public static bool[,] objectMap;

    public DayPhase currentPhase = DayPhase.day;

    public event Action<DayPhase> dayChange;

    public uint CurrentDay = 0;

    public Light Sun;

    public Entity Base;

    public ObservableCollection<Entity> enemies = new ObservableCollection<Entity>();

    public ObservableCollection<Entity> allies = new ObservableCollection<Entity>();

    public ObservableCollection<Entity> buildings = new ObservableCollection<Entity>();

    public uint MaxSoulsCount;

    public TimeSpan CurrentTime;

    public Vector3 SunRotationOffset;

    public int DayLength = 480;

    [PropertyRange(0, "DayLength")]
    public int Day;

    public float Night => DayLength - Day;

    public Action<Entity, bool> OnSoulsChanged;

    public Vector2Int MapSize;

    public ref bool GetCell(int x, int y) => ref objectMap[x + MapSize.x / 2, y + MapSize.y / 2];

    private void Awake()
    {
        instance = this;

        StartCoroutine(SunRotate());

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

        objectMap = new bool[MapSize.x, MapSize.y];
    }


    [Button]
    public void ChangePhase()
    {
        currentPhase = currentPhase == DayPhase.day ? DayPhase.night : DayPhase.day;

        if (currentPhase == DayPhase.day)
        {
            CurrentDay++;
        }

        dayChange?.Invoke(currentPhase);
    }

    public IEnumerator SunRotate()
    {
        while (true)
        {
            CurrentTime = new TimeSpan();

            while (CurrentTime.TotalSeconds < DayLength)
            {
                CurrentTime = CurrentTime.Add(TimeSpan.FromSeconds(Time.fixedDeltaTime));

                double height = CurrentTime.TotalSeconds / DayLength;

                Sun.transform.localEulerAngles = Vector3.Lerp(SunRotationOffset, SunRotationOffset + Vector3.right * 360, (float)height);

                if(CurrentTime.TotalSeconds == Day)
                {
                    ChangePhase();
                }

                yield return new WaitForFixedUpdate();
            }
            ChangePhase();
        }
    }

    private void OnDrawGizmos()
    {
        if (objectMap != null)

            for (int x = 0; x < objectMap.GetLength(0); x++)
            {
                for (int y = 0; y < objectMap.GetLength(1); y++)
                {
                    Vector3 start = new Vector3(x - MapSize.x / 2, 0, y - MapSize.y / 2);

                    Gizmos.color = objectMap[x, y] ? Color.red : Color.green;
                    Gizmos.DrawLine(start, start + new Vector3(1, 0, 1));
                }
            }
    }
}

public enum DayPhase
{
    day,
    night,
}

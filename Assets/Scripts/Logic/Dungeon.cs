using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dungeon : MonoBehaviour
{
    public Action OnEnter;
    public Action OnExit;

    public GameObject dungeon;

    public GameManager GameManager;

    public List<Unit> Participants = new();

    private void Start()
    {
        OnExit += DestroyDungeon;
        OnEnter += GameManager.StopAllCoroutines;
    }

    public void DestroyDungeon()
    {
        Destroy(GetComponent<GameObject>());
    }
}

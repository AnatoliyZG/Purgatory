using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dungeon : MonoBehaviour
{
    public Action OnEnter;
    public Action OnExit;


    public List<Unit> Participants = new();

    private GameManager gameManager => GameManager.instance;

    private void Start()
    {
        OnExit += DestroyDungeon;
        OnEnter += gameManager.StopAllCoroutines;
    }

    public void DestroyDungeon()
    {
        Destroy(gameObject);
    }
}

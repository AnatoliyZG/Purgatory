using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingUI : MonoBehaviour
{
    public GameObject BuildingCanvas;

    private GameObject _workersCards => BuildingCanvas.transform.GetChild(0).gameObject;

    public Image[] _content = new Image[5];

    private Building _currentBuilding;

    private void Start()
    {
        _content = _workersCards.GetComponentsInChildren<Image>(true);
    }

    public void Refresh()
    {
        for (int i = 0; i < _currentBuilding.workers.Count; i++)
        {
            int q = i;
            Unit unit = _currentBuilding.workers[q];

            _content[i].color = Color.black;

            _content[i].GetComponent<Button>().onClick.RemoveAllListeners();
            _content[i].GetComponent<Button>().onClick.AddListener(() => PickWorker(q, unit));
        }
    }

    public void OnEnter(Unit unit)
    {
        Refresh();
    }

    public void OpenUI(Building building)
    {
        _currentBuilding = building;

        Refresh();

        building.OnEnter += OnEnter;

        BuildingCanvas.SetActive(true);
    }

    public void CloseUI()
    {
        BuildingCanvas.SetActive(false);

        _currentBuilding.OnEnter -= OnEnter;

        foreach (var c in _content)
            c.color = Color.white;
    }

    public void PickWorker(int i, Unit worker) 
    {
        worker.gameObject.SetActive(true);

        _currentBuilding.workers.Remove(worker);

        _content[i].color = Color.white;

        _currentBuilding.OnQuit?.Invoke(worker);
    }
}

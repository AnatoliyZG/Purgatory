using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BuildingUI : MonoBehaviour
{
    public GameObject BuildingCanvas;

    public GameObject _actionCards;

    private GameObject _workersCards => BuildingCanvas.transform.GetChild(0).gameObject;

    private Image[] _content = new Image[5];

    private Building _currentBuilding;

    private Button[] actionButtons = new Button[4];

    private void Start()
    {
        actionButtons = _actionCards.GetComponentsInChildren<Button>(true);
        _content = _workersCards.GetComponentsInChildren<Image>(true);
        CameraController.controller.onFocusedBuilding += OpenUI;
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

        BuildingCanvas.gameObject.SetActive(true);

        for(int i = 0; i < building.entityActions.Count; i++)
        {
            int q = i;
            actionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            actionButtons[i].gameObject.SetActive(true);
            actionButtons[i].onClick.AddListener(() => building.entityActions[q].Execute(building));
            actionButtons[i].onClick.AddListener(() => CloseUI());
            actionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = building.entityActions[q].Description;
        }
    }

    public void CloseUI()
    {
        for (int i = 0; i < actionButtons.Length; i++)
            actionButtons[i].gameObject.SetActive(false);

        BuildingCanvas.gameObject.SetActive(false);

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

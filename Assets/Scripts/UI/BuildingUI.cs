using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BuildingUI : MonoBehaviour
{
    public GameObject Content;

    public Transform ActionContent;

    public Transform WorkersContent;

    public Button ActionPrefab;

    private Image[] _content = new Image[5];

    private Building _currentBuilding;

    private List<Button> _actionButton = new List<Button>();

    private void Start()
    {
        _content = WorkersContent.GetComponentsInChildren<Image>(true);

        CameraController.controller.onFocusedBuilding += OpenUI;
    }

    public void RefreshWorkers()
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

    public void Refresh(Unit unit)
    {
        RefreshWorkers();
    }

    public void OpenUI(Building building)
    {
        if (_currentBuilding == building)
            return;

        if(_currentBuilding != null) {
            CloseUI();
        }

        _currentBuilding = building;

        RefreshWorkers();

        building.OnQuit += Refresh;

        building.OnEnter += Refresh;

        Content.gameObject.SetActive(true);

        foreach(var action in building.entityActions)
        {
            Button button = Instantiate<Button>(ActionPrefab, ActionContent);

            button.onClick.AddListener(() => action.Execute(building));

            button.GetComponentInChildren<TextMeshProUGUI>().text = action.Description;

            _actionButton.Add(button);
        }
    }

    public void CloseUI()
    {
        foreach(var btn in _actionButton)
        {
            Destroy(btn.gameObject);
        }
        _actionButton.Clear();

        _currentBuilding.OnQuit -= Refresh;

        _currentBuilding.OnEnter -= Refresh;

        foreach (var c in _content)
            c.color = Color.white;

        Content.gameObject.SetActive(false);
    }

    public void PickWorker(int i, Unit worker) 
    {
        worker.gameObject.SetActive(true);

        _currentBuilding.workers.Remove(worker);

        _content[i].color = Color.white;

        _currentBuilding.OnQuit?.Invoke(worker);
    }
}

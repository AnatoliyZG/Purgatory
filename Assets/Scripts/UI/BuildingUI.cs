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

    private Building _currentBuilding;

    private List<Button> _actionButton = new List<Button>();

    private void Start()
    {
        CameraController.controller.onFocused += OpenUI;
        CameraController.controller.onUnfocused += CloseUI;
    }

    public void RefreshWorkers()
    {
        //TODO:

        for (int i = 0; i < _currentBuilding.workers.Count; i++)
        {

        }
    }

    public void Refresh(Unit unit)
    {
        RefreshWorkers();
    }

    public void OpenUI(Entity entity)
    {
        if (_currentBuilding == entity)
            return;

        if(_currentBuilding != null) {
            CloseUI();
        }

        if (entity is not Building building)
            return;

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
        if (!Content.activeSelf)
            return;

        foreach(var btn in _actionButton)
        {
            Destroy(btn.gameObject);
        }

        _actionButton.Clear();

        _currentBuilding.OnQuit -= Refresh;

        _currentBuilding.OnEnter -= Refresh;

        _currentBuilding = null;

        Content.SetActive(false);
    }

    public void PickWorker(int i) 
    {
        if (_currentBuilding.workers.Count <= i)
            return;

        Unit worker = _currentBuilding.workers[i];

        worker.gameObject.SetActive(true);

        _currentBuilding.workers.Remove(worker);

        _currentBuilding.OnQuit?.Invoke(worker);

        RefreshWorkers();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public GameObject CardPrefab;

    public Transform unitUI;

    private List<GameObject> UnitCards = new();

    public void SetActiveUI(List<Unit> units)
    { 
        if (units.Count == 0)
        {
            if (UnitCards.Count > 0)
                foreach (var c in UnitCards)
                    Destroy(c);

            UnitCards.Clear();
        }
        else
        {
            if (UnitCards.Count > 0) 
            {
                foreach (var c in UnitCards)
                    Destroy(c);
            }
            UnitCards.Clear();

            foreach (var c in units)
            {
                var b = Instantiate(CardPrefab, unitUI);

                UnitCards.Add(b);
            }
        }
    }

    private void Start()
    {
        CameraController.controller.onFocusedAlly += SetActiveUI;
    }

    public void SetCard(Unit unit,GameObject card)
    {

    }
}

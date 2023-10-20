using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternUI : MonoBehaviour
{
    public GameObject ConstructionUI;

    private void Start()
    {
        GameManager.instance.dayChange += SetContstructionUI;
    }

    public void SetContstructionUI(DayPhase dayPhase)
    {
        if (dayPhase == DayPhase.night)
            ConstructionUI.SetActive(false);
        else
            ConstructionUI.SetActive(true);
    }
}

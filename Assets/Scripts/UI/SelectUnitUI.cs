using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectUnitUI : MonoBehaviour
{
    public GameObject Content;
    public TextMeshProUGUI UnitHp;

    void Start()
    {
        //CameraController.controller.onFocused += Select;
    }

    public void Select(Entity entity)
    {
        if(entity is Unit unit)
        {
            Select(unit);
        }
    }

    public void Select(Unit unit)
    {
        Content.SetActive(true);
        UnitHp.text = unit.unitProperties.Hp.ToString();
    }

}

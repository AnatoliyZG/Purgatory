using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public Unit unit;
    public Transform nameUnit;
    public Image image;
    public Slider hpSlider;
    public Slider mpSlider;

    public UnitUI(Unit unitt)
    {
        unit = unitt;
        nameUnit.GetComponent<TextMeshProUGUI>().text = unit.name;
        image.sprite = unit.properties.Image;

        hpSlider.maxValue = unit.properties.MaxHp;
        hpSlider.value = unit.properties.Hp;
        
        mpSlider.maxValue = unit.properties.MaxMp;
        mpSlider.value = unit.properties.Mp;
    }
}

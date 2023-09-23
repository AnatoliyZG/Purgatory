using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public TextMeshProUGUI nameUnit;
    public Image image;
    public Slider hpSlider;
    public Slider mpSlider;

    public void Awake()
    {
        //Fighting.onGetHit
    }

    public void Setup(Unit unit)
    {
        nameUnit.text = unit.properties.Name;
        image.sprite = unit.properties.Image;

        hpSlider.maxValue = unit.properties.MaxHp;
        hpSlider.value = unit.properties.Hp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UnitUI : MonoBehaviour
{
    public TextMeshProUGUI nameUnit;
    public Image image;
    public Slider hpSlider;
    public Slider mpSlider;

    public event Action<Impact> onGetHit;
    public event Action<Impact> onAttack;

    public void SetupHero(Unit unit)
    {
        Setup(unit);

        mpSlider.maxValue = unit.properties.MaxMp;
        mpSlider.value = unit.properties.Mp;
    }
    public void Setup(Unit unit)
    {
        nameUnit.text = unit.properties.Name;
        image.sprite = unit.properties.Image;

        hpSlider.maxValue = unit.properties.MaxHp;
        hpSlider.value = unit.properties.Hp;
    }
}

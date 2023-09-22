using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitUI : MonoBehaviour
{
    public Unit unit;
    public Transform name;
    public Transform image;
    public Transform hpSlider;
    public Transform mpSlider;

    UnitUI()
    {
        GetComponent<TextMeshProUGUI>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectUnitUI : MonoBehaviour
{
    public GameObject Content;
    public TextMeshProUGUI UnitHp;

    // Start is called before the first frame update
    void Start()
    {
        CameraController.controller.onFocused += (a) =>
        {
            Content.SetActive(true);
            UnitHp.text = a.unitProperties.Hp.ToString();
        };
    }

}

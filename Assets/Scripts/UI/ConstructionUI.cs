using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionUI : MonoBehaviour
{
    public GameObject buildings;

    public void OpenOrCloseUI()
    {
        buildings.SetActive(!buildings.activeSelf);
    }
}

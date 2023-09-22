using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour
{
    public static GroupController instance;
    private GameManager manager;


    public void Awake()
    {
        instance = this;
        manager = GameManager.instance;
    }

    public void CreateHeroUI(Unit hero)
    {
        manager.UnitListUI[0] = Instantiate(manager.UnitUI, Vector3.zero, Quaternion.identity, manager.canvas);
        Transform newUnitUI = manager.UnitListUI[0];
        newUnitUI.GetComponent<RectTransform>().position = new Vector2(210, 995);
        newUnitUI.transform.localScale = Vector2.one;

    }

    public void CreateCapitanUI(Unit capitan)
    {
        manager.UnitListUI.Add(Instantiate(manager.UnitUI, Vector3.zero, Quaternion.identity, manager.capicansList));
        manager.capitans.Add(capitan);


    }
}

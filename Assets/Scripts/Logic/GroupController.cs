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
        manager.Hero = Instantiate(manager.UnitUI, Vector3.zero, Quaternion.identity, manager.canvas);
        manager.Hero.GetComponent<RectTransform>().position = new Vector2(210, 995);
        manager.Hero.transform.localScale = Vector2.one;

    }

    public void CreateCapitanUI(Unit capitan)
    {
        manager.UnitListUI.Add(Instantiate(manager.UnitUI, Vector3.zero, Quaternion.identity, manager.capicansList));
        manager.capitans.Add(capitan);


    }
}

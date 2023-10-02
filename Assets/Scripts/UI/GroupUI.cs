using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupUI : MonoBehaviour
{
    public UnitUI MainHero;

    public List<UnitUI> Captains = new();

    public UnitUI HeroPrefab;
    public UnitUI UnitPrefab;

    public Transform Content;

    void Awake()
    {
        GroupController.instance.onAddCaptain += CreateCapitanUI;
        GroupController.instance.onAddHero += CreateHeroUI;
    }

    public void CreateHeroUI(Unit hero)
    {
        MainHero = Instantiate(HeroPrefab, Vector3.zero, Quaternion.identity, Content);
        MainHero.transform.SetAsFirstSibling();
        MainHero.SetupHero(hero);
    }

    public void CreateCapitanUI(Unit capitan)
    {
        var un = Instantiate(UnitPrefab, Vector3.zero, Quaternion.identity, Content);
        un.Setup(capitan);
        Captains.Add(un);
    }

    //public void OnEnable(Unit unit)
    //{
    //    onGetHit += Setup();
    //}

    //public void OnDisable(Unit unit)
    //{
    //    onGetHit += Setup(unit);
    //}
}

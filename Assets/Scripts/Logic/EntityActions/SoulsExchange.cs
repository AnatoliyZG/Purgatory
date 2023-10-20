using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new soulsexchange action", menuName = "SoulsExchange Action", order = 51)]
public class SoulsExchange : EntityAction<Building>
{
    public override bool IsInterectable => true;

    public int WoodExchange;

    public int RockExchange;

    public GameObject ExchangeUI;

    public override string Description => $"Меняет душу на {WoodExchange} дерева или {RockExchange} камня";

    public override void Execute(Building obj)
    {
        ExchangeUI.SetActive(true);

        Button[] buttons = ExchangeUI.GetComponentsInChildren<Button>();
        foreach(var c in buttons)
            c.onClick.RemoveAllListeners();

        buttons[0].onClick.AddListener(() => PickRock(obj));
        buttons[1].onClick.AddListener(() => PickWood(obj));
        buttons[2].onClick.AddListener(() => ExchangeUI.SetActive(false));
    }

    public void PickWood(Building obj)
    {
        ResourceController.wood += WoodExchange * obj.workers.Count;
        ExchangeUI.SetActive(false);

        foreach (var c in obj.workers)
        {
            GameManager.instance.allies.Remove(c);
            obj.OnQuit?.Invoke(c);
            obj.workers.Remove(c);
        }
    }

    public void PickRock(Building obj)
    {
        ResourceController.rock += RockExchange * obj.workers.Count;
        ExchangeUI.SetActive(false);

        foreach(var c in obj.workers)
        {
            GameManager.instance.allies.Remove(c);
            obj.OnQuit?.Invoke(c);
            obj.workers.Remove(c);
        }
    }
}

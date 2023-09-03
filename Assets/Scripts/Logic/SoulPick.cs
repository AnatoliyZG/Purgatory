using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPick:MonoBehaviour
{
    public GameObject content => transform.GetChild(0).gameObject;

    public GameObject SoulCards;

    public Unit Soul;

    private Image[] cards;

    private List<UnitProperties> souls = new List<UnitProperties>();

    private List<(UnitProperties properties, int index)> selectedSouls = new();

    private int _cycle = 1;

    private uint _maxSouls = 2;

    private GameManager gameManager => GameManager.instance;


    public void Start()
    {
        gameManager.dayChange += OnDayChange;
        cards = SoulCards.GetComponentsInChildren<Image>(true);
    }

    private void OnDayChange(DayPhase phase)
    {
        if(phase == DayPhase.day && gameManager.CurrentDay % _cycle == 0)
            SoulGenerate(3);
    }

    private void SoulGenerate(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            int q = i;
            UnitProperties prop = new UnitProperties()
            {
                Damage = Random.Range(10, 15),
                Hp = Random.Range(10, 15)
            };

            souls.Add(prop);

            cards[i].GetComponent<Button>().onClick.RemoveAllListeners();
            cards[i].GetComponent<Button>().onClick.AddListener(() => SoulChoose(prop, q));
            cards[i].GetComponentInChildren<TextMeshProUGUI>(true).text = $"Damage={souls[i].Damage}\n Hp={souls[i].Hp}";
        }

        content.SetActive(true);
    }

    public void StopPickUp()
    {
        content.SetActive(false);

        foreach (var c in selectedSouls)
        {
            Unit unit = Instantiate(Soul, new Vector3(0, 1, 0), Quaternion.identity);
            unit.unitProperties = c.properties;
            souls.Remove(c.properties);

            gameManager.allies.Add(unit);
        }

        foreach (var c in souls)
        {
            gameManager.enemies.Add(new Unit()
            {
                unitProperties = c
            });
        }

        souls.Clear();
        selectedSouls.Clear();
    }

    public void SoulChoose(UnitProperties prop,int index)
    {         
        if (cards[index].color == Color.grey)
        {
            cards[index].color = Color.white;
            selectedSouls.Remove((prop,index));
        }
        else
        {
            cards[index].color = Color.grey;

            if (selectedSouls.Count == _maxSouls)
            {
                cards[selectedSouls[0].index].color = Color.white;
                selectedSouls[0] = (prop, index);
            }
            else
                selectedSouls.Add((prop, index));
        }
    }
}

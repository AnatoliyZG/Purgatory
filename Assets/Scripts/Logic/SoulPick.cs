using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class SoulPick : MonoBehaviour 
{
    public GameObject content => transform.GetChild(0).gameObject;

    public GameObject SoulCards;

    public Unit Soul;

    private UnityEngine.UI.Image[] cards;

    private List<UnitProperties> souls = new List<UnitProperties>();

    private List<(UnitProperties properties, int index)> selectedSouls = new();

    private int _cycle = 1;

    private uint _maxSouls = 2;

    private GameManager gameManager => GameManager.instance;

    public void Start()
    {
        gameManager.dayChange += OnDayChange;
        cards = SoulCards.GetComponentsInChildren<UnityEngine.UI.Image>(true);
        SoulGenerate(3);
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
                Hp = Random.Range(10, 15),
                MoveSpeed = 3
            };

            souls.Add(prop);

            cards[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            cards[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SoulChoose(prop, q));
            cards[i].GetComponentInChildren<TextMeshProUGUI>(true).text = $"Damage={souls[i].Damage}\n Hp={souls[i].Hp}";
        }

        content.SetActive(true);
    }

    public void StopPickUp()
    {
        content.SetActive(false);

        int i = 360 / selectedSouls.Count;
        int j = i;
        foreach (var c in selectedSouls)
        {
            Unit unit = Instantiate(Soul, new Vector3(Mathf.Cos(j), 0.5f, Mathf.Sin(j)) * 3, Quaternion.identity);
            unit.unitProperties = c.properties;
            souls.Remove(c.properties);

            gameManager.AddToAllies(unit);

            j += i;
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

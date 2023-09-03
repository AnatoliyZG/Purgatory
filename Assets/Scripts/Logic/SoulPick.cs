using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPick:MonoBehaviour
{
    public List<UnitProperties> souls;

    public Unit soul;

    private uint _maxPickUp = 2;

    public List<(UnitProperties properties, int index)> pickUp = new();

    public GameObject soulCards;

    private int _cycle = 1;

    public Image[] cards;

    public GameManager gameManager => GameManager.instance;

    public void Start()
    {
        gameManager.dayChange += SoulsCheck;
        cards = soulCards.transform.GetChild(0).GetComponentsInChildren<Image>(true);
    }

    private void SoulsCheck(DayPhase phase)
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

        soulCards.SetActive(true);
    }

    public void StopPickUp()
    {
        soulCards.SetActive(false);

        foreach(var c in cards)
            c.GetComponentInChildren<TextMeshProUGUI>(true).text = "";

        foreach (var c in pickUp)
        {
            Unit unit = Instantiate(soul, new Vector3(0, 1, 0), Quaternion.identity);
            unit.unitProperties = c.properties;
            souls.Remove(c.properties);

            gameManager.allies.Add(new Unit()
            {
                unitProperties = c.properties
            });
        }

        foreach (var c in souls)
            gameManager.enemies.Add(new Unit()
            {
                unitProperties = c
            });

        souls = null;
        pickUp = null;
    }

    public void SoulChoose(UnitProperties prop,int index)
    { 
        Debug.Log(index);
        
        if (cards[index].GetComponent<Image>().color == Color.grey)
        {
            cards[index].GetComponent<Image>().color = Color.white;
            pickUp.Remove((prop,index));
        }
        else
        {
            cards[index].GetComponent<Image>().color = Color.grey;

            if (pickUp.Count == _maxPickUp)
            {
                cards[pickUp[0].index].GetComponent<Image>().color = Color.white;
                pickUp[0] = (prop, index);
            }
            else
                pickUp.Add((prop, index));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPick:MonoBehaviour
{
    public List<UnitProperties> souls;

    public Unit soul;

    public GameObject soulCards;

    public Image[] cards;

    public GameManager gameManager => GameManager.instance;

    public void Start()
    {
        cards = soulCards.GetComponentsInChildren<Image>(true);

        //yield return new WaitForSeconds(5f);
        SoulGenerate(3);
        //yield return new WaitForSeconds(1f);
    }

    public void SoulPickUp(UnitProperties unitProperties, int index)
    {
        Unit unit = Instantiate(soul, new Vector3(0, 1, 0), Quaternion.identity);
        unit.unitProperties = unitProperties;
        souls.Remove(unitProperties);
        cards[index].gameObject.SetActive(false);
        gameManager.allies.Add(unit);
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
            cards[i].GetComponent<Button>().onClick.AddListener(() => SoulPickUp(prop, q));
            cards[i].GetComponentInChildren<TextMeshProUGUI>(true).text = $"Damage={souls[i].Damage}\n Hp={souls[i].Hp}";
        }
    }

    public void StopPickUp()
    {
        for (int i = 0; i < souls.Count; i++)
        {
            gameManager.enemies.Add(new Unit()
            {
                unitProperties = souls[i]
            });
        }
    }

    public void ShowCards()
    {
        soulCards.SetActive(!soulCards.activeSelf);
    }
}

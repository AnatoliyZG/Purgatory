using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPick:MonoBehaviour
{
    public List<UnitProperties> souls;

    public GameManager gameManager;

    public Unit soul;

    public GameObject soulCards;

    public Image[] cards;

    public IEnumerator SoulGenerate()
    {
        yield return new WaitForSeconds(5f);
        SoulGenerate(3);
    }

    private void Start()
    {
        cards = soulCards.GetComponentsInChildren<Image>(true);
        StartCoroutine(SoulGenerate());
    }

    public void SoulPickUp(UnitProperties unitProperties)
    {
        Unit unit = Instantiate(soul, new Vector3(0, 1, 0), Quaternion.identity);
        unit.unitProperties = unitProperties;
        souls.Remove(unitProperties);
        gameManager.allies.Add(unit);
    }

    private void SoulGenerate(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            souls.Add(new UnitProperties()
            {
                Damage = Random.Range(10, 15),
                Hp = Random.Range(10, 15)
            });
            cards[i].GetComponentInChildren<TextMeshPro>().text = $"Damage={souls[i].Damage}\n Hp={souls[i].Hp}";
        }
    }

    public void StopPickUp()
    {
        for (int i = 0; i < souls.Count; i++)
            gameManager.enemies.Add(new Unit()
            {
                unitProperties = souls[i]
            });
    }

    public void ShowCards()
    {
        soulCards.SetActive(!soulCards.activeSelf);
        int i = 0;
        foreach(var v in cards)
        {
            v.GetComponent<Button>().onClick.AddListener(() => SoulPickUp(souls[i]));
            i++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SoulPick : MonoBehaviour 
{
    public GameObject content => transform.GetChild(0).gameObject;

    public Transform SoulsParent;

    public Button SoulCard;

    public Unit Soul;

    public int PickPeriod = 1;

    public int SoulsCount = 3;

    public int MaxSoulsPick = 2;

    private SelectedSoul[] souls;

    private List<SelectedSoul> selectedSouls = new();

    private GameManager gameManager => GameManager.instance;

    public void Start()
    {
        SoulGenerate(SoulsCount);

        gameManager.dayChange += OnDayChange;
    }

    private void OnDayChange(DayPhase phase)
    {
        if (phase == DayPhase.day && gameManager.CurrentDay % PickPeriod == 0)
        {
            SoulGenerate(SoulsCount);
        }
    }

    private void SoulGenerate(int quantity)
    {
        foreach(var soul in souls)
        {
            Destroy(soul.button.gameObject);
        }

        souls = new SelectedSoul[quantity];

        selectedSouls.Clear();

        for (int i = 0; i < quantity; i++)
        {
            UnitProperties unitProperties = new UnitProperties()
            {
                Damage = Random.Range(10, 15),
                Hp = Random.Range(10, 15),
                MoveSpeed = 3
            };

            var button = Instantiate<Button>(SoulCard, SoulsParent);

            SelectedSoul soul = new SelectedSoul(button, unitProperties);

            souls[i] = soul;

            button.onClick.AddListener(() => SoulChoose(soul));

            button.GetComponentInChildren<TextMeshProUGUI>(true).text = $"Damage={unitProperties.Damage}\n Hp={unitProperties.Hp}";
        }

        content.SetActive(true);
    }

    public void StopPickUp()
    {
        int i = 360 / selectedSouls.Count;

        for(int j = 0; j < selectedSouls.Count; j++)
        {
            Unit unit = Instantiate(Soul, new Vector3(Mathf.Cos(j * i), 0, Mathf.Sin(j * i)) * 3f, Quaternion.identity);

            unit.unitProperties = selectedSouls[j].unitProperties;

            gameManager.allies.Add(unit);
        }

        //Оставшиеся души обращаем во врагов
        foreach (var c in souls.Except(selectedSouls))
        {
            gameManager.enemies.Add(new Unit()
            {
                unitProperties = c.unitProperties
            });
        }

        content.SetActive(false);
    }

    private void SoulChoose(SelectedSoul soul)
    {         
        if (selectedSouls.Contains(soul))
        {
            RemoveSoul(soul);
        }
        else
        {
            soul.image.color = Color.grey;

            if (selectedSouls.Count == MaxSoulsPick)
            {
                RemoveSoul(selectedSouls[0]);
            }

            selectedSouls.Add(soul);
        }

        void RemoveSoul(SelectedSoul soul)
        {
            soul.image.color = Color.white;
            selectedSouls.Remove(soul);
        }
    }

    private struct SelectedSoul
    {
        public Button button;

        public Image image => button.image;

        public UnitProperties unitProperties;

        public SelectedSoul(Button button, UnitProperties unitProperties)
        {
            this.button = button;
            this.unitProperties = unitProperties;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceController : MonoBehaviour 
{
    public TextMeshProUGUI WoodText;

    public TextMeshProUGUI RockText;

    private static ResourceController _controller;

    private static float _wood;

    private static float _rock;

    public static float Wood
    {
        get => _wood;
        set
        {
            if (_controller != null)
                _controller.WoodText.text = $"Wood = {value}";

            _wood = value;
        }
    }

    public static float Rock
    {
        get => _rock;
        set
        {
            if (_controller != null)
                _controller.RockText.text = $"Rock = {value}";

            _rock = value;
        }
    }

    private void Start()
    {
        _controller = this;

        Wood = 0;

        Rock = 0;
    }
}

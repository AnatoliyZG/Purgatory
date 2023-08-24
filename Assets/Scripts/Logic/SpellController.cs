using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public Spell curentSpell;

    void Start()
    {

    }

    void Update()
    {

        //Это потом перекинуть в PlayerInput
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
                curentSpell.Execute(hit.point);
        }
    }
}

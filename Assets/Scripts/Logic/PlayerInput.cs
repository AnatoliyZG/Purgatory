using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : InputController
{
    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        Move(new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")));
    }

    public void StartPath(Vector3 point)
    {

    }
}

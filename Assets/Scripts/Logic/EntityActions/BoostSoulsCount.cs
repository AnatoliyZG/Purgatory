using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new boostsoulscount action", menuName = "BoostSoulCount Action", order = 51)]
public class BoostSoulsCount : EntityAction<Building>
{
    public override bool IsInterectable => false;

    public override string Description => $"Повышает лимит душ на {AddLimit}";

    public uint AddLimit;

    public override void Execute(Building obj)
    {
        GameManager.instance.MaxSoulsCount += AddLimit;
    }

    public override void Cancel()
    {
        GameManager.instance.MaxSoulsCount -= AddLimit;
    }

}

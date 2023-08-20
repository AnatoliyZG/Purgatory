using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Entity Properties", order = 51)]
public class EntityProperties : ScriptableObject
{
    public string Name;
    public float Hp;
    public float MoveSpeed;

    public EntityProperties Clone()
    {
        return (EntityProperties)MemberwiseClone();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapObject
{
    public int x { get; set; }

    public int y { get; set; }

    public int angle { get; set; }

    public void Setup(int x, int y, int angle = 0)
    {
        this.x = x;
        this.y = y;
        this.angle = angle;
    }

    public void Dispose();
}

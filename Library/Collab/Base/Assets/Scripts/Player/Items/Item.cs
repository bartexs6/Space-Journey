using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    private short amount;
    public string name;
    public string description;
    public Sprite itemIcon;

    public short Amount
    {
        get { return amount; }
        set { amount = (value <= 1000) ? value : amount = 1000;  }
    }
}

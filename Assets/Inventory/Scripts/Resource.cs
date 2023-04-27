using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public Item Item;
    public int Amount;

    public Resource(Item item, int amount = 1)
    {
        Item = item;
        Amount = amount;
    }
}

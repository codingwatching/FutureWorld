using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType { 
        Stick,
        Stone,
        Axe,
        CampFire,
    }

    public ItemType itemType;
    public int amount;
    public int maxStack;

    public Item(ItemType type, int itemAmount)
    {
        itemType = type;
        amount = itemAmount;

        switch (type)
        {
            case ItemType.Axe:
                maxStack = 1;
                break;
            case ItemType.Stick:
                maxStack = 10;
                break;
            case ItemType.Stone:
                maxStack = 10;
                break;
            case ItemType.CampFire:
                maxStack = 1;
                break;
        }
    }
}

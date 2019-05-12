using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public GameItem item;
    public int quantity;

    public InventoryItem(GameItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public override bool Equals(object obj)
    {
        var other = obj as InventoryItem;

        if (item == null) return false;
        return this.item.itemName.Equals(other.item.itemName);
    }
    public override int GetHashCode()
    {
        return this.item.itemName.Length;
    }
}

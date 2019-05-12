using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int goldAmmount;
    public List<InventoryItem> inventory;
    public List<InventoryItem> keyInventory;

    //Devuelve la nueva cantidad de newItem que hay
    public int addItem(InventoryItem newItem)
    {
        if (inventory.Contains(newItem))
        {
            InventoryItem existingItem = inventory[inventory.IndexOf(newItem)];
            existingItem.quantity += newItem.quantity;
            return existingItem.quantity;
        }
        else
        {
            this.inventory.Add(newItem);
            return newItem.quantity;
        }
    }

    public int removeItem(InventoryItem newItem)
    {
        InventoryItem existingItem = inventory[inventory.IndexOf(newItem)];
        existingItem.quantity -= newItem.quantity;
        if (existingItem.quantity == 0) inventory.Remove(existingItem);
        return existingItem.quantity;

    }

}

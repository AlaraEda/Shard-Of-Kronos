using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Object", menuName = "Inventory/Items/Key")]
public class ItemKey : Item
{
    [Header("Item specific variables")]
    public int key;
    private void Awake()
    {
        type = ItemType.Key;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Object", menuName = "Inventory/Items/QuestObject")]
public class ItemQuestObject : Item
{

    private void Awake()
    {
        type = ItemType.PuzzlePiece;
    }
}

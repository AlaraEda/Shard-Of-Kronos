/*
Make it easy to create new item sorts in your inventory. 
Every new item sort consists of a unique Item ID, General Information & a description.
General Description could be what the name of the particular item type is 
and with what sprite it is shown in the UI. 
*/

using UnityEngine;

/// <summary>
/// Types of Items possible in Inventory.
/// </summary>
public enum ItemType {
    Food,
    Equipment,
    Arrow,
    ShrineActivationKey,
    Key,
    PuzzlePiece
}

/// <summary>
/// What an item is made off. 
/// </summary>
public abstract class Item : ScriptableObject{
    [Header("Make sure no other item already uses this unique ID!")]
    public int uniqueItemId;

    [Header("General Item Information")]
    public string itemName;
    public ItemType type;
    public GameObject uiElementPrefab;
    public Sprite image;
    
    [TextArea(15,10)]
    public string itemDescription;
    
}


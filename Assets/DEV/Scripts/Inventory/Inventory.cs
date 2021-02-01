/*
 * Inventory.cs is the main class for the player Inventory. It keeps track of all the InventorySlots filled with x item and y amount.
 * The Inventory class also implements the function to add, remove and check for items.
 * Each time an Inventory Slot is created, or updated, this class activates a function to update the inventory UI elements.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using DEV.Scripts;
using DEV.Scripts.Inventory;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Settings;

namespace DEV.Scripts.Inventory
{


    /// <summary>
    /// The class inventory slot keeps track of all information regarding an Item in a slot of the players inventory. It houses the
    /// prefab that should be spawned and the actual instance of the spawned prefab, so that scripts can later change information in the UI
    /// during runtime.
    /// </summary> 
    [Serializable]
    public class InventorySlot
    {
        public int id;

        // Item settings
        public Item item;
        public int amount;
        public int maxItemsPerStack;

        // UI
        public GameObject uiPrefab;
        //public GameObject instantiatedUI;
        //public InventoryUI inventoryUIScript;

        /// <summary>
        /// Constructor that initializes an Inventory slot. Called when an Item is added to the inventory, when there is no inventory slot available for the items unique ID. 
        /// </summary>
        /// <param name="i">Item object to make the inventory slot for</param>
        /// <param name="a">Amount of items to add to the inventory slot.</param>
        /// <param name="maxStackSize">Maximum amount of items that this slot can contain</param>
        /// <param name="ui">Prefab element for UI</param>
        public InventorySlot(int ID, Item i, int a, GameObject ui)
        {
            id = ID;
            item = i;
            amount = a;

            uiPrefab = ui;
        }

        /// <summary>
        /// Adds a given amount to this inventory slot
        /// </summary>
        /// <param name="value">Amount of items to add to the inventory slot</param>
        public void AddAmount(int value)
        {
            amount += value;
            Debug.Log("Added " + value + " to the corresponding Inventory slot  of item: " + item.itemName);
        }

        /// <summary>
        /// Removes a given amount of items from this inventory slot
        /// </summary>
        /// <param name="value">Amount of items to remove from the inventory slot</param>
        /// <returns>Returns true when the new amount is below zero (so the whole inventory slot can be removed)</returns>
        public bool RemoveAmount(int value)
        {
            if (amount - value <= 0)
            {
                RemoveItemSlot();
                return true;
            }

            amount -= value;
            return false;
        }

        /// <summary>
        /// Removes UI elements that links with this inventory slot. The entry is dictionary is removed in the Inventory class
        /// in the method 'RemoveItemFromInventory'.
        /// </summary>
        public void RemoveItemSlot()
        {
        }
    }

    ///<summary>
    /// Create an Asset Menu within Unity to make scriptable objects from items.
    [CreateAssetMenu(fileName = "NewMainInventoryObject", menuName = "Inventory/InventoryMain")]
    public class Inventory : ScriptableObject, ISerializationCallbackReceiver
    {
        // Dictionary that keeps track of items in inventory.
        // int for unique item ID, and Inventory Slot holds ItemObject and amount (see class at top of script).
        public Dictionary<int, InventorySlot> slots = new Dictionary<int, InventorySlot>();
        public List<InventorySlot> inventoryList;

        private bool isListeningForSceneChange;

        /**
          * Event handlers for inventory
        */
        public delegate void ItemAddedHandler(object sender, ItemAddedEventArgs args);

        public delegate void ItemRemovedHandler(object sender, ItemRemovedEventArgs args);

        public event ItemAddedHandler OnItemAdded;
        public event ItemRemovedHandler OnItemRemoved;

        private void Awake()
        {
            SceneManager.sceneUnloaded += arg0 =>
            {
                if (OnItemAdded != null)
                {
                    foreach (var eventHandle in OnItemAdded.GetInvocationList())
                    {
                        OnItemAdded -= (ItemAddedHandler)eventHandle;
                    }
                }
            };
        }

        /// <summary>
        /// Add item to the InventorySlot that corresponds with the 'Item' parameter unique ID that is passed in.
        /// If the Inventory Slot for that item does not exist, this function will create one.
        /// </summary>
        /// <param name="item">Item to add to inventory</param>
        /// <param name="amount">The amount to add.</param>
        public void AddItemToInventory(Item item, int amount)
        {
            var inventoryAndItemSettings = SettingsEditor.Instance.inventorySettings;
            bool hasItem = false;

            foreach (var slot in slots)
            {
                if (slot.Key == item.uniqueItemId)
                {
                    //Debug.Log("Player already had a slot with this item, adding the amount now.");
                    slot.Value.AddAmount(amount);
                    hasItem = true;
                    break;
                }
            }

            if (!hasItem)
            {
                //Debug.Log("Player does not have item already, make new Inventory Slot");
                slots.Add(item.uniqueItemId,
                    new InventorySlot(item.uniqueItemId, item, amount,
                        item.uiElementPrefab));

                //Convert the current dictionary values to a list, so that the json formatter can save and read it
                inventoryList = slots.Values.ToList();
            }
            
            OnItemAdded?.Invoke(this, new ItemAddedEventArgs
            {
                Item = item,
                Amount = amount
            });
        }


        /// <summary>
        /// Remove item from the InventorySlot that corresponds with the 'Item' parameter unique ID that is passed in.
        /// If the item amount gets to 0 or lower, the corresponding Inventory Slot will be removed. 
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="amount">Amount to remove</param>
        public void RemoveItemFromInventory(Item item, int amount)
        {
            foreach (var slot in slots)
            {
                if (slot.Key == item.uniqueItemId)
                {
                    //Debug.Log("Player does have item slot, trying to remove "  + amount + " items now.");
                    bool doRemoveSlot = slot.Value.RemoveAmount(amount);
                    var eventArgs = new ItemRemovedEventArgs
                    {
                        Item = item,
                        Amount = amount,
                        AmountLeft = doRemoveSlot ? 0 : slot.Value.amount
                    };
                    if (doRemoveSlot)
                    {
                        //Debug.Log("Amount is under 0, removing whole InventorySlot");
                        slots.Remove(item.uniqueItemId);
                    }
                    OnItemRemoved?.Invoke(this, eventArgs);
                    break;
                }
            }
        }

        /// <summary>
        /// Retrieves the Inventory Slot object that corresponds to the given item.
        /// </summary>
        /// <param name="item">Item to retrieve Inventory Slot for.</param>
        /// <returns>Returns the Inventory Slot that corresponds with the item parameter.</returns>
        public InventorySlot GetInventorySlotByItem(Item item)
        {
            foreach (var slot in slots)
            {
                if (slot.Key == item.uniqueItemId)
                {
                    return slot.Value;
                }
            }

            return null;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
        }
    }
}
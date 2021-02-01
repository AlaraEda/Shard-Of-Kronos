using System.Collections;
using DEV.Scripts.Input;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class InteractableAddToInventory : Interactable
    {
        public Item item;
        private Inventory.Inventory inventory;


        protected override void Awake()
        {
            base.Awake();
            inventory = Resources.Load<Inventory.Inventory>("PlayerInventory");
        }

        public override void OnPlayerInteract()
        {
            inventory.AddItemToInventory(item, 1);
            Destroy(gameObject);
        }
    }
}
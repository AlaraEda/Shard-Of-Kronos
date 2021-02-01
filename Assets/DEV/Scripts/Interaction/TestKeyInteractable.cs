using DEV.Scripts.Input;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class TestKeyInteractable : Interactable
    {
        //Settings

        private Inventory.Inventory inventory;

        public Item item;

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
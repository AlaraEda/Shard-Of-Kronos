    using DEV.Scripts.Input;
    using DEV.Scripts.Managers;
    using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class TestKeyRemoveInteractable : Interactable
    {
        //Settings
        private GameObjectRunTime playerSet;
        private Inventory.Inventory inventory;
        
        public Item item;
        
        protected override void Awake()
        {
            base.Awake();
            //playerSet = Resources.Load<GameObjectRunTime>("ObjectRunTime");
           // inventory = SceneContext.Instance.inventory;

        }

        public override void OnPlayerInteract()
        {
            inventory.RemoveItemFromInventory(item, 1 );
            //Destroy(gameObject);
        }
    }
    
    
}
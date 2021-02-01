using DEV.Scripts.Input;
using DEV.Scripts.Interaction;
using DEV.Scripts.Managers;

namespace DEV.Scripts.Interaction
{
    public class RemoveFromInventoryInteractable : Interactable
    {
        public Item item;

        public override string InteractionVerb => $"Press [E] to remove {item.name} from inventory!";
        public override void OnPlayerInteract()
        {
            SceneContext.Instance.playerInventory.RemoveItemFromInventory(item, 1);
            SceneContext.Instance.hudContext.DisplayHint($"You have removed {item.name} from your inventory!");
            Destroy(GameObject);
        }
    }
}
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine.SceneManagement;

namespace DEV.Scripts.Interaction
{
    public class ActivateEmitterPartInteractable : Interactable
    {
        public ActivateEmitterInteractable activator;

        public override void OnPlayerInteract()
        {
            SceneContext.Instance.hudContext.DisplayHint("You have picked up a part that belongs to a light activator switch!");
            activator.playerHasSwitchPart = true;
            Destroy(gameObject);
        }
    }
}
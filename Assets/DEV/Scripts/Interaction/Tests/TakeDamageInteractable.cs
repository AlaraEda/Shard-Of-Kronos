using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class TakeDamageInteractable : Interactable
    {
        public float damageAmount = 20;
        
        public override void OnPlayerInteract()
        {
            SceneContext.Instance.playerManager.PlayerHealth -= damageAmount;
            SceneContext.Instance.hudContext.DisplayHint($"You have taken {damageAmount} damage!");
        }
    }
}
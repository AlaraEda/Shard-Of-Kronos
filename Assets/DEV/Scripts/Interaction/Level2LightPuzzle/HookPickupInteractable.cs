using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class HookPickupInteractable : Interactable
    {
        public GameObject hookPoint;

        public override void OnPlayerInteract()
        {
            hookPoint.layer = 14;
            SceneContext.Instance.hudContext.DisplayHint("You have picked up a box of special hooks. You can now fire your arrow on certain hook points and use it as a grapple by holding F!");
            Destroy(gameObject);
        }
    }
}
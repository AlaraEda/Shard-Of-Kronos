using DEV.Scripts.Input;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class KeyInteractable : Interactable
    {
        public override void OnPlayerInteract()
        {
            Debug.Log("Player has picked up a key!");
            Destroy(gameObject);
        }
    }
}
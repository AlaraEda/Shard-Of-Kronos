using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.UI;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class HintInteractable : Interactable
    {
        private bool first = false;

        public override void OnPlayerInteract()
        {
            string txt = first
                ? "This is a sample hit message and will disappear after 5 seconds!This is a sample hit message and will disappear after 5 seconds!This is a sample hit message and will disappear after 5 seconds!"
                : "This is a sample hit message and will disappear after 5 seconds!";

            
            SceneContext
                .Instance
                .hudContext
                .DisplayHint(txt);

            

            first = true;
        }
    }
}
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.Puzzles.LightReflector;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class ActivateEmitterInteractable : Interactable
    {
        public LightReflector reflector;
        public bool playerHasSwitchPart;
        public GameObject orb;

        public override void OnPlayerInteract()
        {
            if (!playerHasSwitchPart)
            {
                SceneContext.Instance.hudContext.DisplayHint("A special switch part is needed in order to use this!");
                return;
            }
            else
            {
                orb.SetActive(true);
            }
            
            // if (reflector.LightSources.Contains(reflector))
            // {
            //    // reflector.LightSources.Remove(reflector);
            // }
            // else
            // {
            //    
            //     //reflector.LightSources.Add(reflector);
            //    
            // }
        }
    }
}
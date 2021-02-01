using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DEV.Scripts.UI
{
    public class HudContext : MonoBehaviour
    {
        [SerializeField] private GameObject hintPrefab;
        [SerializeField] private TMP_Text interactionText;

        private GameObject prevHint;
        
        /// <summary>
        /// Display a UI hint notification on the screen
        /// </summary>
        /// <param name="text">The text that needs to be displayed</param>
        /// <param name="duration">(Optional) the duration that the needs to be displayed for</param>
        public void DisplayHint(string text, int duration = 5)
        {
            if (prevHint != null) Destroy(prevHint);
            prevHint = Instantiate(hintPrefab, transform);
            prevHint.GetComponent<Hint>().Activate(text, duration);
        }
        
        /// <summary>
        /// Control the interaction UI text
        /// </summary>
        public string InteractionText
        {
            get => interactionText.text;
            set => interactionText.text = value;
        }

    }
}
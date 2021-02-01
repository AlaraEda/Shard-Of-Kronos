using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DEV.Scripts.UI
{
    public class Hint : MonoBehaviour
    {
        public TMP_Text hintText;
        public Image background;
        public float fadeSpeed;

        private float maxBgAlpha;
        private int maxDuration = 0;
        
        public void Activate(string text, int duration)
        {
            hintText.text = text;
            maxDuration = duration;
            StartCoroutine(nameof(PlayEffect));
        }

        private void Awake()
        {
            maxBgAlpha = background.color.a;
            hintText.color = new Color(hintText.color.r, hintText.color.g, hintText.color.b, 0);
            background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        }

        private IEnumerator PlayEffect()
        {
            bool fadeIn = true;
            while (fadeIn)
            {
                if (hintText.color.a <= 1)
                {
                    var curColor = hintText.color;
                    curColor.a += fadeSpeed * Time.deltaTime;
                    hintText.color = curColor;
                }

                if (background.color.a <= maxBgAlpha)
                {
                    var curColor = background.color;
                    curColor.a += fadeSpeed * Time.deltaTime;
                    background.color = curColor;
                }

                if (hintText.color.a >= 1 && background.color.a >= maxBgAlpha) fadeIn = false;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(maxDuration);
            bool fadeOut = true;
            while (fadeOut)
            {
                if (hintText.color.a > 0)
                {
                    var curColor = hintText.color;
                    curColor.a -= fadeSpeed * Time.deltaTime;
                    hintText.color = curColor;
                }

                if (background.color.a > 0)
                {
                    var curColor = background.color;
                    curColor.a -= fadeSpeed * Time.deltaTime;
                    background.color = curColor;
                }

                if (hintText.color.a <= 0 && background.color.a <= 0) fadeOut = false;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
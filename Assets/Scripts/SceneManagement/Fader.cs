using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        private void Start() {
            
            //StartCoroutine(FadeOutIn());
        }
        public void FadeOutImmediately()
        {
            canvasGroup.alpha = 1;
        }
        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(3);
            yield return FadeIn(1.5f);
        }
        public IEnumerator FadeOut(float time)
        {
         while(canvasGroup.alpha != 1)
         {
             canvasGroup.alpha += Time.deltaTime/time;
             yield return null;
         }

        }
        public IEnumerator FadeIn(float time)
        {
            float timePassed = Time.deltaTime / time;

            while (canvasGroup.alpha != 0)
            {
                canvasGroup.alpha -= timePassed;
                yield return null;
            }
        }
    }
}
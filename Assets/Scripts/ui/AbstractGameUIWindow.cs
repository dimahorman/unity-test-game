using System;
using System.Collections;
using UnityEngine;

namespace ui {
    public abstract class AbstractGameUIWindow : MonoBehaviour {
        protected CanvasGroup canvasGroup;
        
        public float fadeSpeed = 6f;
        
        protected virtual void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public IEnumerator FadeWindow(Action afterFade) {
            canvasGroup.interactable = false;
            while (canvasGroup.alpha > 0) {
                canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }

            afterFade();
        }

        public void HideWindow() {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }

        public void ShowWindow() {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }
    }
}

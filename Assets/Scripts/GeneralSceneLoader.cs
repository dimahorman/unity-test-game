using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace {
    public class GeneralSceneLoader : MonoBehaviour {
        
        public static GeneralSceneLoader Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void LoadScene(string sceneName, Action onFinish) {
            GameEvent.OnSceneLoadingStarted.Invoke();
            StartCoroutine(LoadSceneAsync(sceneName, onFinish));
        }
        
        public void LoadScene(string sceneName) {
            LoadScene(sceneName, () => {});
        }

        private IEnumerator LoadSceneAsync(string sceneName, Action onFinish) {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                GameEvent.OnSceneLoadingProgress.Invoke(progress);
                yield return null;
            }

            // We're ready to activate
            GameEvent.OnSceneLoadingProgress.Invoke(1f);

            yield return new WaitForSeconds(0.5f); // Optional delay
            operation.allowSceneActivation = true;

            // Wait until actually done
            while (!operation.isDone)
                yield return null;

            GameEvent.OnSceneLoadingFinished.Invoke();
            onFinish();
        }
    }
}
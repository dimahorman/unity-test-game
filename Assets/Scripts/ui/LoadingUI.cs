using mode;
using UnityEngine;
using UnityEngine.UI;

namespace ui {
    public class LoadingUI : MonoBehaviour {
        [SerializeField] private Slider loadingProgressBar;
        
        public void SetProgressBarValue(float progress) {
            loadingProgressBar.value = progress;
        }
        
        void HandleLoadStarted() {
            Debug.Log("Loading started...");
            GameEvent.GameModeSwitchUIEvent.Invoke(GameMode.Loading);
            SetProgressBarValue(0f);
        }

        void HandleLoadProgress(float progress) {
            SetProgressBarValue(progress);
            Debug.Log($"There is some loading progress: {progress}");
        }

        void HandleLoadCompleted() {
            // load current mode UI when loading is finished 
            Debug.Log("Changing UI MODE to current");
            GameEvent.GameModeSwitchUIEvent.Invoke(Managers.GameMode.Mode);
        }
        
        private void Awake() {
            GameEvent.OnSceneLoadingStarted.AddListener(HandleLoadStarted);
            GameEvent.OnSceneLoadingProgress.AddListener(HandleLoadProgress);
            GameEvent.OnSceneLoadingFinished.AddListener(HandleLoadCompleted);
        }

        private void OnDestroy() {
            GameEvent.OnSceneLoadingStarted.RemoveListener(HandleLoadStarted);
            GameEvent.OnSceneLoadingProgress.RemoveListener(HandleLoadProgress);
            GameEvent.OnSceneLoadingFinished.RemoveListener(HandleLoadCompleted);
        }
    }
}

    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MainMenuStartupController : MonoBehaviour {
        [SerializeField] private Slider _progressSlider;

        private void Awake() {
            GameEvent.ManagersProgressEvent.AddListener(OnManagersProgress);
            GameEvent.ManagersStartedEvent.AddListener(OnManagersStarted);
        }

        private void OnDestroy() {
            GameEvent.ManagersProgressEvent.RemoveListener(OnManagersProgress);
            GameEvent.ManagersStartedEvent.RemoveListener(OnManagersStarted);
        }

        private void OnManagersStarted() {
            SceneManager.LoadScene($"Scenes/Menu/MainMenuScene");
        }

        private void OnManagersProgress(int ready, int modules) {
            float progress = (float)ready / modules;
            _progressSlider.value = progress;
        }
    }

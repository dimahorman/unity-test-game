using mode;
using UnityEngine;
using UnityEngine.UI;

namespace controller {
    public class StartupController : MonoBehaviour {
        [SerializeField] private Slider progressSlider;

        private void Awake() {
            GameEvent.ManagersProgressEvent.AddListener(OnManagersProgress);
            GameEvent.ManagersStartedEvent.AddListener(OnManagersStarted);
        }

        private void OnDestroy() {
            GameEvent.ManagersProgressEvent.RemoveListener(OnManagersProgress);
            GameEvent.ManagersStartedEvent.RemoveListener(OnManagersStarted);
        }

        private void OnManagersStarted() {
            // Load Main menu when every manager has started
            Managers.GameMode.SwitchMode(GameMode.MainMenu);
        }

        private void OnManagersProgress(int ready, int modules) {
            float progress = (float)ready / modules;
            progressSlider.value = progress;
        }
    }
}
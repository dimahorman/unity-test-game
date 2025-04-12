using mode;
using ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace controller {
    public class StartupController : MonoBehaviour {
        [SerializeField] private LoadingUI loadingUI;
        
        private void Awake() {
            GameEvent.ManagersLaunchBeginEvent.AddListener(OnManagersLaunchBegin);
            GameEvent.ManagersProgressEvent.AddListener(OnManagersProgress);
            GameEvent.ManagersStartedEvent.AddListener(OnManagersStarted);
        }

        private void OnManagersLaunchBegin() {
            loadingUI.gameObject.SetActive(true);
        }

        private void OnDestroy() {
            GameEvent.ManagersStartedEvent.RemoveListener(OnManagersStarted);
        }

        private void OnManagersStarted() {
            // Load Main menu when every manager has started
            loadingUI.gameObject.SetActive(false);
            Managers.GameMode.SwitchMode(GameMode.MainMenu);
        }
        
        private void OnManagersProgress(int ready, int modules) {
            float progress = (float)ready / modules;
            loadingUI.SetProgressBarValue(progress);
        }
    }
}
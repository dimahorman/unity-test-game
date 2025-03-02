using System;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class StartupController : MonoBehaviour {
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
        Managers.Mission.GoNext();
    }

    private void OnManagersProgress(int ready, int modules) {
        float progress = (float)ready / modules;
        _progressSlider.value = progress;
    }
}
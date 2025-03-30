using System;
using System.Collections;
using System.Collections.Generic;
using mode;
using ui;
using ui.menu;
using ui.settings;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour {
    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private PointAndClickWindow pointAndClickWindow;
    [SerializeField] private MainMenuPanelWindow mainMenuPanel;

    [SerializeField] private AbstractGameUIWindow startWindow;

    private AbstractGameUIWindow _currentWindow;

    private readonly Stack<AbstractGameUIWindow> _previousWindows = new();

    private void Start() {
        settingsPopUp.HideWindow();
        pointAndClickWindow.HideWindow();

        _currentWindow = startWindow;
        _currentWindow.ShowWindow();
    }

    public void OnSettings() {
        OpenNextWindow(settingsPopUp);
    }

    public void OnPointAndClick() {
        OpenNextWindow(pointAndClickWindow);
    }
    
    public void OnLoadPointAndClickGame() {
        Managers.Data.LoadGameData();
    }
    
    public void OnPointAndClickNewGame() {
        Managers.Mission.GoNext();
    }

    public void OnFirstPerson() {
        GameEvent.GameModeChangeEvent.Invoke(GameMode.FirstPersonShooter);
    }

    public void OnQuit() {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void GoToPreviousWindow() {
        if (_previousWindows.Count > 0) {
            StartCoroutine(FadeCurrentWindow(() => {
                _currentWindow.HideWindow();
                _currentWindow = _previousWindows.Pop();
                
                _currentWindow.ShowWindow();
            }));
        }
    }

    private void OpenNextWindow(AbstractGameUIWindow window) {
        StartCoroutine(FadeCurrentWindow(() => {
            _currentWindow.HideWindow();
            _previousWindows.Push(_currentWindow);

            _currentWindow = window;
            window.ShowWindow();
        }));
    }

    private IEnumerator FadeCurrentWindow(Action afterFade) {
        return _currentWindow.FadeWindow(afterFade);
    }
}
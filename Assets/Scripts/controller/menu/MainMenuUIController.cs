using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour {
    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private PointAndClickWindow pointAndClickWindow;

    [SerializeField] private GameObject startWindow;

    private GameObject _currentWindow;

    private readonly Stack<GameObject> _previousWindows = new();

    private void Start() {
        settingsPopUp.Close();
        pointAndClickWindow.gameObject.SetActive(false);

        _currentWindow = startWindow;

        _currentWindow.SetActive(true);
    }

    public void OnSettings() {
        OpenNextWindow(settingsPopUp.gameObject);
    }

    public void OnPointAndClick() {
        OpenNextWindow(pointAndClickWindow.gameObject);
    }

    public void OnFirstPerson() {
        SceneManager.LoadScene($"Scenes/FirstPersonScene");
    }

    public void OnQuit() {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void GoToPreviousWindow() {
        if (_previousWindows.Count > 0) {
            StartCoroutine(FadeCurrentWindow(() => {
                _currentWindow.SetActive(false);
                _currentWindow = _previousWindows.Pop();

                _currentWindow.SetActive(true);
                var canvasGroup = _currentWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.alpha = 1;
            }));
        }
    }

    private void OpenNextWindow(GameObject window) {
        StartCoroutine(FadeCurrentWindow(() => {
            _currentWindow.SetActive(false);
            _previousWindows.Push(_currentWindow);

            _currentWindow = window;
            window.SetActive(true);
            var canvasGroup = window.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }));
    }

    private IEnumerator FadeCurrentWindow(Action afterFade) {
        var canvasGroup = _currentWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime * 6;
            yield return null;
        }

        afterFade();
    }
}
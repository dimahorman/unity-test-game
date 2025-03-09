using System;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour {
    [SerializeField] SettingsPopUp settingsPopUp;

    private void Start() {
        settingsPopUp.Close();
    }

    public void OnSettings() {
        
    }

    public void OnPointAndClick() {
        
    }

    public void OnFirstPerson() {
        
    }

    public void OnQuit() {
        Application.Quit();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopUp : MonoBehaviour {
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Toggle soundToggle;
    
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle musicToggle;

    [SerializeField] private AudioClip _clickSound;

    // Start is called before the first frame update
    void Start() {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
        soundSlider.value = PlayerPrefs.GetFloat("sound", 1f);
        soundToggle.isOn = PlayerPrefs.GetInt("sound_toggle", 1) == 1;
        
        musicSlider.value = PlayerPrefs.GetFloat("music_level", 1f); 
        musicToggle.isOn = PlayerPrefs.GetInt("music_toggle", 1) == 1;
    }

    // Update is called once per frame
    void Update() {
    }

    public void Open() {
        gameObject.SetActive(true);
    }

    public void Close() {
        Managers.Audio.PlayAudio(_clickSound);
        gameObject.SetActive(false);
    }

    public bool IsActive() {
        return gameObject.activeSelf;
    }

    public void OnSpeedChange() {
        PlayerPrefs.SetFloat("speed", speedSlider.value);
        GameEvent.SpeedChangeEvent.Invoke(speedSlider.value);
    }

    public void OnSoundChange() {
        PlayerPrefs.SetFloat("sound", soundSlider.value);
        Managers.Audio.soundVolume = soundSlider.value;
    }

    public void OnSoundToggle() {
        Managers.Audio.PlayAudio(_clickSound);
        Managers.Audio.soundMute = soundToggle.isOn;
        PlayerPrefs.SetInt("sound_toggle", soundToggle.isOn ? 1 : 0);
    }
    
    public void OnMusicLevelChange() {
        PlayerPrefs.SetFloat("music_level", musicSlider.value);
        Managers.Audio.musicVolume = musicSlider.value;
    }
    
    public void OnMusicToggle() {
        Managers.Audio.PlayAudio(_clickSound);
        Managers.Audio.musicMute = musicToggle.isOn;
        PlayerPrefs.SetInt("music_toggle", musicToggle.isOn ? 1 : 0);
    }

    public void OnMusic1() {
        PlayerPrefs.SetInt("music", 1);
        Managers.Audio.PlayIntroMusic();
    }
    
    public void OnMusic2() {
        PlayerPrefs.SetInt("music", 2);
        Managers.Audio.PlayLoopMusic();
    }
    
    public void OnStopMusic() {
        PlayerPrefs.SetInt("music", 3);
        Managers.Audio.StopMusic();
    }
}
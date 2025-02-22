using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour, IGameManager {
    private NetworkService _network;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource music1Source;
    [SerializeField] private AudioSource music2Source;
    
    [SerializeField] private string introBgMusic;
    [SerializeField] private string inGameBgMusic;

    private AudioSource _inactiveMusic;
    private AudioSource _activeMusic;

    private float _musicVolume;
    
    public float musicVolume {
        get {
            return _musicVolume;
        }

        set {
            _musicVolume = value;

            if (music1Source != null) {
                music1Source.volume = _musicVolume;
                music2Source.volume = _musicVolume;
            }
        }
    }

    public bool musicMute {
        get {
            return (music1Source != null && music1Source.mute) || (music2Source != null && music2Source.mute);
        }

        set {
            if (music1Source != null) {
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }
    }
    
    public float crossFadingRate = 1.5f;
    private bool _crossFading;
    
    public float soundVolume {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public ManagerStatus Status { get; private set; }

    public void Startup(NetworkService networkService) {
        Status = ManagerStatus.Initializing;
        
        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;      
        
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;
        
        soundVolume = PlayerPrefs.GetFloat("sound", 1f);
        soundMute = PlayerPrefs.GetInt("sound_toggle", 1) == 1;

        musicVolume = PlayerPrefs.GetFloat("music_level", 1f);
        musicMute = PlayerPrefs.GetInt("music_toggle", 1) == 1; 
        
        var musicChoice = PlayerPrefs.GetInt("music", 1);
        if (musicChoice == 1) {
            PlayIntroMusic();
        }
        else if (musicChoice == 2) {
            PlayLoopMusic();
        }
        else { 
            StopMusic();
        }

        _activeMusic = music1Source;
        _inactiveMusic = music2Source;
        
        _network = networkService;
        Status = ManagerStatus.Started;
    }

    private IEnumerator CrossFade(AudioClip clip) {
        _crossFading = true;
        
        _inactiveMusic.clip = clip;
        _inactiveMusic.volume = 0;
        _inactiveMusic.Play();
        
        float scaledRate = crossFadingRate * _musicVolume;

        while (_activeMusic.volume > 0) {
            _activeMusic.volume -= scaledRate * Time.deltaTime;
            _inactiveMusic.volume += scaledRate * Time.deltaTime;
            yield return null;
        }

        AudioSource temp = _activeMusic;
        _activeMusic = _inactiveMusic;
        _inactiveMusic = temp;
        _inactiveMusic.Stop();

        _crossFading = false;
    }

    public void PlayAudio(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void PlayIntroMusic() {
        PlayMusic(Resources.Load("Music/" + introBgMusic) as AudioClip);
    }
    
    public void PlayLoopMusic() {
        PlayMusic(Resources.Load("Music/" + inGameBgMusic) as AudioClip);
    }
    
    private void PlayMusic(AudioClip clip) {
        StartCoroutine(CrossFade(clip));
    }
    
    public void StopMusic() {
        music1Source.Stop();
        music2Source.Stop();
    }

    public void Awake() {
    }
}
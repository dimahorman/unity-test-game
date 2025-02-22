using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private Text _scoreLabel;
    [SerializeField] private GameObject _settingsWindow;
    SettingsPopUp settingsPopUp;
    
    private int _score = 0; 
    
    // Start is called before the first frame update
    void Start() {
        settingsPopUp = _settingsWindow.GetComponent<SettingsPopUp>();
        settingsPopUp.Close();
        _scoreLabel.text = _score.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            bool isShowing = settingsPopUp.gameObject.activeSelf;
            settingsPopUp.gameObject.SetActive(!isShowing);
            
            if (isShowing) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Managers.State.DoResume();
            }
            else {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Managers.State.DoPause();
            }
        }
    
    }

    public void OnOpenSettings() {
        Debug.Log("Settings opened");

        settingsPopUp.Open();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
        Managers.State.DoPause();
    }
    
    public void OnCloseSettings() {
        Debug.Log("Settings closed");
        settingsPopUp.Close();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Managers.State.DoResume();
    }

    public void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnQuit() {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnPointerDown() {
        Debug.Log("Pointer Down");
    }

    public void OnSpeedValue() {
        Debug.Log("Speed Changed");
        settingsPopUp.OnSpeedChange();
    }

    public void OnSoundValue() {
        Debug.Log("Sound level changed");
        settingsPopUp.OnSoundChange();
    }
    
    public void OnSoundToggle() {
        Debug.Log("Sound mute setting used");
        settingsPopUp.OnSoundToggle();
    }
    
    public void OnMusicValue() {
        Debug.Log("Music level changed");
        settingsPopUp.OnMusicLevelChange();
    }
    
    public void OnMusicToggle() {
        Debug.Log("Music mute setting used");
        settingsPopUp.OnMusicToggle();
    }

    public void OnMusic1() {
        settingsPopUp.OnMusic1();
    }
    
    public void OnMusic2() {
        settingsPopUp.OnMusic2();
    }
    
    public void OnStopMusic() {
        settingsPopUp.OnStopMusic();
    }
    
    public void OnNameSubmit() {
        Debug.Log("Name Submit");
    }

    public void Awake() {
        GameEvent.HitEvent.AddListener(OnEnemyHit);
    }

    public void OnDestroy() {
        GameEvent.HitEvent.RemoveListener(OnEnemyHit);
    }

    public void OnEnemyHit() {
        Debug.Log("Enemy hit event");
        _score += 1;
        _scoreLabel.text = _score.ToString();
    }
}
using mode;
using ui;
using UnityEngine;

namespace controller {
    public class GeneralUIController : MonoBehaviour {

        [SerializeField] private GameObject loadingCanvas;
        private LoadingUI _loadingUI;
        
        [SerializeField] private GameObject mainMenuCanvas;

        [SerializeField] private GameObject inGameMenuCommonCanvas;

        [SerializeField] private GameObject firstPersonCanvas;
        
        [SerializeField] private GameObject pointAndClickCanvas;
        
        private void Awake() { 
            GameEvent.GameModeSwitchUIEvent.AddListener(OnChangeGameModeUI);
        }
        
        private void Start() {
            loadingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            inGameMenuCommonCanvas.SetActive(false);
            firstPersonCanvas.SetActive(false);
            pointAndClickCanvas.SetActive(false);
        }

        private void OnDestroy() {
            GameEvent.GameModeSwitchUIEvent.RemoveListener(OnChangeGameModeUI);
        }

        private void OnChangeGameModeUI(GameMode mode) {
            switch (mode) {
                case GameMode.Loading:
                    Debug.Log("Switching to Loading UI...");
                    loadingCanvas.SetActive(true);
                    
                    mainMenuCanvas.SetActive(false);
                    inGameMenuCommonCanvas.SetActive(false);
                    firstPersonCanvas.SetActive(false);
                    pointAndClickCanvas.SetActive(false); 
                    break; 
                case GameMode.MainMenu:
                    mainMenuCanvas.SetActive(true);
                    
                    loadingCanvas.SetActive(false);
                    inGameMenuCommonCanvas.SetActive(false);
                    firstPersonCanvas.SetActive(false);
                    pointAndClickCanvas.SetActive(false); 
                    break; 
                
                case GameMode.FirstPersonShooter:
                    firstPersonCanvas.SetActive(true);

                    mainMenuCanvas.SetActive(false);
                    loadingCanvas.SetActive(false);
                    inGameMenuCommonCanvas.SetActive(false);
                    pointAndClickCanvas.SetActive(false); 
                    break; 
                
                case GameMode.PointAndClick:
                    pointAndClickCanvas.SetActive(true);

                    firstPersonCanvas.SetActive(false);
                    mainMenuCanvas.SetActive(false);
                    loadingCanvas.SetActive(false);
                    inGameMenuCommonCanvas.SetActive(false);
                    break; 
                
                case GameMode.PointAndClickPause:
                case GameMode.FirstPersonShooterPause:
                    inGameMenuCommonCanvas.SetActive(true);

                    pointAndClickCanvas.SetActive(false);
                    firstPersonCanvas.SetActive(false);
                    mainMenuCanvas.SetActive(false);
                    loadingCanvas.SetActive(false);
                    break;
            }
        }
    }
}
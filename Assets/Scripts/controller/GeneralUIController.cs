using System;
using mode;
using UnityEngine;

namespace controller {
    public class GeneralUIController : MonoBehaviour {

        [SerializeField] private GameObject loadingCanvas;
        
        [SerializeField] private GameObject mainMenuCanvas;

        [SerializeField] private GameObject inGameMenuCommonCanvas;

        [SerializeField] private GameObject firstPersonCanvas;
        
        [SerializeField] private GameObject pointAndClickCanvas;
        
        private void Awake() {
            GameEvent.GameModeChangeEvent.AddListener(OnChangeGameMode);
        }

        private void Start() {
            loadingCanvas.SetActive(true);
                    
            mainMenuCanvas.SetActive(false);
            inGameMenuCommonCanvas.SetActive(false);
            firstPersonCanvas.SetActive(false);
            pointAndClickCanvas.SetActive(false);
        }

        private void OnDestroy() {
            GameEvent.GameModeChangeEvent.RemoveListener(OnChangeGameMode);
        }

        private void OnChangeGameMode(GameMode mode) {
            switch (mode) {
                case GameMode.Loading:
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
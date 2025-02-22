
    using System;
    using UnityEngine;

    public class Crosshair : MonoBehaviour {
        private void Awake() {
            GameEvent.PauseGameEvent.AddListener(OnGamePause);
            GameEvent.ResumeGameEvent.AddListener(OnGameResume);
        }

        private void OnDestroy() {
            GameEvent.PauseGameEvent.RemoveListener(OnGamePause);
            GameEvent.ResumeGameEvent.RemoveListener(OnGameResume);
        }

        private void Start() {
            if (Managers.State.State == GameStateManager.GameState.INGAME) {
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);    
            }
        }

        private void Update() {
            
        }

        protected void OnGamePause() {
            gameObject.SetActive(false);
        }
        
        protected void OnGameResume() {
            gameObject.SetActive(true);
        }
    }


    using System;
    using UnityEngine;

    public abstract class PausableBehavior : MonoBehaviour {
        private void Update() { 
            if (Managers.State.State == GameStateManager.GameState.PAUSED) return;
            
            PausableUpdate();
        }

        protected virtual void PausableUpdate() {
            
        }
    } 
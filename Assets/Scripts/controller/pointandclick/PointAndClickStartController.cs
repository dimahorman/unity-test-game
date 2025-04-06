using System;
using System.Collections;
using manager.component;
using UnityEngine;

namespace controller.pointandclick {
    public class PointAndClickStartController : MonoBehaviour {
        
        private void Start() {
            StartCoroutine(LoadNewOrSavedGame());
        }

        private IEnumerator LoadNewOrSavedGame() {
            // just in case to load new scene after the start scene is completely loaded 
            yield return null;

            switch (PointAndClickStartManager._mode) {
                default:
                case PointAndClickStartManager.LoadingMode.NewGame:
                    Managers.Mission.GoNext();
                    break;
                
                case PointAndClickStartManager.LoadingMode.SavedGame:
                    Managers.Data.LoadGameData();
                    break;
            }
            
        }
    }
}
using System;
using UnityEngine;

namespace controller {
    public class GeneralCanvasHandler : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
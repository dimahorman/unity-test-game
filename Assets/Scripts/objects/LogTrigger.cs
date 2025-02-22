using System;
using UnityEngine;

public class LogTrigger : MonoBehaviour {
    public string _identifier;
    
    private bool _triggered;
    
    private void OnTriggerEnter(Collider other) {
        if(_triggered) return;
        
        Managers.Weather.LogWeather(_identifier);
        _triggered = true;
    }
}
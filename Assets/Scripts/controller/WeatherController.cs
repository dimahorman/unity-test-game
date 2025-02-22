using System;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    [SerializeField] private Light sun;
    [SerializeField] private Material sky;

    private float _cloudValue = 0f;
    private float _fullIntensity;

    private void Start() {
        _fullIntensity = sun.intensity;
    }

    private void Awake() {
        GameEvent.WeatherUpdatedEvent.AddListener(OnWeatherUpdate);
    }

    private void OnDestroy() {
        GameEvent.WeatherUpdatedEvent.RemoveListener(OnWeatherUpdate);
    }

    private void OnWeatherUpdate() {
        _cloudValue = Managers.Weather.cloudValue / 100;
        Debug.Log("Updated cloud value: " + _cloudValue);
        SetOvercast(_cloudValue);
    }
    
    private void Update() {

    }

    public void SetOvercast(float value) {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
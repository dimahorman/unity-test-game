using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus Status { get; private set; }

    private NetworkService _networkService;

    public float cloudValue { get; private set; }

    public void Startup(NetworkService networkService) {
        Debug.Log("Weather manager starting...");

        _networkService = networkService;

        StartCoroutine(_networkService.GetWeatherData(OnWeatherDataLoaded));
        Status = ManagerStatus.Initializing;
    }

    public void LogWeather(string name) {
        StartCoroutine(_networkService.SendLog(name, "Hello WORLD!!11!", cloudValue, OnWeatherLog));
    }

    public void OnWeatherLog(LogResponse data) {
        Debug.Log($"Weather logging executed: {JsonUtility.ToJson(data)}");
    }

    private void OnWeatherDataLoaded(WeatherDataResponse dataResponse) {
        Debug.Log("Weather manager started");
        Debug.Log(dataResponse);

        cloudValue = dataResponse.current.cloud_cover;

        GameEvent.WeatherUpdatedEvent.Invoke();
        Status = ManagerStatus.Started;
    }
}
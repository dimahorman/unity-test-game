[System.Serializable]
public class WeatherDataResponse {
    public CurrentWeatherResponse current;
    public float latitude;
    public float longitude;
    
}

[System.Serializable]
public class CurrentWeatherResponse {
    public string time;
    public int interval;
    public int cloud_cover;
}

[System.Serializable]
public class WeatherLogData {
    public string message;
    public long timestamp;
    public float cloudValue;
    public string name;
}

[System.Serializable]
public class LogResponse {
    public string status;
    public string message;
    public string operationType;
}
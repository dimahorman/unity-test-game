using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService {
    // latitude=25.7743&longitude=-80.1937 - Miami
    // latitude=51.5085&longitude=-0.1257 - London
    // private string apiUrl = "https://api.open-meteo.com/v1/forecast?latitude=25.7743&longitude=-80.1937&current=cloud_cover&format=json";
    private string weatherApiUrl =
        "https://api.open-meteo.com/v1/forecast?latitude=51.5085&longitude=-0.1257&current=cloud_cover&format=json";

    private string imageUrl =
        "https://transcode-v2.app.engoo.com/image/fetch/f_auto,c_lfill,w_300,dpr_3/https://assets.app.engoo.com/images/6exlGpcXkQgJJZ8B5T3w4Z.jpeg";

    private string loggerUrl = "http://localhost:8080/api/weather";


    private bool IsResponseValid(UnityWebRequest request) {
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("bad connection: " + request.error);
            return false;
        }
        else if (string.IsNullOrEmpty(request.downloadHandler.text)) {
            Debug.Log("bad data");
            return false;
        }
        else {
            return true;
        }
    }


    private IEnumerator PostJson<T>(string url, Action<T> callback, string postData = null) {
        using (var req = new UnityWebRequest(url, "POST")) {
            Debug.Log($"Log data: {postData}");

            if (postData != null) {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(postData);
                req.uploadHandler = new UploadHandlerRaw(bodyRaw);
                req.downloadHandler = new DownloadHandlerBuffer();    
            }
            
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();

            if (!IsResponseValid(req)) {
                Debug.Log("Response is invalid. Error: " + req.error);
                yield break;
            }

            T obj = JsonUtility.FromJson<T>(req.downloadHandler.text);
            callback(obj);
        }
    }

    private IEnumerator CallAPI<T>(string url, Action<T> callback, string postData = null) {
        using (var req = postData != null
                   ? UnityWebRequest.PostWwwForm("", postData)
                   : UnityWebRequest.Get(url)) {
            yield return req.SendWebRequest();

            if (!IsResponseValid(req)) {
                Debug.Log("Response is invalid. Error: " + req.error);
                yield break;
            }

            T obj = JsonUtility.FromJson<T>(req.downloadHandler.text);
            callback(obj);
        }
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback) {
        using (var req = UnityWebRequestTexture.GetTexture(imageUrl)) {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ProtocolError ||
                req.result == UnityWebRequest.Result.ConnectionError) {
                //TODO add callback handler for errors if needed
                Debug.Log($"Error downloading image: {req.error}");
            }

            callback(DownloadHandlerTexture.GetContent(req));
        }
    }

    public IEnumerator SendLog(string name, string message, float cloudValue, Action<LogResponse> callback) {
        var data = new WeatherLogData {
            message = message,
            cloudValue = cloudValue,
            timestamp = DateTime.UtcNow.Ticks,
            name = name
        };
        
        return PostJson(loggerUrl, callback, JsonUtility.ToJson(data));
    }

    public IEnumerator GetWeatherData(Action<WeatherDataResponse> callback) {
        return CallAPI(weatherApiUrl, callback);
    }
}
using System;
using UnityEngine;

public class ImageManager : MonoBehaviour, IGameManager {
    public ManagerStatus Status { get; private set; }

    private Texture2D _webImage;

    private NetworkService _service;

    public void Startup(NetworkService networkService) {
        _service = networkService;
        Status = ManagerStatus.Started;
    }

    public void GetWebImage(Action<Texture2D> callback) {
        if (_webImage == null) {
            StartCoroutine(_service.DownloadImage((image => {
                _webImage = image;
                callback(image);
            })));
        }
        else {
            callback(_webImage);
        }
    }
}
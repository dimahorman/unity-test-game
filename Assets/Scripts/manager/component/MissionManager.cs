using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager {
    public ManagerStatus Status { get; private set; }
    
    public int curLevel {get; private set;}
    public int maxLevel {get; private set;}
    
    private NetworkService _network;
    
    public void Startup(NetworkService networkService) {
        Debug.Log("Mission manager starting...");
        _network = networkService;

        curLevel = 0;
        maxLevel = 1;
        
        Status = ManagerStatus.Started;
    }

    public void GoNext() {
        if (curLevel < maxLevel) {
            curLevel++;
            var sceneName = $"Level{curLevel}";
            Debug.Log($"Loading scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else {
            Debug.Log("Last level");
        }
     }
}
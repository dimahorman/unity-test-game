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
        
        UpdateData(0, 1);
        
        Status = ManagerStatus.Started;
    }

    public void UpdateData(int curLevel, int maxLevel) {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }

    public void GoNext() {
        if (curLevel < maxLevel) {
            curLevel++;
            var sceneName = GetCurrentLevelName();
            Debug.Log($"Loading scene: {sceneName}");
            SceneManager.LoadScene($"Scenes/PointAndClick/{sceneName}");
        }
        else {
            Debug.Log("Last level");
        }
     }

    public void RestartCurrentLevel() {
        var sceneName = GetCurrentLevelName();
        
        SceneManager.LoadScene($"Scenes/PointAndClick/{sceneName}");
    }

    public string GetCurrentLevelName() {
        return $"Level{curLevel}";
    }

    public void ReachObjective() {
        GameEvent.LevelCompleteEvent.Invoke();
    }
}
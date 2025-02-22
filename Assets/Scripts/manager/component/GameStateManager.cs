using UnityEngine;

public class GameStateManager : MonoBehaviour, IGameManager {
    private NetworkService _network;

    public GameState State { get; private set; }

    public ManagerStatus Status { get; private set; }

    public void Startup(NetworkService networkService) {
        _network = networkService;
        State = GameState.INGAME;
        
        Status = ManagerStatus.Started;
    }

    public bool DoPause() {
        var changedState = DoChangeState(GameState.PAUSED);
        if (changedState) {
            GameEvent.PauseGameEvent.Invoke();
        }
        return changedState;
    }
    
    public bool DoResume() {
        var changedState = DoChangeState(GameState.INGAME);
        if (changedState) {
            GameEvent.ResumeGameEvent.Invoke();
        }
        return changedState;
    }

    private bool DoChangeState(GameState newState) {
        if (State != newState) {
            State = newState;
            return true;
        }

        return false;
    }
    
    public enum GameState {
        PAUSED,
        INGAME
    }
}


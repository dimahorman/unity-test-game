using mode;
using UnityEngine;

namespace manager.component {
    public class GameModeManager : MonoBehaviour, IGameManager {
        public GameMode Mode { get; private set; }
        
        public ManagerStatus Status { get; private set; }
        
        public void Startup(NetworkService networkService) {
            Mode = GameMode.Loading;

            Status = ManagerStatus.Started;
        }

        public void SwitchMode(GameMode newMode) {
            Mode = newMode;
            GameEvent.GameModeChangeEvent.Invoke(newMode);
        }
    }
}
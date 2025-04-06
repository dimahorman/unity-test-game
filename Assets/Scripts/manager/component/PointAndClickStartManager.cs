using UnityEngine;

namespace manager.component {
    
    // for now it's not mandatory to have it as component on a scene
    public class PointAndClickStartManager : MonoBehaviour, IGameManager {
        
        public enum LoadingMode {
            NewGame,
            SavedGame
        }

        public static LoadingMode _mode { get; private set; } = LoadingMode.NewGame;
        
        private NetworkService _networkService;
        
        public ManagerStatus Status { get; private set; }
        public void Startup(NetworkService networkService) {
            _networkService = networkService;
            
            Status = ManagerStatus.Started;
        }

        public static void ChangeLoadingMode(LoadingMode mode) {
            _mode = mode;
        }
    }
}
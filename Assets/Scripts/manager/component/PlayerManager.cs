
    using UnityEngine;

    public class PlayerManager : MonoBehaviour, IGameManager {
        public ManagerStatus Status { get; private set; }
        
        private NetworkService _network;
        
        public int Health {get; private set; }
        public int MaxHealth {get; private set; }
        
        public void Startup(NetworkService networkService) {
            Debug.Log("Starting PlayerManager...");
            
            Health = 50;
            MaxHealth = 100;
            _network = networkService;
            Status = ManagerStatus.Started;
        }

        public void ChangeHealth(int value) {
            Health += value;
            if (Health > MaxHealth) {
                Health = MaxHealth;
            } else if (Health < 0) {
                Health = 0;
            }
            
            Debug.Log("Health: " + Health + "/" + MaxHealth);
        }
    }

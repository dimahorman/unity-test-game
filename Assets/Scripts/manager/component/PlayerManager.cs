
    using UnityEngine;

    public class PlayerManager : MonoBehaviour, IGameManager {
        public ManagerStatus Status { get; private set; }
        
        private NetworkService _network;
        
        public int Health {get; private set; }
        public int MaxHealth {get; private set; }
        
        public void Startup(NetworkService networkService) {
            Debug.Log("Starting PlayerManager...");
            
            Health = 3;
            MaxHealth = 3;
            _network = networkService;
            Status = ManagerStatus.Started;
        }

        private void UpdateData(int health, int maxHealth) {
            Health = health;
            MaxHealth = maxHealth;
        }

        public void ChangeHealth(int value) {
            var healthBefore = Health;
            Health += value;
            if (Health > MaxHealth) {
                Health = MaxHealth;
                return;
            } else if (Health < 0) {
                Health = 0;
                return;
            }

            GameEvent.HealthChangedEvent.Invoke(healthBefore, Health);
            if (Health == 0) {
                GameEvent.LevelFailedEvent.Invoke();
            }
            
            Debug.Log("Health: " + Health + "/" + MaxHealth);
        }

        public void Respawn() {
            UpdateData(3, 3);
        }
    }

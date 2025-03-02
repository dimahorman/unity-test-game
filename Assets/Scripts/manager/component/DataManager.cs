using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager{
    public ManagerStatus Status { get; private set; }

    private string _filename;
    
    private NetworkService _network;
    
    public void Startup(NetworkService networkService) {
        _network = networkService;
        
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        Status = ManagerStatus.Started;
    }

    public void SaveGameData() {
        IDictionary<string, object> data = new Dictionary<string, object>();
        data.Add("inventory", Managers.Inventory.GetData());
        data.Add("maxHealth", Managers.Player.MaxHealth);
        data.Add("health", Managers.Player.Health);
        data.Add("curLevel", Managers.Mission.curLevel);
        data.Add("maxLevel", Managers.Mission.maxLevel);

        var stream = File.Create(_filename);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        
        stream.Close();
    }

    public void LoadGameData() {
        if (!File.Exists(_filename)) {
            Debug.Log("No game data saved!");
            return;
        }

        IDictionary<string, object> data;

        var stream = File.Open(_filename, FileMode.Open);
        var formatter = new BinaryFormatter();
        data = formatter.Deserialize(stream) as Dictionary<string, object>;

        Managers.Inventory.UpdateData(data["inventory"] as IDictionary<string, int>);
        Managers.Player.UpdateData((int)data["health"], (int)data["maxHealth"]);
        Managers.Mission.UpdateData((int) data["curLevel"], (int) data["maxLevel"]);
        
        Managers.Mission.RestartCurrentLevel();
    }
}
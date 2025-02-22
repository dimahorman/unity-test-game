using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(ImageManager))]
[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(GameStateManager))]
public class Managers : MonoBehaviour {
    public static InventoryManager Inventory { get; private set; }
    public static PlayerManager Player { get; private set; }
    public static WeatherManager Weather { get; private set; }
    public static ImageManager Image { get; private set; }
    public static AudioManager Audio { get; private set; }
    public static GameStateManager State { get; private set; }
    
    private List<IGameManager> _startSequence;

    private void Awake() {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        Weather = GetComponent<WeatherManager>();
        Image = GetComponent<ImageManager>();
        Audio = GetComponent<AudioManager>();
        State = GetComponent<GameStateManager>();
        
        NetworkService network = new NetworkService();
        
        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);
        _startSequence.Add(Weather);
        _startSequence.Add(Image);
        _startSequence.Add(Audio);
        _startSequence.Add(State);

        StartCoroutine(StartupManagers(network));
    }
    
    private IEnumerator StartupManagers(NetworkService network) {
        foreach (IGameManager manager in _startSequence) {
            manager.Startup(network);
        }
        yield return null;
        int numModules = _startSequence.Count;
        int numReady = 0;
        while (numReady < numModules) { 
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in _startSequence) {
                if (manager.Status == ManagerStatus.Started) {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null;
        }
        Debug.Log("All managers started up");
    }
}
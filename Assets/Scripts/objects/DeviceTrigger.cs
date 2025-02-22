using System;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {
    [SerializeField] private GameObject[] targets;

    public bool requireKey;
    
    private void Start() {
        
    }
    private void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (requireKey && Managers.Inventory.equippedItem != "Key") {
            return;
        }
        foreach (var target in targets) {
            target.SendMessage("Activate");
        }
    }
    
    private void OnTriggerExit(Collider other) {
        foreach (var target in targets) {
            target.SendMessage("Deactivate");
        }
    }
}
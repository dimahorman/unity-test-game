using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
    public ManagerStatus Status { get; private set; }

    private IDictionary<string, int> _items;
    
    public string equippedItem {get; private set;}
    
    public void Startup(NetworkService networkService) {
        Debug.Log("InventoryManager starting...");
        _items = new Dictionary<string, int>();
        Status = ManagerStatus.Started;
    }

    public bool EquipItem(string item) {
        
        if (_items.ContainsKey(item) && equippedItem != item) {
            equippedItem = item;
            Debug.Log("Equipped " + item);
            return true;
        }

        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }
    public void AddItem(string item) {
        if (_items.ContainsKey(item)) {
            _items[item] += 1;
        } else {
            _items[item] = 1;
        }
    }

    public bool ConsumeItem(string name) {
        if (_items.ContainsKey(name)) {
            _items[name] -= 1;

            if (_items[name] == 0) {
                _items.Remove(name);
            }
            
        } else {
            return false;
        }

        return true;
    }

    public List<string> GetItemList() {
        return new List<string>(_items.Keys);
    }

    public int GetItemCount(string name) {
        if (_items.ContainsKey(name)) {
            return _items[name];
        }

        return 0;
    }
 
    private void DisplayItems() {
        Debug.Log("*Inventory*");
        foreach (var item in _items) {
            Debug.Log("Item: " + item.Key + " Quantity: " + item.Value);
        }
    }
}
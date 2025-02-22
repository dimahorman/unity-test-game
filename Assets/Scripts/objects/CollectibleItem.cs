using System;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
    [SerializeField] private string itemName;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("ITEM: " + itemName + " COLLECTED!");
        Managers.Inventory.AddItem(itemName);
        Destroy(this.gameObject);
    }
}
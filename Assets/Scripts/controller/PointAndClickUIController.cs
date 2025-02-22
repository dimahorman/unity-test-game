
    using System;
    using UnityEngine;

    public class PointAndClickUIController : MonoBehaviour {
        
       private InventoryPopUp _inventoryPopUp;
       [SerializeField] private GameObject inventoryWindow;

       private void Awake() {
           GameEvent.OnSelectEventInventoryItem.AddListener(OnSelectInventoryItem);
       }

       private void OnDestroy() {
           GameEvent.OnSelectEventInventoryItem.RemoveListener(OnSelectInventoryItem);
       }

       private void Start() {
           _inventoryPopUp = inventoryWindow.GetComponent<InventoryPopUp>();
           _inventoryPopUp.Close();
       }

       private void Update() {
           if (Input.GetKeyDown(KeyCode.I)) {
               if (_inventoryPopUp.gameObject.activeSelf) {
                   _inventoryPopUp.Close();
                   Debug.Log("Close Inventory");
               }
               else {
                   _inventoryPopUp.Open();
                   Debug.Log("Open Inventory");
               }
           }
       }

       public void OnSelectInventoryItem(string item) {
           Debug.Log($"{item} selected");
           _inventoryPopUp.OnSelectItem(item);
       }

       public void OnClickEquip() {
           _inventoryPopUp.OnItemEquip();
       }
       
       public void OnClickUse() {
           _inventoryPopUp.OnItemUse();
       }
    }
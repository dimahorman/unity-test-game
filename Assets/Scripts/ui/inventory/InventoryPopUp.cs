using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopUp : MonoBehaviour{
    [SerializeField] private GameObject _itemUIPrefab;

    private IDictionary<string, InventoryElem> _inventoryElems;

    [SerializeField] private Button _equipButton; 
    [SerializeField] private Button _useButton; 
    
    private InventoryElem _selectedElem;
    
    public Vector2 initialElemPosition = new Vector2(-100, 57);
    public Vector2 horizontalElemShift = new Vector2(40, 0);
    
    private void Start() {
        _inventoryElems = new Dictionary<string, InventoryElem>();
        
        _itemUIPrefab.SetActive(false);
        _useButton.interactable = false;
        _equipButton.interactable = false;
    }

    private IEnumerator LoadInventory() {
        var itemNames = Managers.Inventory.GetItemList();
        Debug.Log("Got item list");
        if (itemNames.Count == 0) {
            _itemUIPrefab.SetActive(false);
            yield break;
        }
        
        for (int i = 0; i < itemNames.Count; i++) {
            var uiElem = i == 0 ? _itemUIPrefab : Instantiate(_itemUIPrefab, transform);
            var rectTransform = uiElem.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1); 
            
            rectTransform.anchoredPosition = initialElemPosition + (i * horizontalElemShift);
            uiElem.SetActive(true);

            var inventoryElem = uiElem.GetComponent<InventoryElem>();
            inventoryElem.NameId = itemNames[i];
            inventoryElem.Quantity = Managers.Inventory.GetItemCount(itemNames[i]);

            _inventoryElems.Add(inventoryElem.NameId, inventoryElem);
            Debug.Log("Added Elem {itemNames[i]}" + inventoryElem.NameId);
            yield return null;
        }
    }

    public void Close() {
        _selectedElem = null;
        _useButton.interactable = false;
        _equipButton.interactable = false;
        gameObject.SetActive(false);
    }

    public void Open() {
        gameObject.SetActive(true);
        StartCoroutine(ReloadInventory());
    }

    private IEnumerator ReloadInventory() {
        foreach (var elem in _inventoryElems.Values.ToList()) {
            _inventoryElems.Remove(elem.NameId);

            if (_itemUIPrefab == elem.gameObject) {
                _itemUIPrefab.SetActive(false);
            }
            else {
                Destroy(elem.gameObject);    
            }
            
            yield return null;
        }

        yield return LoadInventory();
    }

    public void OnSelectItem(string itemName) {
        Debug.Log($"Selected elem: {itemName}");
        
        _selectedElem = _inventoryElems[itemName];
        if (itemName == "Health") {
            _useButton.interactable = true;
        }
        else {
            _useButton.interactable = false;
        }
        
        _equipButton.interactable = true;
    }

    public void OnItemEquip() {
        Managers.Inventory.EquipItem(_selectedElem.NameId);
    }
    
    public void OnItemUse() {
        Managers.Inventory.ConsumeItem(_selectedElem.NameId);
        if (Managers.Inventory.GetItemCount(_selectedElem.NameId) == 0) {
            _selectedElem = null;
            _useButton.interactable = false;
            _equipButton.interactable = false;
        }

        StartCoroutine(ReloadInventory());
    }
}
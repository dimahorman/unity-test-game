using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryElem : MonoBehaviour, IPointerClickHandler {
    private int _quantity;
    private string _nameId;

    public string NameId {
        set {
            _nameId = value;
            _nameText.text = _nameId;
            elemImage.sprite = Resources.Load<Sprite>($"UI/{value}");
        }

        get => _nameId;
    }
    public int Quantity {
        set {
            _qText.text = $"x{value}";
            _quantity = value;
        }
        get => _quantity;
    }

    [SerializeField] private TextMeshProUGUI _qText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image elemImage;

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log($"Elem {NameId} is clicked");
        GameEvent.OnSelectEventInventoryItem.Invoke(NameId);
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    [SerializeField] private Image[] heartImages;

    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private void Start() {
        UpdateHealth(3,Managers.Player.Health);
    }

    public void UpdateHealth(int healthBefore, int healthAfter) {
        Debug.Log("HEALTH IS UPDATING...");
        for (var i = 0; i < heartImages.Length; i++) {
            heartImages[i].sprite = i < healthAfter ? fullHeartSprite : emptyHeartSprite;
        }
    }
}
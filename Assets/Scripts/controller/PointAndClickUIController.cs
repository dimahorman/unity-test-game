
    using System.Collections;
    using TMPro;
    using UnityEngine;

    public class PointAndClickUIController : MonoBehaviour { 
        [SerializeField] private InventoryPopUp inventoryPopUp; 
        [SerializeField] private HealthUI healthUI; 
        [SerializeField] private TextMeshProUGUI levelCompleteText;
       

       private void Awake() {
           GameEvent.OnSelectEventInventoryItem.AddListener(OnSelectInventoryItem);
           GameEvent.LevelCompleteEvent.AddListener(OnLevelComplete);
           GameEvent.LevelFailedEvent.AddListener(OnLevelFailed);
           GameEvent.HealthChangedEvent.AddListener(OnUpdateHealth);
       }

       private void OnDestroy() {
           GameEvent.OnSelectEventInventoryItem.RemoveListener(OnSelectInventoryItem);
           GameEvent.LevelCompleteEvent.RemoveListener(OnLevelComplete);
           GameEvent.LevelFailedEvent.RemoveListener(OnLevelFailed);
           GameEvent.HealthChangedEvent.RemoveListener(OnUpdateHealth);
       }

       private void Start() {
           levelCompleteText.gameObject.SetActive(false);
           inventoryPopUp.Close();
       }

       private void Update() {
           if (Input.GetKeyDown(KeyCode.I)) {
               if (inventoryPopUp.gameObject.activeSelf) {
                   inventoryPopUp.Close();
                   Debug.Log("Close Inventory");
               }
               else {
                   inventoryPopUp.Open();
                   Debug.Log("Open Inventory");
               }
           }
       }

       public void OnSelectInventoryItem(string item) {
           Debug.Log($"{item} selected");
           inventoryPopUp.OnSelectItem(item);
       }

       public void OnClickEquip() {
           inventoryPopUp.OnItemEquip();
       }
       
       public void OnClickUse() {
           inventoryPopUp.OnItemUse();
       }

       public void OnLevelComplete() {
           StartCoroutine(CompleteLevel());
       }
       
       public void OnLevelFailed() {
           StartCoroutine(HandleLevelFailed());
       }

       private IEnumerator HandleLevelFailed() {
           levelCompleteText.text = "Level Failed!";
           levelCompleteText.gameObject.SetActive(true);
           yield return new WaitForSeconds(2);

           Managers.Player.Respawn();
           Managers.Mission.RestartCurrentLevel();
       }

       private IEnumerator CompleteLevel() {
           levelCompleteText.text = "Level Complete!";
           levelCompleteText.gameObject.SetActive(true);
           yield return new WaitForSeconds(2);
           
           Managers.Mission.GoNext();
       }
       
       private void OnUpdateHealth(int healthBefore, int healthAfter) {
           healthUI.UpdateHealth(healthBefore, healthAfter);
       }
    }
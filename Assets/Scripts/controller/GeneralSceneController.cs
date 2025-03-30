using mode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace controller {
    public class GeneralSceneController : MonoBehaviour {
        private const string MainMenuSceneName = "MainMenuScene";
        private const string FirstPersonSceneName = "FirstPersonScene";

        private void Awake() {
            DontDestroyOnLoad(gameObject);
            GameEvent.GameModeChangeEvent.AddListener(OnGameModeChange);
        }

        private void OnDestroy() {
            GameEvent.GameModeChangeEvent.RemoveListener(OnGameModeChange);
        }

        private void OnGameModeChange(GameMode mode) {
            Debug.Log("GAME MODE CHANGED");
            switch (mode) {
                // TODO Add handling for other modes as well
                case GameMode.MainMenu: 
                    LoadSceneIfNotAlready(MainMenuSceneName);
                    break;
                
                case GameMode.FirstPersonShooter:
                    LoadSceneIfNotAlready(FirstPersonSceneName);
                    break;
            }
        }

        private void LoadSceneIfNotAlready(string sceneName) {
            if (SceneExists(sceneName) && !IsSceneActive(sceneName)) {
                Debug.Log("SCENE LOADING");
                SceneManager.LoadScene(sceneName);    
            }
        }
        
        private bool IsSceneActive(string sceneName) {
            return SceneManager.GetActiveScene().name == sceneName;
        }
        
        private bool SceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string extractedSceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath); 
                
                Debug.Log($"Name of the scene checked: {sceneName}");
                Debug.Log($"Name of the actual scene: {extractedSceneName}");
                
                if (extractedSceneName == sceneName)
                {
                    return true; // Scene exists in Build Settings
                }
            }
            return false; // Scene not found
        }
    }
}
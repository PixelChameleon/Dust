using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalScripts {
    public class DustManager : MonoBehaviour {

        public List<ItemStack> ItemRegistry = new();
        private int _currentSceneID = 2;
        public PlayerScript player;
        public GameObject bullet;
        public Sprite[] ItemSprites;

        private void Start() {
            
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            ItemRegistry.Add(new ItemStack(0, "Key", ItemSprites[0]));
            if (!Application.isEditor) { // Scene loading behaves differently in the editor for some reason...
                SceneManager.LoadScene(_currentSceneID, LoadSceneMode.Additive);
            }
            //Debug.LogError("Scenes: " + SceneManager.sceneCount + "/" + SceneManager.sceneCountInBuildSettings);
        }
        

        // Switches the scene. Scene IDs are set in File -> Build Settings
        public void SwitchScene(int id) {
            SceneManager.UnloadScene(_currentSceneID);
            SceneManager.LoadScene(id, LoadSceneMode.Additive); // Maybe async loading? although they are pretty small anyways
            _currentSceneID = id;
            Debug.Log("Switched scene to " + _currentSceneID);
        }
    }
    
    
}

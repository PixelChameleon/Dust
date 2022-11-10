using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalScripts {
    public class DustManager : MonoBehaviour {

        private IDictionary<int, ItemStack> _itemRegistry = new Dictionary<int, ItemStack>();

        private int _currentSceneID = 1;
        public PlayerScript player;

        private void Start() {
            // TODO: Load items from configuration file instead of hardcoding them.
            _itemRegistry.Add(0, new ItemStack(0, "Key"));
            if (!Application.isEditor) { // Scene loading behaves differently in the editor for some reason...
                SceneManager.LoadScene(_currentSceneID, LoadSceneMode.Additive);
            }
            //Debug.LogError("Scenes: " + SceneManager.sceneCount + "/" + SceneManager.sceneCountInBuildSettings);
        }

        public ItemStack GetItemStack(int id) {
            return _itemRegistry[id];
        }

        // Switches the scene. Scene IDs are set in File -> Build Settings
        public void SwitchScene(int id) {
            player.camera.enabled = false;
            SceneManager.UnloadScene(_currentSceneID);
            SceneManager.LoadScene(id, LoadSceneMode.Additive); // Maybe async loading? although they are pretty small anyways
            _currentSceneID = id;
            player.camera.enabled = true;
            Debug.Log("Switched scene to " + _currentSceneID);
        }
    }
    
    
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalScripts {
    public class DustManager : MonoBehaviour {

        public static readonly List<ItemStack> ItemRegistry = new();
        private int _currentSceneID = 2;
        public PlayerScript player;
        public GameObject bullet;
        public GameObject ClickableObjectTextBox;
        public GameObject interactionHintText;
        public GameObject interactionHintImage;
        public Sprite[] ItemSprites;

        [Serializable]
        public struct NamedImage {
            public string name;
            public Texture2D image;
        }
        public NamedImage[] cursors;
        public GameObject cursorPrefab;

        private void Start() {
            
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            ItemRegistry.Add(new ItemStack(0, "TestKey", ItemSprites[0]));
            ItemRegistry.Add(new ItemStack(1, "Geheimes Dokument", ItemSprites[1]));
            ItemRegistry.Add(new ItemStack(2, "Schraubenzieher", ItemSprites[2]));
            Debug.Log("Loaded " + ItemRegistry.Count + " items.");
            if (!Application.isEditor) { // Scene loading behaves differently in the editor for some reason...
                SceneManager.LoadScene(_currentSceneID, LoadSceneMode.Additive);
            }
            //Debug.LogError("Scenes: " + SceneManager.sceneCount + "/" + SceneManager.sceneCountInBuildSettings);
        }

        // Switches the scene. Scene IDs are set in File -> Build Settings
        public void SwitchScene(int id) {
            player.camera.gameObject.SetActive(false);
            SceneManager.LoadScene(id, LoadSceneMode.Additive); // Maybe async loading? although they are pretty small anyways
            if (SceneManager.UnloadScene(_currentSceneID) == false) {
                Debug.LogError("Failed to unload scene.");
            }
            Resources.UnloadUnusedAssets();
            _currentSceneID = id;
            Debug.Log("Switched scene to " + _currentSceneID);
            
            // reset cursor hint on level switch
            interactionHintImage.SetActive(false);
            interactionHintText.GetComponent<TextMeshProUGUI>().text = "";
        }
        

        public Texture2D GetCursorTexture(MouseCursorChange.CursorShape shape) {
            foreach (var picshape in cursors) {
                if (picshape.name == shape.ToString()) {
                    return picshape.image;
                }
            }

            return null;
        }
    }
    
    
}

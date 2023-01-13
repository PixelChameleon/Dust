using System;
using System.Collections.Generic;
using GlobalScripts.combat;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalScripts {
    public class DustManager : MonoBehaviour {

        public static readonly List<ItemStack> ItemRegistry = new();
        private int _currentSceneID = 2;
        public bool[] fixedCamera = new bool[5];
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
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
            
            ItemRegistry.Add(new ItemStack(0, "Zange", ItemSprites[0]));
            ItemRegistry.Add(new ItemStack(1, "Geheimes Dokument", ItemSprites[1]));
            ItemRegistry.Add(new ItemStack(2, "Schraubenzieher", ItemSprites[2]));
            ItemRegistry.Add(new ItemStack(3, "Defekter Roboterkörper", ItemSprites[3]));
            ItemRegistry.Add(new ItemStack(4, "Defekte Roboterbeine", ItemSprites[4]));
            ItemRegistry.Add(new ItemStack(5, "Roboterkopf", ItemSprites[5]));
            ItemRegistry.Add(new ItemStack(6, "Kopfloser Roboter", ItemSprites[6]));
            ItemRegistry.Add(new ItemStack(7, "Inaktiver Roboter", ItemSprites[7]));
            ItemRegistry.Add(new Weapon(8, "BoomBoom-2 Shotgun", ItemSprites[8], 1, 20, 2, 0.5f, 2.0f));
            ItemRegistry.Add(new Weapon(9, "BzzzBzzz-42 Laserpistole", ItemSprites[9], 1, 10, 7, 0.2f, 7.0f));
            ItemRegistry.Add(new Weapon(10, "BrrrBrrr-64 Maschinengewehr", ItemSprites[10], 3, 5, 5, 0.3f, 4.0f));
            Debug.Log("Loaded " + ItemRegistry.Count + " items.");

            ItemRegistry[3].CanCombineWith = ItemRegistry[4]; // Körper + Beine
            ItemRegistry[3].CombinationResult = ItemRegistry[6]; // = Kopfloser Roboter
            ItemRegistry[4].CanCombineWith = ItemRegistry[3]; // Beine + Körper
            ItemRegistry[4].CombinationResult = ItemRegistry[6]; // = Kopfloser Roboter
            ItemRegistry[5].CanCombineWith = ItemRegistry[6]; // Kopf + Kopfloser Roboter
            ItemRegistry[5].CombinationResult = ItemRegistry[7]; // = Roboter
            ItemRegistry[6].CanCombineWith = ItemRegistry[5]; // Kopfloser Roboter + Kopf
            ItemRegistry[6].CombinationResult = ItemRegistry[7]; // = Roboter
            /*if (!Application.isEditor) { // Scene loading behaves differently in the editor for some reason...
                SceneManager.LoadScene(_currentSceneID, LoadSceneMode.Additive);
            }*/
            //Debug.LogError("Scenes: " + SceneManager.sceneCount + "/" + SceneManager.sceneCountInBuildSettings);
        }

        // Switches the scene. Scene IDs are set in File -> Build Settings
        public void SwitchScene(int id) {
            //player.camera.gameObject.SetActive(false);
            SceneManager.UnloadSceneAsync(_currentSceneID);
            SceneManager.LoadScene(id, LoadSceneMode.Additive); // Maybe async loading? although they are pretty small anyways
            //Resources.UnloadUnusedAssets(); Do we need this?
            _currentSceneID = id;
            Debug.Log("Switched scene to " + _currentSceneID);
            
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
            
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

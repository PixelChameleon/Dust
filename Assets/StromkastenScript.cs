using GlobalScripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class StromkastenScript : MonoBehaviour {
    
    private int successCounter = 0;

    public void OnClick() {
        var button = EventSystem.current.currentSelectedGameObject;
        
        if (successCounter == 0) {
            if (button.CompareTag("CableBlue")) {
                successCounter = 1;
                Debug.Log("First color success.");
                return;
            }
            Debug.Log("Failed first");
            // TODO: Kill the player
        }
        if (successCounter == 1) {
            if (button.CompareTag("CableRed")) {
                successCounter = 2;
                Debug.Log("Second color success.");
                return;
            }
            Debug.Log("Failed second");
            // TODO: Kill the player
            
        }
        if (successCounter == 2) {
            if (button.CompareTag("CableGreen")) {
                successCounter = 3;
                Debug.Log("Done");
                close();
                PlayerPrefs.SetInt("Stromkasten", 1);
                return;
            }
            Debug.Log("Failed last");
            // TODO: Kill the player
        }
    }

    public void close() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().canMove = true;
        gameObject.SetActive(false);
    }
}

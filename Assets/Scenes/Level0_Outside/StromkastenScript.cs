using GlobalScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StromkastenScript : MonoBehaviour {
    
    private int successCounter = 0;
    private PlayerScript _player;
    public GameObject background;
    public GameObject greenCableImage;
    public GameObject blueCableImage;
    public GameObject redCableImage;

    public void OnClick() {
        var button = EventSystem.current.currentSelectedGameObject;
        _player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
        if (successCounter == 0) {
            if (button.CompareTag("CableRed")) {
                successCounter = 1;
                Debug.Log("First color success.");
                redCableImage.GetComponent<CableDestroyScript>().DestroyCable();
                return;
            }
            Debug.Log("Failed first");
            close();
            _player.Die("Du hast einen Stromschlag bekommen. Bzzz bzzz");
        }
        if (successCounter == 1) {
            if (button.CompareTag("CableBlue")) {
                successCounter = 2;
                Debug.Log("Second color success.");
                blueCableImage.GetComponent<CableDestroyScript>().DestroyCable();
                return;
            }   
            Debug.Log("Failed second");
            close();
            successCounter = 0;
            _player.Die("Aua! Stromschläge sind gefährlich.");

        }
        if (successCounter == 2) {
            if (button.CompareTag("CableGreen")) {
                successCounter = 3;
                Debug.Log("Done");
                PlayerPrefs.SetInt("Stromkasten", 1);
                greenCableImage.GetComponent<CableDestroyScript>().DestroyCable();
                return;
            }
            Debug.Log("Failed last");
            close();
            successCounter = 0;
            _player.Die("Hochspannung ist tödlich.");
        }
    }

    public void close() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().canMove = true;
        gameObject.SetActive(false);
    }

    public void ResetCables() {
        redCableImage.GetComponent<CableDestroyScript>().FixCable();
        greenCableImage.GetComponent<CableDestroyScript>().FixCable();
        blueCableImage.GetComponent<CableDestroyScript>().FixCable();
    }
}

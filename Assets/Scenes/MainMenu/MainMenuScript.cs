using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public void Quit() {
        Application.Quit();
    }

    public void Play() {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ResetGame() {
        PlayerPrefs.SetInt("Stromkasten", 0);
        PlayerPrefs.SetInt("Lagerraum", 0);
    }
    
    public void VolumeChange() {
        GameObject o = EventSystem.current.currentSelectedGameObject;
        PlayerPrefs.SetFloat("volume", o.GetComponent<Slider>().value);
    }
}
    

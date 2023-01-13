using System;
using GlobalScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedCamera : MonoBehaviour {
    
    private GameObject playerCam;
    void Start() {
        if (!GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>().fixedCamera[gameObject.scene.buildIndex]) {
            return;
        }
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().camera;
        cam.gameObject.SetActive(false);
        playerCam = cam.gameObject;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().camera = GetComponent<Camera>();
    }
    
    private void OnDestroy() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().camera = playerCam.GetComponent<Camera>();
        playerCam.SetActive(true);
    }
}

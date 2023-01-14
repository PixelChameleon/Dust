using System.Collections;
using System.Collections.Generic;
using GlobalScripts;
using UnityEngine;

public class CompanionTextTrigger : MonoBehaviour {

    public string text;

    public int duration = 5;

    public bool alreadyTriggered = false;
    

    void Start() {
    }

    private void OnTriggerEnter(Collider other) {
        var player = other.gameObject.GetComponent<PlayerScript>();
        if (alreadyTriggered || player == null || player.Companion == null) {
            return;
        }
        player.Companion.GetComponent<CompanionScript>().Talk(text, duration);
        alreadyTriggered = true;
    }
}

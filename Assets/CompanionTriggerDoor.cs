using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionTriggerDoor : MonoBehaviour {

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other) {
        if (alreadyTriggered) {
            return;
        }
        var comp = other.gameObject.GetComponent<CompanionScript>();
        if (comp == null) {
            return;
        }
        PlayerPrefs.SetInt("Lagerraum", 1);
        comp.Talk("Schl-oss ge-öffnet. Boop.", 5);
    }
}

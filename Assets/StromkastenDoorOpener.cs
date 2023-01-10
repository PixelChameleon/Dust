using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StromkastenDoorOpener : MonoBehaviour {
    
    private OffMeshLink _link;
    void Start() {
        InvokeRepeating(nameof(Check), 2, 2);
        _link = GetComponent<OffMeshLink>();
    }
    
    void Check() {
        if (!_link.activated && PlayerPrefs.GetInt("Stromkasten") == 1) {
            _link.activated = true;
        }
    }
}

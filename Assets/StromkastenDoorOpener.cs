using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StromkastenDoorOpener : MonoBehaviour {
    
    private OffMeshLink _link;
    public string openerID = "Stromkasten";
    void Start() {
        InvokeRepeating(nameof(Check), 2, 2);
        _link = GetComponent<OffMeshLink>();
    }
    
    void Check() {
        if (!_link.activated && PlayerPrefs.GetInt(openerID) == 1) {
            _link.activated = true;
        }
    }
}

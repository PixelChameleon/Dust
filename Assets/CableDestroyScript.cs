using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CableDestroyScript : MonoBehaviour {
    
    public Sprite brokenCable;
    public Sprite fixedCable;

    public void DestroyCable() {
        if (brokenCable == null) {
            return;
        }
        gameObject.GetComponent<Image>().sprite = brokenCable;
    }

    public void FixCable() {
        if (fixedCable == null) {
            return;
        }
        gameObject.GetComponent<Image>().sprite = fixedCable;
    }
    
}

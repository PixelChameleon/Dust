using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSpriteRender : MonoBehaviour {
    void Update() {
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}

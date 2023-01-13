using System.Collections;
using System.Collections.Generic;
using GlobalScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvestigateObjectScript : MonoBehaviour, IClickableGameObject {

    private DustManager _manager;
    private GameObject _box;
    public string text;
    public int hideDelay;
    public bool interactable = true;

    [Range(-5.0f, 5.0f)] 
    public float zOffset = 1.2f;

    private void Start() {
        _manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
    }
    
    public void OnClick(PlayerScript player) {
        if (!interactable) return;
        SpawnBox();
    }

    public void SpawnBox() {
        SpawnBox(text);
    }

    public void SpawnBox(string boxText) {
        if (_box != null) {
            return;
        }
        var parent = gameObject.transform;
        var pos = parent.position;
        pos.y = zOffset;
        var quat = Quaternion.Euler(90, 0, 0);
        _box = Instantiate(_manager.ClickableObjectTextBox, pos, quat);
        _box.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x, 3, pos.z + zOffset);
        _box.transform.rotation = quat;
        _box.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        _box.GetComponent<TextMeshPro>().text = boxText;
        Invoke(nameof(hide), hideDelay);
    }

    private void hide() {
        Destroy(_box);
        _box = null;
    }
}

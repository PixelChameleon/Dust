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

    private void Start() {
        _manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
    }
    
    public void OnClick(PlayerScript player) {
        if (_box != null) {
            return;
        }
        var parent = gameObject.transform;
        _box = Instantiate(_manager.ClickableObjectTextBox, parent);
        var pos = parent.position;
        pos.y = 1.2f;
        var quat = Quaternion.Euler(90, 0, 0);
        _box.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 1.2f, 1f);
        _box.transform.rotation = quat;
        _box.transform.localScale = new Vector3(0.01f, 0.02f, 0.01f);
        _box.GetComponent<TextMeshPro>().text = text;
        Invoke(nameof(hide), hideDelay);
    }

    private void hide() {
        Destroy(_box);
        _box = null;
    }
}

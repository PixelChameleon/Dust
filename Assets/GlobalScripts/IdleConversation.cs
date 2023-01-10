using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class IdleConversation : MonoBehaviour {

    public string[] messages;

    public GameObject TextBoxPrefab;

    public int DisplayDurationPerMessage = 5;
    public int MessageFrequency = 10;

    public bool random;

    private int _waitTime;
    private int _currentMsg;
    private GameObject _box;
    
    void Start() {
        var parent = gameObject.transform;
        InvokeRepeating(nameof(Converse), 1f, 1f);
        _box = Instantiate(TextBoxPrefab, parent);
        var pos = parent.position;
        pos.y = 1.2f;
        _box.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 1.2f, -4f);
        _box.SetActive(false);
    }

    private void Converse() {
        if (_box == null) {
            return;
        }
        _waitTime++;
        if (_waitTime > DisplayDurationPerMessage) {
            _box.SetActive(false);
            _box.GetComponent<TextMeshPro>().text = "";
        }
        if (_waitTime >= MessageFrequency) {
            _box.SetActive(true);
            _box.GetComponent<TextMeshPro>().text = NextMsg();
            _waitTime = 0;
        }
        
    }


    private string NextMsg() {
        var index = 0;
        if (random) {
            index = UnityEngine.Random.Range(0, messages.Length);
            return messages[index];
        }

        if (_currentMsg >= (messages.Length)) {
            _currentMsg = 0;
        }

        index = _currentMsg;
        _currentMsg++;
        return messages[index];
    }

}

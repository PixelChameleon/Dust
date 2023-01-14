using System.Collections;
using System.Collections.Generic;
using GlobalScripts;
using TMPro;
using UnityEngine;

public class PlayerConversation : MonoBehaviour {

    public GameObject PlayerTextBox;
    private GameObject _playerBox;
    public PlayerScript player;
    private PlayerConversationHolder _currentHolder = null;
    
    private int _waitTime;
    private bool _npcIsTalking;

    void Start() {
        var parent = gameObject.transform;
        _playerBox = Instantiate(PlayerTextBox, parent);
        var pos = parent.position;
        pos.y = -2f;
        _playerBox.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 15f, 3f);
        _playerBox.SetActive(false);
        InvokeRepeating(nameof(Timer), 1f, 1f);
    }
    
    public void ClickNext(PlayerConversationHolder holder) {
        if (_npcIsTalking) {
            return;
        }
        if (_currentHolder != null && _currentHolder != holder) {
            return;
        }

        if (_currentHolder == null && (!holder.hasTalkedToPlayer && !holder.canTalkAgain)) {
            _currentHolder = holder;
            Debug.Log("Started conversation with " + _currentHolder.gameObject.name);
            player.isTalking = true;
            _playerBox.SetActive(true);
            _playerBox.GetComponent<TextMeshPro>().text = _currentHolder.NextPlayerMsg();
            _npcIsTalking = true;
            return;
        }
        if (_currentHolder.Done()) {
            player.isTalking = false;
            _currentHolder.Box.SetActive(false);
            _playerBox.SetActive(false);
            _currentHolder.Reset();
            _currentHolder.hasTalkedToPlayer = true;
            Debug.Log("Finished conversation with " + _currentHolder.gameObject.name);
            _currentHolder = null;
            return;
        }
        
        _playerBox.SetActive(true);
        _playerBox.GetComponent<TextMeshPro>().text = _currentHolder.NextPlayerMsg();
    }

    private void Timer() {
        if (_currentHolder == null) {
            return;
        }
        if (_currentHolder.Done()) {
            return;
        }
        _waitTime++;
        if (_waitTime < _currentHolder.WaitTimeBetweenMessages) return;
        
        if (_playerBox.activeSelf) {
            _playerBox.SetActive(false);
            _currentHolder.Box.SetActive(true);
            _currentHolder.Box.GetComponent<TextMeshPro>().text = _currentHolder.NextNPCMessage();
            _npcIsTalking = true;
            Debug.Log("NPC talking");
            if (_currentHolder.Done()) {
                player.isTalking = false;
                _npcIsTalking = false;
                _currentHolder.Box.SetActive(false);
                _playerBox.SetActive(false);
                _currentHolder.Reset();
                Debug.Log("Finished conversation with " + _currentHolder.gameObject.name);
                _currentHolder = null;
            }
            _waitTime = 0;
            return;
        }
        if (_currentHolder.Box.activeSelf) {
            _currentHolder.Box.SetActive(false);
            _npcIsTalking = false;
            if (_currentHolder.Done()) {
                player.isTalking = false;
                _currentHolder.Box.SetActive(false);
                _playerBox.SetActive(false);
                _currentHolder.Reset();
                Debug.Log("Finished conversation with " + _currentHolder.gameObject.name);
                _currentHolder = null;
            }
            Debug.Log("NPC not talking");
        }
        _waitTime = 0;
    }
}

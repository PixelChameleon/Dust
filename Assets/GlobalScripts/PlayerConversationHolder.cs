using System;
using UnityEngine;

public class PlayerConversationHolder : MonoBehaviour {

    public string[] NPCMessages;
    public string[] PlayerReplies;
    public GameObject NPCTextBox;
    public int WaitTimeBetweenMessages;
    
    private int _playerMsgID;
    private int _npcMsgID;

    public GameObject Box;
    void Start() {
        if (NPCMessages.Length > PlayerReplies.Length) {
            Debug.LogError(gameObject.name + " has not enough player replies for provided NPC messages.");
        }
        var parent = gameObject.transform;
        Box = Instantiate(NPCTextBox, parent);
        var pos = parent.position;
        pos.y = 1.5f;
        Box.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 2f, -3f);
        Box.SetActive(false);
    }

    public bool Done() {
        return _playerMsgID > PlayerReplies.Length - 1|| _npcMsgID > NPCMessages.Length;
    }

    public void Reset() {
        _npcMsgID = 0;
        _playerMsgID = 0;
    }

    public string NextPlayerMsg() {
        var index = 0;
        index = _playerMsgID;
        _playerMsgID++;
        return PlayerReplies[index];
    }
    
    public string NextNPCMessage() {
        var index = 0;
        index = _npcMsgID;
        _npcMsgID++;
        return NPCMessages[index];
    }
    
}               
using UnityEngine;

public class PlayerConversationHolder : MonoBehaviour {

    public string[] NPCMessages;

    public string[] PlayerReplies;

    void Start() {
        if (NPCMessages.Length > PlayerReplies.Length) {
            Debug.LogError(gameObject.name + " has not enough player replies for provided NPC messages.");
        }
    }
    
}               
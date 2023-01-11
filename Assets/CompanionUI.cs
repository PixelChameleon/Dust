using GlobalScripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CompanionUI : MonoBehaviour {

    private PlayerScript _player;
    void Start() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
    }

    public void OnCompanionButtonClick() {
        var button = EventSystem.current.currentSelectedGameObject;
        if (_player.Companion == null) {
            return;
        }

        var script = _player.Companion.GetComponent<CompanionScript>();
        if (!_player.CompanionControlMode) {
            script.AIEnabled = false;
            _player.CompanionControlMode = true;
        }
        else {
            script.AIEnabled = true;
            _player.CompanionControlMode = false;
        }
    }
}

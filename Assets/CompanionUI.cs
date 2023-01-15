using GlobalScripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanionUI : MonoBehaviour {

    
    private Color _activeColor = new(0, 255, 0, 1);
    private Color _inactiveColor = new(255, 255, 255, 1f);

    public void OnCompanionButtonClick() {
        var button = EventSystem.current.currentSelectedGameObject;
        var player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
        if (player.Companion == null) {
            return;
        }

        var script = player.Companion.GetComponent<CompanionScript>();
        if (!player.CompanionControlMode) {
            script.AIEnabled = false;
            player.CompanionControlMode = true;
            var colorBlock = button.GetComponent<Button>().colors;
            colorBlock.normalColor = _activeColor;
            colorBlock.highlightedColor = _activeColor;
            colorBlock.pressedColor = _activeColor;
            button.GetComponent<Button>().colors = colorBlock;
        }
        else {
            script.AIEnabled = true;
            player.CompanionControlMode = false;
            var colorBlock = button.GetComponent<Button>().colors;
            colorBlock.normalColor = _inactiveColor;
            colorBlock.highlightedColor = _inactiveColor;
            colorBlock.pressedColor = _inactiveColor;
            button.GetComponent<Button>().colors = colorBlock;
        }
    }
}

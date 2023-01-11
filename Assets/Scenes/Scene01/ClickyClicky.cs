using GlobalScripts;
using UnityEngine;

namespace Scenes.Scene01 {
    public class ClickyClicky : MonoBehaviour, IClickableGameObject {
        public void OnClick(PlayerScript player) {
            if (!player.HasItem(0)) {
                Debug.Log("No key.");
                return;
            }
            Debug.Log("Woooooosh");
            player.manager.SwitchScene(2);
        }

    }
}

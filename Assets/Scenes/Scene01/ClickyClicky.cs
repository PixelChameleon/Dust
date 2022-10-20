using GlobalScripts;
using UnityEngine;

namespace Scenes.SampleScene {
    public class ClickyClicky : MonoBehaviour, IClickableGameObject {
    
        public void OnClick(PlayerScript player) {
            if (!player.HasItem(0, 1)) {
                Debug.Log("No key.");
                return;
            }
            Debug.Log("Woooooosh");
            player.manager.SwitchScene(2);
        }

    }
}

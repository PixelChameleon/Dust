using UnityEngine;

namespace GlobalScripts {
    public class CollectableItem : MonoBehaviour, IClickableGameObject {
        private bool _alreadyCollected;
        public int itemID;
        public int itemAmount;
        
        public void OnClick(PlayerScript player) {
            if (_alreadyCollected) return;
            _alreadyCollected = true;
            player.AddItem(player.manager.ItemRegistry[itemID], itemAmount);
        }
    }
}
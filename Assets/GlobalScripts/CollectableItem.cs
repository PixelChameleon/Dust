using UnityEngine;

namespace GlobalScripts {
    public class CollectableItem : MonoBehaviour, IClickableGameObject {
        private bool _alreadyCollected;
        public int itemID;

        public void OnClick(PlayerScript player) {
            DustManager manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
            Debug.Log("Received Item pick click^. Registry size: " + manager.ItemRegistry.Count);
            if (_alreadyCollected) return;
            _alreadyCollected = true;
            player.AddItem(manager.ItemRegistry[itemID]);
            player.InventoryUI.refreshInventory();
            Debug.Log("Picked up " + manager.ItemRegistry[itemID].Name);
        }
    }
}
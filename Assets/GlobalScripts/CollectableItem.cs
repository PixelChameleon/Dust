using UnityEngine;

namespace GlobalScripts {
    public class CollectableItem : MonoBehaviour, IClickableGameObject {
        private bool _alreadyCollected;
        public GameObject visibleSprite;
        public int itemID;

        public void OnClick(PlayerScript player) {
            DustManager manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
            Debug.Log("Received Item pick click^. Registry size: " + DustManager.ItemRegistry.Count);
            if (_alreadyCollected) return;
            _alreadyCollected = true;
            player.AddItem(DustManager.ItemRegistry[itemID]);
            player.InventoryUI.refreshInventory();
            Debug.Log("Picked up " + DustManager.ItemRegistry[itemID].Name);
            var popupscript = gameObject.GetComponent<InvestigateObjectScript>();
            if (popupscript != null) {
                popupscript.SpawnBox(DustManager.ItemRegistry[itemID].Name + " aufgenommen.");
            }

            if (visibleSprite != null) {
                visibleSprite.SetActive(false);
            }
        }
    }
}
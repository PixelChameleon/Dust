using GlobalScripts;
using UnityEngine;

namespace Scenes.Scene01 {
    public class StromkastenInteraction : MonoBehaviour, IClickableGameObject {
        public GameObject stromkasten;
        
        public void OnClick(PlayerScript player) {
            if (player.InventoryUI.PickedItem != DustManager.ItemRegistry[2]) {
                Debug.Log("Wrong item for interaction.");
                return;
            }
            player.canMove = false;
            stromkasten.SetActive(true);

        }

    }
}

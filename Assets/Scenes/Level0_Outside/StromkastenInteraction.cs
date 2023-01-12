using GlobalScripts;
using UnityEngine;

namespace Scenes.Scene01 {
    public class StromkastenInteraction : MonoBehaviour, IClickableGameObject {
        public GameObject stromkasten;
        
        public void OnClick(PlayerScript player) {
            if (player.InventoryUI.PickedItem != DustManager.ItemRegistry[2]) {
                GetComponent<InvestigateObjectScript>().SpawnBox("Ein Schraubenzieher wäre hier hilfreich...");
                return;
            }
            player.canMove = false;
            stromkasten.SetActive(true);

        }

    }
}

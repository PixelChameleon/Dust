using GlobalScripts;
using UnityEngine;

namespace Scenes.Scene01 {
    public class StromkastenInteraction : MonoBehaviour, IClickableGameObject {
        public GameObject stromkasten;
        
        public void OnClick(PlayerScript player) {
            if (player.InventoryUI.PickedItem != DustManager.ItemRegistry[2]) {
                return;
            }
            player.canMove = false;
            stromkasten.SetActive(!stromkasten.activeSelf);
            var position = player.camera.gameObject.transform.position;
            Vector3 pos = new(position.x, 2, position.z);
            pos.z += 4;
            //stromkasten.transform.SetPositionAndRotation(pos, stromkasten.transform.rotation);
            
        }

    }
}

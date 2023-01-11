using GlobalScripts;
using UnityEngine;

namespace Scenes.Scene01 {
    public class StromkastenInteraction : MonoBehaviour, IClickableGameObject {
        public GameObject stromkasten;
        
        public void OnClick(PlayerScript player) {
            if (!player.HasItem(2)) {
                return;
            }
            player.canMove = false;
            stromkasten.SetActive(!stromkasten.activeSelf);
            Vector3 pos = new(player.transform.position.x, 2, player.transform.position.z);
            pos.z += 4;
            stromkasten.transform.SetPositionAndRotation(pos, stromkasten.transform.rotation);
            
        }

    }
}

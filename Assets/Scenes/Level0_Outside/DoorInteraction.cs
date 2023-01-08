using GlobalScripts;
using UnityEngine;

namespace Scenes.Level0_Outside {
    public class DoorInteraction : MonoBehaviour, IClickableGameObject {

        public int sceneToLoadID = 3;
        
        public void OnClick(PlayerScript player) {
            GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>().SwitchScene(sceneToLoadID);
        }
    }
}

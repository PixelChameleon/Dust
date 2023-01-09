using GlobalScripts;
using UnityEngine;

namespace Scenes.Level0_Outside {
    public class LevelSwitcher : MonoBehaviour, IClickableGameObject {

        public int sceneToLoadID = 3;
        public bool isFixedCameraScene = false;
        public Vector3 fixedCameraLocation;

        private Vector3 _defaultCameraLocation = new (0, 20, 0);
        
        public void OnClick(PlayerScript player) {
            GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>().SwitchScene(sceneToLoadID);
            if (isFixedCameraScene) {
                player.camera.gameObject.transform.position = fixedCameraLocation;
            }
            else {
                player.camera.gameObject.transform.localPosition = _defaultCameraLocation;
            }
        }
    }
}

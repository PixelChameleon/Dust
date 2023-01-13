using GlobalScripts;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    public GameObject ExactRespawnPoint;

    public int respawnPointID;

    public Vector3 GetLocation() {
        return ExactRespawnPoint.gameObject.transform.position;
        //return ExactRespawnPoint.gameObject.transform.TransformVector(gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collide");
        var player = other.gameObject.GetComponent<PlayerScript>();
        if (player == null) {
            return;
        }
        player.CheckNewSpawnPoint(this);
    }
    
}

using GlobalScripts;
using UnityEngine;
using UnityEngine.AI;

public class CompanionScript : MonoBehaviour {

    public GameObject player;

    public int pathfindingCooldown = 5;

    public bool AIEnabled = true;

    private NavMeshAgent _agent;
    
    private void Start() {
        InvokeRepeating(nameof(Repath), 0, pathfindingCooldown);
        player = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<PlayerScript>().Companion = gameObject;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Repath() {
        if (!AIEnabled) {
            return;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < 1) {
            return;
        }
        Vector3 pos = player.transform.position;
        _agent.destination = new Vector3(pos.x + Random.Range(0, 1.2f), pos.y, pos.z +  + Random.Range(0, 1.2f));
    }
}

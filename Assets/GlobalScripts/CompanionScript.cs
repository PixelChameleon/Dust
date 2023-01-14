using GlobalScripts;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CompanionScript : MonoBehaviour {

    public GameObject player;

    public int pathfindingCooldown = 5;

    public bool AIEnabled = true;

    public GameObject textBoxPrefab;

    private NavMeshAgent _agent;
    private GameObject _box;

    
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

    public void Talk(string text, int time) {
        if (_box != null) {
            return;
        }
        var parent = gameObject.transform;
        Invoke(nameof(RemoveBox), time);
        _box = Instantiate(textBoxPrefab, parent);
        var pos = parent.position;
        pos.y = 1.2f;
        _box.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 1.2f, -4f);
        _box.GetComponent<TextMeshPro>().text = text;
    }

    private void RemoveBox() {
        Destroy(_box);
        _box = null;
    }
}

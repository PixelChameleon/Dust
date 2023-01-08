using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationFixer : MonoBehaviour {
    
    private NavMeshAgent _agent;
    void Start() {
        _agent = transform.parent.GetComponent<NavMeshAgent>();
        if (_agent == null) {
            Debug.LogError("Parent of " + gameObject.name + " has no NavMeshAgent!");
            gameObject.SetActive(false);
        }
    }
    
    void Update() {
        var target = _agent.destination;
        if (target.x < transform.parent.position.x) {
            GetComponent<Animator>().SetFloat("Horizontal", -1);
        }
        if (target.x > transform.parent.position.x) {
            GetComponent<Animator>().SetFloat("Horizontal", 1);
        }
    }
}

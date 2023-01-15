using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationScript : MonoBehaviour {

    private Animator _animator;

    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start() {
        _animator = GetComponent<Animator>();
        _agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        _animator.SetFloat("Horizontal", _agent.velocity.x);
        _animator.SetFloat("Vertical", _agent.velocity.z);
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }
}

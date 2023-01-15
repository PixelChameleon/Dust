using System;
using UnityEngine;

namespace GlobalScripts.combat {
    public class BulletScript : MonoBehaviour {

        public int damage;
        public ICombatant shooter;
        public LayerMask shooterMask;
        public Vector3 target;

        private void Start() {
            Invoke(nameof(Despawn), 5); 
        }

        private void Update() {
            transform.position =  Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.2f) {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision) {
            Debug.Log("Collided with " + collision.gameObject.name);
            ICombatant combatant = collision.gameObject.GetComponent<ICombatant>();
            if (combatant == null || combatant == shooter) {
                return;
            }
            combatant.Damage(damage);
            Destroy(gameObject);
        }

        public void Despawn() {
            Destroy(gameObject);
        }
        
    }
}
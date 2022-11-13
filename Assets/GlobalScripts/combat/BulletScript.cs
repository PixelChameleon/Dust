using System;
using UnityEngine;

namespace GlobalScripts.combat {
    public class BulletScript : MonoBehaviour {

        public int damage;
        private void OnCollisionEnter(Collision collision) {
            Debug.Log("Collided with " + collision.gameObject.name);
            ICombatant combatant = collision.gameObject.GetComponent<ICombatant>();
            if (combatant == null) {
                return;
            }
            combatant.Damage(damage);
            Destroy(gameObject);
        }
    }
}
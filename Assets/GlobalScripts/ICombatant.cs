using GlobalScripts.combat;
using UnityEngine;

namespace GlobalScripts {
    public interface ICombatant {
        public void Damage(int amount);
        public int GetHealth();

        public void Die(string deathmessage);

        public Weapon GetWeapon();

        public GameObject GetGameObject();
    }
}
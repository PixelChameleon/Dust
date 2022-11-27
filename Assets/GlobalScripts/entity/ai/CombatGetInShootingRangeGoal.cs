using GlobalScripts.combat;
using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class CombatGetInShootingRangeGoal : CombatAIGoal {
        private Vector3 _playerPos;
        
        public CombatGetInShootingRangeGoal(DustEntity entity) : base(entity) {
        }
        
        public override bool Check(LastCombatAction action) {
            _playerPos = action.Player.transform.position;
            float distance = Vector3.Distance(Entity.transform.position, _playerPos);
            return distance > Entity.Weapon.IdealRange || Physics.Linecast(_playerPos, Entity.transform.position);
        }

        public override bool Run() {
            var pos = Entity.gameObject.transform.position;
            Vector3 target = Entity.CombatManager.Player.transform.position;
            for (var i = 0; i < 100; i++) {
                target.x = +Random.Range(-5f, 5f);
                target.z = +Random.Range(-5f, 5f);
                target.y = pos.y;
                if (Vector3.Distance(Entity.transform.position, Entity.CombatManager.Player.transform.position) > 1.5f && !Physics.Linecast(_playerPos, target)) {
                    Debug.Log("Found position at " + Vector3.Distance(Entity.transform.position, Entity.CombatManager.Player.transform.position));
                    break;
                }
            }

            if (target == default) {
                return false;
            }
            Agent.destination = target;
            Entity.isBusy = true;
            return true;
        }


    }
}
using GlobalScripts.combat;
using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class CombatHideGoal : CombatAIGoal {
        private Vector3 _playerPos;
        
        public CombatHideGoal(DustEntity entity) : base(entity) {
        }

        public override bool Check(LastCombatAction action) {
            return Entity.GetHealth() <= 50f && !Physics.Linecast(action.Player.transform.position, Entity.transform.position);
        }

        public override bool Run() {
            var pos = Entity.gameObject.transform.position;
            Vector3 target = default;
            for (var i = 0; i < 100; i++) {
                target.x = +Random.Range(-5, 5);
                target.z = +Random.Range(-5, 5);
                target.y = pos.y;
                if (Physics.Linecast(_playerPos, Entity.transform.position)) {
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
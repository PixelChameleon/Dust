using System;
using GlobalScripts.combat;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GlobalScripts.entity.ai {
    public class CombatGetInShootingRangeGoal : CombatAIGoal {
        private Vector3 _playerPos;
        
        public CombatGetInShootingRangeGoal(DustEntity entity) : base(entity) {
        }
        
        public override bool Check(LastCombatAction action) {
            _playerPos = action.Player.transform.position;
            float distance = Vector3.Distance(Entity.transform.position, _playerPos);
            return distance > Entity.Weapon.IdealRange;
        }

        public override bool Run() {
            var pos = Entity.gameObject.transform.position;
            Vector3 target = Entity.CombatManager.Player.transform.position;
            for (var i = 0; i < 50; i++) {
                Random.InitState(DateTime.Now.Millisecond);
                Vector3 t = new() {
                    x = target.x + Random.Range(-3f, 3f),
                    z = target.z + Random.Range(-3f, 3f),
                    y = target.y
                };
                if (!Physics.Linecast(_playerPos, target)) {
                    target = t;
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
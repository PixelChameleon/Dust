using GlobalScripts.combat;
using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class CombatShootGoal : CombatAIGoal {
        
        private Vector3 _playerPos;

        public CombatShootGoal(DustEntity entity) : base(entity) {
        }

        public override bool Check(LastCombatAction action) {
            _playerPos = Entity.CombatManager.Player.transform.position;
            float distance = Vector3.Distance(Entity.transform.position, _playerPos);
            return Entity.Weapon.TurnsUsed <= Entity.Weapon.TurnsToReload && !Physics.Linecast(_playerPos, Entity.transform.position) /*&& distance < Entity.Weapon.IdealRange + 1*/;
        }

        public override bool Run() {
            Entity.CombatManager.Shoot(Entity, Entity.CombatManager.Player);
            return true;
        }
    }
}
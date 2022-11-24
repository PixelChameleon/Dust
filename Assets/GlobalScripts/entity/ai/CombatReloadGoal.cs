using GlobalScripts.combat;

namespace GlobalScripts.entity.ai {
    public class CombatReloadGoal : CombatAIGoal {
        public CombatReloadGoal(DustEntity entity) : base(entity) {
        }

        public override bool Check(LastCombatAction action) {
            return Entity.Weapon.TurnsUsed >= Entity.Weapon.TurnsToReload;
        }

        public override bool Run() {
            Entity.Weapon.TurnsUsed = 0;
            return true;
        }
    }
}
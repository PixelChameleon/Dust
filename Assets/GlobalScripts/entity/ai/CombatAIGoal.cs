using GlobalScripts.combat;

namespace GlobalScripts.entity.ai {
    
    public abstract class CombatAIGoal : AbstractAIGoal {

        protected CombatAIGoal(DustEntity entity) : base(entity) {
        }
    }
}
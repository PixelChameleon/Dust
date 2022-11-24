using GlobalScripts.combat;
using UnityEngine.AI;

namespace GlobalScripts.entity.ai {
    
    public abstract class CombatAIGoal {
        
        protected DustEntity Entity;
        protected NavMeshAgent Agent;

        protected CombatAIGoal(DustEntity entity) {
            Entity = entity;
            Agent = entity.GetComponent<NavMeshAgent>();

        }

        public abstract bool Check(LastCombatAction action);

        public abstract bool Run();
    }
}
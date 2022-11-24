using UnityEngine.AI;

namespace GlobalScripts.entity.ai {
    public abstract class AbstractAIGoal {

        protected DustEntity Entity;
        protected NavMeshAgent Agent;
        public bool ShouldCancelOtherGoals = false;
        
        protected AbstractAIGoal(DustEntity entity) {
            Entity = entity;
            Agent = entity.GetComponent<NavMeshAgent>();
        }

        public abstract bool ShouldStart();
        public abstract void OnStart();

        public abstract void Run();

        public abstract void OnStop();
        
    }
}
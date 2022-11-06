using UnityEngine.AI;

namespace GlobalScripts.entity.ai {
    public abstract class AbstractAIGoal {

        protected DustEntity Entity;
        protected NavMeshAgent Agent;
        
        protected AbstractAIGoal(DustEntity entity) {
            Entity = entity;
            Agent = entity.GetComponent<NavMeshAgent>();
        }

        public abstract void OnStart();

        public abstract void Run();

        public abstract void OnStop();

        public abstract void Cancel();
    }
}
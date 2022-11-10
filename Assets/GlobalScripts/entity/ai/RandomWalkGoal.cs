using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class RandomWalkGoal : AbstractAIGoal {


        public RandomWalkGoal(DustEntity entity) : base(entity) {
            
        }

        public override void OnStart() {
          
        }

        public override void Run() {
            var pos = Entity.gameObject.transform.position;
            Vector3 target;
            target.x = +Random.Range(-2, 2);
            target.z = +Random.Range(-2, 2);
            target.y = pos.y;
            Debug.Log("Moving to " + target);
            Agent.destination = target;
        }

        public override void OnStop() {
            
        }

        public override void Cancel() {
            
        }
    }
}
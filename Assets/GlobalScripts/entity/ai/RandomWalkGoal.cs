using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class RandomWalkGoal : AbstractAIGoal {


        public RandomWalkGoal(DustEntity entity) : base(entity) {
            
        }

        public override bool ShouldStart() {
            return true;
        }

        public override void OnStart() {
          
        }

        public override void Run() {
            var pos = Entity.gameObject.transform.position;
            Vector3 target;
            target.x = +Random.Range(-4, 4);
            target.z = +Random.Range(-4, 4);
            target.y = pos.y;
            Agent.destination = target;
        }

        public override void OnStop() {
            
        }
        
    }
}
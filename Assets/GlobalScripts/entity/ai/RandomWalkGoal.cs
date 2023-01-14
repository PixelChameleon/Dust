using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GlobalScripts.entity.ai {
    public class RandomWalkGoal : AbstractAIGoal {

        public int cooldown = 4;

        public RandomWalkGoal(DustEntity entity) : base(entity) {
            
        }

        public override bool ShouldStart() {
            return true;
        }

        public override void OnStart() {
          
        }

        public override void Run() {
            if (cooldown < 5) {
                cooldown++;
                return;
            }
            cooldown = 0;
            
            var pos = Entity.gameObject.transform.position;
            for (var i = 0; i < 32; i++) {
                Random.InitState(DateTime.Now.Millisecond);
                Vector3 target = new() {
                    x = pos.x + Random.Range(-3f, 3f),
                    z = pos.z + Random.Range(-3f, 3f),
                    y = pos.y
                };
                NavMeshPath navMeshPath = new NavMeshPath();
                if (Agent.CalculatePath(target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete || navMeshPath.status == NavMeshPathStatus.PathPartial) {
                    Agent.destination = target;
                    break;
                }
            }
        }

        public override void OnStop() {
            
        }
        
    }
}
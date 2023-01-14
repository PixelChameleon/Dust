using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GlobalScripts.entity.ai {
    public class GuardPositionGoal : AbstractAIGoal {

        private Vector3 _guardPosition;
        private float _maxDistance = 5f;

        public GuardPositionGoal(DustEntity entity) : base(entity) {

        }

        public override bool ShouldStart() {
            return true;
        }

        public override void OnStart() {

        }

        public override void Run() {
            var pos = Entity.gameObject.transform.position;
            if (Vector3.Distance(pos, _guardPosition) > _maxDistance) {
                Agent.destination = _guardPosition;
                return;
            }

            for (var i = 0; i < 16; i++) {
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
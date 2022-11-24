using UnityEngine;

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

            Vector3 target;
            target.x = +Random.Range(-1.5f, 1.5f);
            target.z = +Random.Range(-1.5f, 1.5f);
            target.y = pos.y;
            Debug.Log("Moving to " + target);
            Agent.destination = target;
        }

        public override void OnStop() {

        }
    }
}
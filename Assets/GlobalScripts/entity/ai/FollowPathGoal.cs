using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class FollowPathGoal : AbstractAIGoal {
        public float speed = 3;
        public float waitTime = 2f;
        public float turnspeed = 90;

        public Transform pathHolder;

        public FollowPathGoal(DustEntity entity) : base(entity) {
        }

        public override void OnStart() {
            Vector3[] waypoints = new Vector3[pathHolder.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathHolder.GetChild(i).position;
                waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }

            StartCoroutine(FollowPath(waypoints));
        }
        

        public override void Run() {
            throw new System.NotImplementedException();
        }

        public override void OnStop() {
            throw new System.NotImplementedException();
        }

        public override void Cancel() {
            throw new System.NotImplementedException();
        }
    }
}
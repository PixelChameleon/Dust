using System.Collections;
using UnityEngine;

namespace GlobalScripts.entity.ai {
    public class FollowPathGoal : AbstractAIGoal {
        
        private float speed = 3;
        private float waitTime = 2f;
        private float turnspeed = 90;
        
        private Vector3[] waypoints;
        private Transform transform;
  

        private int currentWaitTick = 0;

        public FollowPathGoal(DustEntity entity) : base(entity) {
        }

        public override bool ShouldStart() {
            return true;
        }

        public override void OnStart() {
            //transform = Entity.transform; - Crashed den Editor. Sehr spaßig.
            waypoints = new Vector3[Entity.dustEntityPathHolder.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = Entity.dustEntityPathHolder.GetChild(i).position;
                waypoints[i] = new Vector3(waypoints[i].x, Entity.transform.position.y, waypoints[i].z);
            }
            
        }
        
        public override void Run() {  // FollowPath and repeat
                Entity.transform.position = waypoints[0];
                int targetWaypointIndex = 1;
                Vector3 targetWaypoint = waypoints[targetWaypointIndex];
                transform.LookAt(targetWaypoint);
        
                while (true)
                {   // Movement
                    Entity.transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
                    if (transform.position == targetWaypoint) { // return to 0
                        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                        targetWaypoint = waypoints[targetWaypointIndex];
                        currentWaitTick++;
                        if (currentWaitTick > 3) {
                            TurnToFace(targetWaypoint);
                            currentWaitTick = 0;
                        }
                    }
                }
        
        
        }
        
        private void TurnToFace(Vector3 lookTarget) {   // Calculate the angle to face "LookTarget"
        
                Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
                float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
        
                // Angle always positive (absolute)
                while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) >0.05f )
                {
                    float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnspeed * Time.deltaTime);
                    transform.eulerAngles = Vector3.up * angle;
                }
        
        
            }

        // Wird nicht angezeigt da keine MonoBehaviour - ka wie wichtig wir das finden
        void OnDrawGizmos() { // sieht man nicht du arsch haha
                Vector3 startPosition = Entity.dustEntityPathHolder.GetChild(0).position;
                Vector3 prevoiusPosition = startPosition;
        
                foreach (Transform waypoint in Entity.dustEntityPathHolder)
                {
                    Gizmos.DrawSphere(waypoint.position, .3f);
                    Gizmos.DrawLine(prevoiusPosition, waypoint.position);
                    prevoiusPosition = waypoint.position;
        
                }
                Gizmos.DrawLine(prevoiusPosition, startPosition);
        }

        public override void OnStop() {
            throw new System.NotImplementedException();
        }
        
    }
}
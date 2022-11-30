using System.Collections;
using System.Collections.Generic;
using GlobalScripts;
using GlobalScripts.combat;
using GlobalScripts.entity;
using GlobalScripts.entity.ai;
using UnityEngine;

public class Guard : DustEntity
{
    public float speed = 3;
    public float waitTime = 2f;
    public float turnspeed = 90;

    public Transform pathHolder;


    void Start()
    {

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
        CombatAIGoals.Add(new CombatHideGoal(this));
        CombatAIGoals.Add(new CombatReloadGoal(this));
        CombatAIGoals.Add(new CombatShootGoal(this));
        CombatAIGoals.Add(new CombatGetInShootingRangeGoal(this));
    }

    IEnumerator FollowPath(Vector3[] waypoints) {   // FollowPath and repeat
        if (inCombat) {
            yield return null;
        }
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {   // Movement
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            { // return to 0
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));



            }

            yield return null;
        }


    }

    IEnumerator TurnToFace(Vector3 lookTarget) {   // Calculate the angle to face "LookTarget"
        if (inCombat) {
            yield return null;
        }
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        // Angle always positive (absolute)
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) >0.05f )
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnspeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }


    }



    void OnDrawGizmos()
    { // sieht man nicht du arsch haha
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 prevoiusPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevoiusPosition, waypoint.position);
            prevoiusPosition = waypoint.position;

        }
        Gizmos.DrawLine(prevoiusPosition, startPosition);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalScripts.entity;
using GlobalScripts.combat;
using GlobalScripts.entity.ai;
using GlobalScripts;

public class Guard : DustEntity
{
    public float speed = 3;
    public float waitTime = 2f;
    public float turnspeed = 90;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;

    public Transform pathHolder;
    PlayerScript player;
    Color orginialSpotlightColour;

    void Start()
    {
        base.Start();
        Weapon = DustManager.ItemRegistry[10] as Weapon;
        //IdleAIGoals.Add(1, new FollowPathGoal(this));
        CombatAIGoals.Add(new CombatHideGoal(this));
        CombatAIGoals.Add(new CombatReloadGoal(this));
        CombatAIGoals.Add(new CombatShootGoal(this));
        CombatAIGoals.Add(new CombatGetInShootingRangeGoal(this));


        const string Tag = "MainCamera";
        player = GameObject.FindGameObjectWithTag(Tag).transform.gameObject.GetComponent<PlayerScript>();
        viewAngle = spotlight.spotAngle;

        orginialSpotlightColour = spotlight.color;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    void Update()
    {
        base.Update();

        if (CanSeePlayer())
        {

            spotlight.color = Color.red;
            if (inCombat)
            {
                spotlight.enabled = false;

            }
        }
        else
        {
            spotlight.color = orginialSpotlightColour;
        }
    }

    bool CanSeePlayer()
    {
        if(inCombat)
        {
            return false;
        }
        if( Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleBetweenGuardAndPlayer < viewAngle /2f)
            {
                if(!Physics.Linecast(transform.position,player.transform.position,viewMask))
                {
                    new CombatManager(player, this);
                    return true;
                }
            }
        }

        
        return false;

    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
       
        // FollowPath and repeat
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            if (inCombat)
            {
                break;            
            }
            // Movement
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

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
       
        // Calculate the angle to face "LookTarget"

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
    { 
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 prevoiusPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevoiusPosition, waypoint.position);
            prevoiusPosition = waypoint.position;

        }
        Gizmos.DrawLine(prevoiusPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}

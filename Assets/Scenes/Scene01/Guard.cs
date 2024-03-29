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
    public float waitTime = 0f;
    public float turnspeed = 90;
    
    public float viewDistance;
    public LayerMask viewMask;
    //float viewAngle;

    public Transform pathHolder;
    public GameObject Triangle;
    PlayerScript player;
    Color orginialSpotlightColour;

    void Start() {
        base.Start();
        Weapon = DustManager.ItemRegistry[10] as Weapon;
        //IdleAIGoals.Add(1, new FollowPathGoal(this));
        CombatAIGoals.Add(new CombatHideGoal(this));
        CombatAIGoals.Add(new CombatReloadGoal(this));
        CombatAIGoals.Add(new CombatGetInShootingRangeGoal(this));
        CombatAIGoals.Add(new CombatShootGoal(this));


        const string Tag = "MainCamera";
        player = GameObject.FindGameObjectWithTag(Tag).transform.gameObject.GetComponent<PlayerScript>();

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
        WeaponAttachPoint.GetComponent<SpriteRenderer>().sprite = Weapon.Sprite;
    }

    void Update() {
        base.Update();

        if (CanSeePlayer()) {
        }
    }

    bool CanSeePlayer() {
        if(inCombat || player.inCombat) {
            return false;
        }   
        if( Vector3.Distance(transform.position, player.transform.position) < viewDistance) {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleBetweenGuardAndPlayer < 30.0f) {
                new CombatManager(player, this);
                Triangle.SetActive(false);
                return true;
            }
        }
        return false;
    }
    

    IEnumerator FollowPath(Vector3[] waypoints) {
       
        // FollowPath and repeat
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true) {
            if (inCombat) {
                break;            
            }
            // Movement
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            Vector2 selfPos2D = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPos2D = new Vector2(targetWaypoint.x, targetWaypoint.z);
            if (Vector2.Distance(selfPos2D, targetPos2D) < 1.0f) { // return to 0
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) {
       
        // Calculate the angle to face "LookTarget"

        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        // Angle always positive (absolute)
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) >0.05f ) {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnspeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }



    void OnDrawGizmos() { 
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 prevoiusPosition = startPosition;

        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevoiusPosition, waypoint.position);
            prevoiusPosition = waypoint.position;
        }
        Gizmos.DrawLine(prevoiusPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}

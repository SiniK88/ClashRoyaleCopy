using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ArtState { Move, Attack };

public class ArtSceneMovements : MonoBehaviour
{

    ArtState currentState = ArtState.Move;
    private NavMeshAgent agent;
    public List<Transform> waypoints;
    int nextPoint = 0;
    public float patrolSpeed = 1f;
    float waypointTolerance = 1f;
    int attackPower = 30; 
    public float hitTime = 2; //time in seconds between each hit
    float curTime = 0; //time in seconds since last hit

    private ArtScenestats towerhp;

    public Transform dust;
    public Transform hit;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        nextPoint = ClosestPoint();
        towerhp = waypoints[nextPoint].GetComponent<ArtScenestats>();
        //unitStats = gameObject.GetComponent<UnitStats>(); // unitStats from different script
        //agent.stoppingDistance = 0;
        dust.GetComponent<ParticleSystem>().enableEmission = false; 
        hit.GetComponent<ParticleSystem>().enableEmission = false;
    }

    void ContinuePatrol() {

        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[nextPoint].position);
        hit.GetComponent<ParticleSystem>().enableEmission = false;
    }


    bool CloseEnoughToWaypoint() {

        return Vector3.Distance(transform.position, waypoints[nextPoint].position)
            < waypointTolerance;
    }

    public int ClosestPoint() {
        // this works, finds the closest point
        int j = 0;
        var smallest = Vector3.Distance(agent.transform.position, waypoints[0].transform.position);
        for (var i = 1; i < waypoints.Count; i++) {
            var dist = Vector3.Distance(agent.transform.position, waypoints[i].transform.position);
            if (dist < smallest) {
                smallest = dist;
                j = i;
            }
        }
        return j;
    }

    void Update() {
        if (currentState == ArtState.Move) {
            dust.GetComponent<ParticleSystem>().enableEmission = true;
            if (CloseEnoughToWaypoint()) {
                dust.GetComponent<ParticleSystem>().enableEmission = false;
                agent.velocity = Vector3.zero;
                //agent.transform.position = waypoints[nextPoint].position;
                if (towerhp.towerMaxHP <= 0) {
                    // we have decided that last tower is element 2. Not the best way, could be good to redo this at some point
                    nextPoint = 2;
                    if (nextPoint > waypoints.Count - 1) {
                        nextPoint = 0;
                    }
                    towerhp = waypoints[nextPoint].GetComponent<ArtScenestats>();
                }
                
                curTime += Time.deltaTime;
                if (curTime >= hitTime && towerhp.towerMaxHP > 0) {
                    towerhp.towerMaxHP -= attackPower;
                    hit.GetComponent<ParticleSystem>().enableEmission = true;
                    curTime = curTime - hitTime;
                }

            }
            ContinuePatrol();


        }

        if (currentState == ArtState.Attack) {

            //attack();
            // minion stops
            // starts attacking and changes animation to attack animation
            // if waypoint "castle" is destroyed, starts to move towards next closest castle
        }
    }

    void attack() {
        print(" starts attacking");
        //int towerhp = waypointsGo[nextPoint].GetComponent<Towers>().towercurHP;
        //towerhp -= giantAttackPower;
        // start to attack waypoint that you are closest to
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, waypointTolerance);
    }






}

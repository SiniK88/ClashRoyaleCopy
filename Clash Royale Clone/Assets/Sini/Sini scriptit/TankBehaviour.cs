using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TankState { Move, Attack };

public class TankBehaviour : MonoBehaviour
{
    TankState currentState = TankState.Move;
    private NavMeshAgent agent;
    public List<Transform> waypoints;
    int nextPoint = 0;
    public float patrolSpeed = 1f;
    float waypointTolerance = 1f;
    public float hitTime = 2; //time in seconds between each hit
    float curTime = 0; //time in seconds since last hit

    private Towers towerhp;
    private UnitStats unitStats; 

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        nextPoint = ClosestPoint();
        towerhp = waypoints[nextPoint].GetComponent<Towers>();
        unitStats = gameObject.GetComponent<UnitStats>(); // unitStats from different script
        //agent.stoppingDistance = 0;
    }

    void ContinuePatrol() {
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[nextPoint].position);
    }

    void StopPatrol() {
       
    }

    bool CloseEnoughToWaypoint() {
        
        return Vector3.Distance(transform.position, waypoints[nextPoint].position)
            < waypointTolerance;
    }

    void StoppingDistance() {

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
        if (currentState == TankState.Move) {
            if (CloseEnoughToWaypoint()) {
                agent.velocity = Vector3.zero;
                //agent.transform.position = waypoints[nextPoint].position;
                if (towerhp.towerMaxHP <= 0) {
                    // we have decided that last tower is element 2. Not the best way, could be good to redo this at some point
                    nextPoint = 2;
                    if (nextPoint > waypoints.Count - 1) {
                        nextPoint = 0;
                    }
                    towerhp = waypoints[nextPoint].GetComponent<Towers>();
                }
                curTime += Time.deltaTime;
                if (curTime >= hitTime && towerhp.towerMaxHP > 0) {
                    towerhp.towerMaxHP -= unitStats.attackPower;
                    curTime = curTime - hitTime;
                }

            }
            ContinuePatrol();


            }

        if (currentState == TankState.Attack) {

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


}// class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MinionState { Move, Attack };
public class GiantScript : MonoBehaviour
{

    private NavMeshAgent agent;
    MinionState currentState = MinionState.Move;
    public List<Transform> waypoints;
    public List<GameObject> waypointsGo;
    int nextPoint = 0;
    public float patrolSpeed = 1f;
    float waypointTolerance = 1f;

    public int giantHP = 20;
    public int giantAttackPower = 20;
    public float hitTime = 2; //time in seconds between each hit
    float curTime = 0; //time in seconds since last hit


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        nextPoint = ClosestPoint();
        //vis = GetComponent<VisibilityChecker>();
    }


    void ContinuePatrol()
    {
        agent.speed = patrolSpeed;

        agent.SetDestination(waypoints[nextPoint].position);
    }

    bool CloseEnoughToWaypoint()
    {
        return Vector3.Distance(transform.position, waypoints[nextPoint].position)
            < waypointTolerance;
    }


    Transform FindClosest(List<Transform> targets)
    {
        Transform closest = targets[0];
        foreach (var t in targets)
        {
            if (Vector3.Distance(transform.position, t.position) <
                Vector3.Distance(transform.position, closest.position))
            {
                closest = t;
            }
        }
        return closest;
    }




    public int ClosestPoint()
    {

        // this works, finds the closest point
        int j = 0;
        var smallest = Vector3.Distance(agent.transform.position, waypoints[0].transform.position);

        for (var i = 1; i < waypoints.Count; i++)
        {
            var dist = Vector3.Distance(agent.transform.position, waypoints[i].transform.position);
            if (dist < smallest)
            {
                smallest = dist;
                j = i;
            }
        }
        return j;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == MinionState.Move)
        {

            if (CloseEnoughToWaypoint())
            {
                // lets try to get hp of tower we are close to
                currentState = MinionState.Attack;
                waypointsGo[nextPoint].GetComponent<Towers>().HurtEnemy(giantAttackPower);
                waypointsGo.Remove(waypointsGo[nextPoint]);


                // GetComponent<Towers>().
                // after tower is destroyed,search closest point again


                //nextPoint++;
                if (nextPoint > waypoints.Count - 1)
                {
                    nextPoint = 0;
                }
                // nextPoint %= waypoints.Count;
            }
            ContinuePatrol();
        }

        if(currentState == MinionState.Attack)
        {
            attack();

            // minion stops
            // starts attacking and changes animation to attack animation
            // if waypoint "castle" is destroyed, starts to move towards next closest castle
        }

    }


    void attack()
    {
        print(" starts attacking");
        
        //int towerhp = waypointsGo[nextPoint].GetComponent<Towers>().towercurHP;
        //towerhp -= giantAttackPower;
        // start to attack waypoint that you are closest to
    }



}

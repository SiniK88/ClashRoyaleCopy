using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankBehaviour : MonoBehaviour
{

    private NavMeshAgent agent;
    public List<GameObject> waypointsGo;
    public List<Transform> waypoints;
    int nextPoint = 0;
    public float patrolSpeed = 1f;
    float waypointTolerance = 1f;

    private Towers towerhp;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        nextPoint = ClosestPoint();
        towerhp = waypointsGo[nextPoint].GetComponent<Towers>();

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

}// class

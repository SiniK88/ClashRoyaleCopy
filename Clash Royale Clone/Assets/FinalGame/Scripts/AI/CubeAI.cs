using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeAI : MonoBehaviour
{
    public Transform waypoint;
    public NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoint.position);
    }

}

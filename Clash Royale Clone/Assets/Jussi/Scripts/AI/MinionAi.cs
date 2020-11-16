using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionAi : MonoBehaviour
{
    public float speed;

    NavMeshAgent minion;
    public Transform goal;

    private void Awake() {
        minion = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        minion.SetDestination(goal.position);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteHandler : MonoBehaviour
{
    IBehaviourStats aiScript;
    Animator[] animator;
    NavMeshAgent agent;

    private void Start() {

        animator = GetComponentsInChildren<Animator>();
        aiScript = transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<IBehaviourStats>();
        agent = transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<NavMeshAgent>();

    }

    private void Update() {
        transform.position = agent.transform.position;

        if (aiScript.GetState() == AIstate.Navigate || aiScript.GetState() == AIstate.Aggro) {
            animator[0].SetBool("Attack", false);
            animator[1].SetBool("Attack", false);
            var dir = aiScript.GetEnemyDirection();

            if(Mathf.Abs(dir.y) >= Mathf.Abs(dir.x)) {
                animator[0].SetFloat("Move Y", dir.y);
                animator[0].SetFloat("Move X", 0);
                animator[1].SetFloat("Move Y", dir.y);
                animator[1].SetFloat("Move X", 0);
            }
            else {
                animator[0].SetFloat("Move X", -dir.x);
                animator[0].SetFloat("Move Y", 0);
                animator[1].SetFloat("Move X", -dir.x);
                animator[1].SetFloat("Move Y", 0);
            }
        }

        if (aiScript.GetState() == AIstate.Attack) {
            
            animator[0].SetBool("Attack", true);
            animator[1].SetBool("Attack", true);
        }

    }

    
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteHandler : MonoBehaviour
{
    IBehaviourStats aiScript;
    Animator[] animator;
    NavMeshAgent agent;

    private void Awake() {

        animator = GetComponentsInChildren<Animator>();
        aiScript = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).GetComponent<IBehaviourStats>();
        agent = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).GetComponent<NavMeshAgent>();

    }

    private void Update() {
        transform.position = agent.transform.position;


        if (aiScript.GetState() == AIstate.Navigate || aiScript.GetState() == AIstate.Aggro) {
            if(Mathf.Abs(agent.velocity.y) >= Mathf.Abs(agent.velocity.x)) {
                animator[0].SetFloat("Move Y", agent.velocity.y);
                animator[0].SetFloat("Move X", 0);
                animator[1].SetFloat("Move Y", agent.velocity.y);
                animator[1].SetFloat("Move X", 0);
            }
            else {
                animator[0].SetFloat("Move X", -agent.velocity.x);
                animator[0].SetFloat("Move Y", 0);
                animator[1].SetFloat("Move X", -agent.velocity.x);
                animator[1].SetFloat("Move Y", 0);
            }
        }

        if (aiScript.GetState() == AIstate.Attack) {
            print("attack state works");
                animator[0].SetTrigger("Hit");
                animator[1].SetTrigger("Hit");
        }

    }
}



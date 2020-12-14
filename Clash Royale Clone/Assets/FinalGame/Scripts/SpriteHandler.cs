using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteHandler : MonoBehaviour
{
    public SpriteRenderer blueRend;
    public SpriteRenderer redRend;

    public Sprite Front;
    public Sprite Back;
    public Sprite Right;
    public Sprite Left;

    IBehaviourStats aiScript;

    //Lista public AnimationBlue
    Animator[] animator;

    //Lista public AnimationRed
    public Animation frontWalkRed;

    //Tai muutta layerin scriptissä.

    Transform[] transforms;
    public Transform movement;
    NavMeshAgent agent;

    private void Awake() {
        //if (animator == null)
        //animator.runtimeAnimatorController = Resources.Load("path_to_your_controller") as RuntimeAnimatorController;

        animator = GetComponentsInChildren<Animator>();
        aiScript = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).GetComponent<IBehaviourStats>();
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1);
        agent = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).GetComponent<NavMeshAgent>();
        blueRend.sprite = Front;
        redRend.sprite = Front;

        aiScript.GetState();

    }

    private void Update() {
        transform.position = movement.position;

        //Vector2 position = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).position; //ROTAATIo
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1); //ROTAATIo
        float rotationX = movement.eulerAngles.x;

        if (aiScript.GetState() == AIstate.Navigate || aiScript.GetState() == AIstate.Aggro) {
            //print("moving");
            // Blue players units behave correctly up on both cameras now, but not red players units. 

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

            /*
            // this one works weirdly. It turns units to left or right at the middle of the arena. 
            animator[0].SetFloat("Move X", movement.position.x);
            animator[0].SetFloat("Move Y", movement.position.y);
            animator[1].SetFloat("Move X", movement.position.x);
            animator[1].SetFloat("Move Y", movement.position.y);
            */

        }

        //Check if unit is moving ...
        //Liikkuu paitsi jos AI.State = Attack TAI Stun TAI NoState
        //Check if unit is attacking ...
        //Onko unit AI.State = Attack?

        //Rotaatio, bool isAttacking, bool isWalking -> oikea animaatio

        if (aiScript.GetState() == AIstate.Attack) {
            print("attack state works");
            //if (animator[0].gameObject.activeSelf) {
                animator[0].SetTrigger("Hit");
                animator[1].SetTrigger("Hit");

            //}

        }

    }

    //Check if unit is dying (comes as a notification)



}



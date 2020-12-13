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


    private void Awake() {
        //if (animator == null)
        //animator.runtimeAnimatorController = Resources.Load("path_to_your_controller") as RuntimeAnimatorController;

        animator = GetComponentsInChildren<Animator>();
        aiScript = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).GetComponent<IBehaviourStats>();
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1);
        blueRend.sprite = Front;
        redRend.sprite = Front;

        aiScript.GetState();


        
    }

    private void Update() {
        transform.position = movement.position;

        //Vector2 position = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).position; //ROTAATIo
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1); //ROTAATIo
        float rotationX = movement.eulerAngles.x;
        //Vector2 pos = gameObject.transform.position;

        // this one works weirdly

        if (aiScript.GetState() == AIstate.Navigate || aiScript.GetState() == AIstate.Aggro) {
            //print("moving");
            // for blend tree but some starnge behaviour there too
            animator[0].SetFloat("Move X", movement.position.x);
            animator[0].SetFloat("Move Y", movement.position.y);
            animator[1].SetFloat("Move X", movement.position.x);
            animator[1].SetFloat("Move Y", movement.position.y);

            /*
            if (rotationX >= -135 && rotationX <= -45) {
                animator[0].SetBool("WalkUp", false);
                animator[0].SetBool("WalkDown", false);
                animator[0].SetBool("WalkLeft", false);
                animator[0].SetBool("WalkRight", false);

                animator[1].SetBool("WalkUp", false);
                animator[1].SetBool("WalkDown", false);
                animator[1].SetBool("WalkLeft", false);
                animator[1].SetBool("WalkRight", false);
            }
            if (rotationX >= 45 && rotationX <= 135) { }
            animator[0].SetBool("WalkUp", false);
            animator[0].SetBool("WalkDown", false);
            animator[0].SetBool("WalkLeft", false);
            animator[0].SetBool("WalkRight",true); /// whyy do both players animations change direction. Does it somehow just search rotation of one of them??
        }
        if (rotationX >= -45 && rotationX <= 45) {
            animator[0].SetBool("WalkUp", false);
            animator[0].SetBool("WalkDown", false);
            animator[0].SetBool("WalkLeft", false);
            animator[0].SetBool("WalkRight", false);

            animator[1].SetBool("WalkUp", false);
            animator[1].SetBool("WalkDown", false);
            animator[1].SetBool("WalkLeft", false);
            animator[1].SetBool("WalkRight", false);
        } */
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



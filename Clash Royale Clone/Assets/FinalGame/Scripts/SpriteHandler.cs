using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Animation frontWalk;
    public Animation backWalk;
    public Animation rightkWalk;
    public Animation leftWalk; 

    //...

    //Lista public AnimationRed
    public Animation frontWalkRed;
    //...

    //Tai muutta layerin scriptissä.


    bool vertical;
    Transform[] transforms;
    public Transform movement;

    Vector3 previousPos;
    Vector3 currentPos;

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

    void OnEnable() {
        //animator.SetBool("IsWalkingUp", true);
        //animator.SetFloat("Move X", movement.position.x);
        //animator.SetFloat("Move Y", movement.position.y);
    }

    private void Update() {
        transform.position = movement.position;

        /*
        Vector2 inputVector = (Vector2.up * Input.GetAxis("Vertical")) + (Vector2.right * Input.GetAxis("Horizontal"));

        Vector2 animationVector = gameObject.transform.InverseTransformDirection(inputVector);

        var VelocityX = animationVector.x;
        var VelocityZ = animationVector.y;

        animator.SetFloat("Move X", VelocityX);
        animator.SetFloat("Move Y", VelocityZ);*/

        //Vector2 position = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).position; //ROTAATIo
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1); //ROTAATIo
        //Vector2 pos = gameObject.transform.position;

        if (aiScript.GetState() == AIstate.Navigate || aiScript.GetState() == AIstate.Aggro) {
            //print("moving");
            animator[0].SetFloat("Move X", movement.position.x);
            animator[0].SetFloat("Move Y", movement.position.y); 
            animator[1].SetFloat("Move X", movement.position.x);
            animator[1].SetFloat("Move Y", movement.position.y);
        }


        //Check if unit is moving ...
        //Liikkuu paitsi jos AI.State = Attack TAI Stun TAI NoState
        //Check if unit is attacking ...
        //Onko unit AI.State = Attack?

        //Rotaatio, bool isAttacking, bool isWalking -> oikea animaatio

        if (aiScript.GetState() == AIstate.Attack) {
            print("attack state works");
            if (animator[0].gameObject.activeSelf) {
                animator[0].SetTrigger("Hit");
                animator[1].SetTrigger("Hit");
                //animator.SetBool("IsAttackingUp", true);
                //animator.SetBool("IsWalkingUp", false); 
            }
        }
 



        //Check if unit is dying (comes as a notification)



    }

}

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
    public Animation frontWalk;
    //...

    //Lista public AnimationRed
    public Animation frontWalkRed;
    //...

    //Tai muutta layerin scriptissä.



    Transform[] transforms;
    public Transform movement;

    Vector3 previousPos;
    Vector3 currentPos;

    private void Awake() {
        aiScript = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1).GetComponent<IBehaviourStats>();
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1);
        blueRend.sprite = Front;
        redRend.sprite = Front;

        //aiScript.GetState();
        
    }

    private void Update() {
        transform.position = movement.position;

        //Check unit Rotation ...
            //gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1); ROTAATIo
        //Check if unit is moving ...
            //Liikkuu paitsi jos AI.State = Attack TAI Stun TAI NoState
        //Check if unit is attacking ...
            //Onko unit AI.State = Attack?

        //Rotaatio, bool isAttacking, bool isWalking -> oikea animaatio






        //Check if unit is dying (comes as a notification)



    }

}

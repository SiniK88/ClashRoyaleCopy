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

    Transform[] transforms;
    public Transform movement;

    Vector3 previousPos;
    Vector3 currentPos;

    private void Awake() {
        movement = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() - 1);
        
    }

    private void Update() {
        transform.position = movement.position;
    }

}

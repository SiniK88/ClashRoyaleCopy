using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerScript : MonoBehaviour
{

    Sprite knight;
    Sprite knightback; 
    GameObject placerObject;
    SpriteRenderer rend1, rend2;

    public GameObject parent;
    private List<GameObject> list1 = new List<GameObject>();


    void Start()
    {
        //placerObject = GameObject.Find("PlacerObject");



        //rend1 = GameObject.Find("PlacerObject").FindComponentInChildWithTag<SpriteRenderer>("P1");

        //rend = GetComponentInChildren<SpriteRenderer>();
        knight = Resources.Load<Sprite>("Clashknightfront");
        knightback = Resources.Load<Sprite>("Clashknightback");
    }



    public void ChangeSprite()
    {
        rend1.sprite = knight; 
        rend2.sprite = knightback;

    }

   

}

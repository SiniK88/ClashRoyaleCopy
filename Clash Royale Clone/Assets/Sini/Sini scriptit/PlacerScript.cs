using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerScript : MonoBehaviour
{

    Sprite knight;
    Sprite knightfront; 
    GameObject placerObject;
    SpriteRenderer rend, rend1, rend2;

    public GameObject parent;
    //private List<GameObject> list1 = new List<GameObject>();
    public List<GameObject> childrens = new List<GameObject>();
    SpriteRenderer[] children;

    void Start()
    {

        knight = Resources.Load<Sprite>("Clashspriteback");
        knightfront = Resources.Load<Sprite>("Clashknightfront");

        children = GetComponentsInChildren<SpriteRenderer>();


        //children = GetComponentsInChildren<Renderer>();

            //placerObject = GameObject.Find("PlacerObject");

        /*
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "P1")
            {
                rend1 = GetComponentInChildren<SpriteRenderer>();

            }




            if (child.gameObject.tag == "P2")
            {
                rend2 = GetComponentInChildren<SpriteRenderer>();
            }

        */

            //else rend2 = GetComponentInChildren<SpriteRenderer>();


            //rend1 = GameObject.Find("PlacerObject").FindComponentInChildWithTag<SpriteRenderer>("P1");

            //rend = GetComponentInChildren<SpriteRenderer>();


        

    }

    public void ChangeSprite()
    {
        //rend.sprite = knight; 

        //children[0].sprite = knight;
        //children[1].sprite = knightback;
        // fucking finally. Changes sprite depending of the tag. Same object gets different sprites for different tags
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].gameObject.tag == "P1")
            {
                children[i].sprite = knight;
            } else if(children[i].gameObject.tag == "P2")
            {
                children[i].sprite = knightfront;
            }



        }
        //rend1.sprite = knight; 
        //rend2.sprite = knightback;

    }

   

}

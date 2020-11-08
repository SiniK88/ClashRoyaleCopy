using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerScript : MonoBehaviour
{

    Sprite knight;
    GameObject placerObject;
    SpriteRenderer rend;
    

    void Start()
    {
        //placerObject = GameObject.Find("PlacerObject");
        rend = GetComponent<SpriteRenderer>();
        knight = Resources.Load<Sprite>("Clashknightfront");
       
    }



    public void ChangeSprite()
    {
        rend.sprite = knight; 
    }



}

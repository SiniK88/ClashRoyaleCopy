using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCard : MonoBehaviour
{
    Sprite knight; 
    GameObject placerObject;
    Renderer rend;
    public PlacerScript placerScript;
    public Camera CamP1, CamP2; 

    private void Awake()
    {
        placerObject = GameObject.Find("PlacerObject");
        rend = placerObject.GetComponent<Renderer>();
        //knight = Resources.Load<Sprite>("Clashknightfront");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            //Camera CamP1 = GameObject.Find("CameraP1");
            Ray ray = CamP1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "CardCube")
                {
                    Debug.Log("Card");
                    // do something to placerobject, call PlacerScript
                    placerScript.ChangeSprite();
                    placerObject.gameObject.tag = "P1"; 
                    //GetComponent<PlacerScript>().ChangeSprite(); 
                }
                else
                {
                    Debug.Log("No card");
                }
            }
        }


    }
}


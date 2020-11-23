using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    public int towerMaxHP = 100;
    public int towercurHP = 100;
    bool isDestroyed =  false; 


    // Update is called once per frame
    void Update()
    {
        if(towerMaxHP <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            //Destroy(gameObject);
            isDestroyed = true; 
        }
        

    }

    public void HurtEnemy(int damageToGive)
    {
        towerMaxHP -= damageToGive;
    }


    public void SetMaxHealth()
    {
        towerMaxHP = towercurHP;
    }

}



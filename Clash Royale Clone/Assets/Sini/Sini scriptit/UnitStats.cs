using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{

    public string unitTypeName;
    public int health = 10;
    public int playerID; // tähän int mielummin kuin string, koska se kevyempi
    public int attackPower = 2;
    bool alive = true;

    void Update() {
        if (!alive) {
            return;
        }
        if (health <= 0) {

            //GetComponent<MeshRenderer>().enabled = false;
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
            }
    }


    public void TakeDamge(int amount) {
        if (!alive) {
            return;
        }

        if(health <= 0) {
            
            //gameObject.SetActive(false);
            Destroy(gameObject);
            alive = false;
            //gameObject.SetActive(false);
        }
        health -= amount; 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtScenestats : MonoBehaviour
{

    public int towerMaxHP = 100;
    public int towercurHP = 100;
    bool isDestroyed = false;
    public Transform wineParticles;
    public float hitTime = 1.5f; //time in seconds between each hit
    float curTime = 0; //time in seconds since last hit


    // Update is called once per frame

    private void Start() {
        wineParticles.GetComponent<ParticleSystem>().enableEmission = false;
        //wineParticles.GetComponent<ParticleSystem>().Stop();
        //Invoke("StopEmitter", 2); 
    }

    void Update() {
        if (towerMaxHP <= 0) {
            wineParticles.GetComponent<ParticleSystem>().enableEmission = true;

            GetComponentInChildren<SpriteRenderer>().enabled = false;

            //Destroy(gameObject);
            isDestroyed = true;
            curTime += Time.deltaTime;
            if (curTime >= hitTime) {
                wineParticles.GetComponent<ParticleSystem>().loop = false;
                return;
            }
        }
    }

    public void HurtEnemy(int damageToGive) {
        towerMaxHP -= damageToGive;
    }


    public void SetMaxHealth() {
        towerMaxHP = towercurHP;
    }



}

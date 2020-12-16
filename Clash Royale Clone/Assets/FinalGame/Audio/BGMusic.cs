using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{

    public float startingTime = 60f;
    float currentTime;
   

    void Start()
    {
        currentTime = startingTime;
        AudioFW.PlayLoop("Clash_1st_Loop");
    }

    private void Update() {
        if (currentTime > 1) {
            currentTime -= Time.deltaTime;
        } else {

            AudioFW.StopLoop("Clash_1st_Loop");
            AudioFW.PlayLoop("Clash_Intense_Loop");
        }

        }


} // class

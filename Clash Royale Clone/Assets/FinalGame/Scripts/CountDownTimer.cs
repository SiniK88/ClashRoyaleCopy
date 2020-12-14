using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    //Game logic below
    public float startingTime;
    float currentTime;

    private void Start() {
        currentTime = startingTime;
    }

    private void Update() {

        if(currentTime > 1) {
            currentTime -= Time.deltaTime;
            UpdateClockVisuals();            
        } else {
            Destroy(this);
        }     
    }

    //Visualization below
    int currentTimeFloored;
    int displaySeconds = 0;
    int displayMinutes = 0;
    string time;
    public Text timerText;

    void UpdateClockVisuals() {
        currentTimeFloored = Mathf.FloorToInt(currentTime);
        displaySeconds = currentTimeFloored % 60;
        displayMinutes = Mathf.FloorToInt(currentTimeFloored / 60);
        if (displaySeconds >= 10) {
            time = displayMinutes.ToString() + ":" + displaySeconds.ToString();
        } else {
            time = displayMinutes.ToString() + ":0" + displaySeconds.ToString();
        }
        timerText.text = time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public delegate void TimerHitZero();
    public static event TimerHitZero OnTimerRunOut;

    //Game logic below
    public float startingTime;
    float currentTime;
    bool timerRunOut = false;

    private void Start() {
        currentTime = startingTime;
    }

    private void Update() {

        if(currentTime > 1) {
            currentTime -= Time.deltaTime;
            UpdateClockVisuals();            
        } else if (timerRunOut == false) {
            OnTimerRunOut();
            timerRunOut = true;
            StartCoroutine(SuddenDeathText(1.5f));
            
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

    IEnumerator SuddenDeathText(float time) {
        yield return new WaitForSeconds(time);
        timerText.text = "Sudden death!";
    }
}

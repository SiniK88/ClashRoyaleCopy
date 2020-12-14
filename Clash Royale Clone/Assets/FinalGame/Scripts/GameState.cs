using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //private void OnEnable() {
    //    TargetingManager.OnUnitsSetChange += RefreshUnitsSet;
    //}
    //private void OnDisable() {
    //    TargetingManager.OnUnitsSetChange -= RefreshUnitsSet;
    //}

    int blueTowers;
    int redTowers;
    int blueBigTower;
    int redBigTower;

    public Transform[] blueSmallTowers;
    public Transform[] redSmallTowers;
    public Transform[] blueBigTowers;    
    public Transform[] redBigTowers;

    private void Start() {
        blueTowers =    blueSmallTowers.Length;
        blueBigTower =  blueBigTowers.Length;
        redTowers =     redSmallTowers.Length;
        redBigTower =   redBigTowers.Length;

        foreach (Transform t in blueSmallTowers) {
            var notify = t.GetComponentInChildren<INotifyOnDestroy>();            
            notify.AddListener(OnBlueTowerDestroy);
        }
        foreach (Transform t in redSmallTowers) {
            var notify = t.GetComponentInChildren<INotifyOnDestroy>();
            notify.AddListener(OnRedTowerDestroy);
        }
        foreach (Transform t in blueBigTowers) {
            var notify = t.GetComponentInChildren<INotifyOnDestroy>();
            notify.AddListener(OnBlueBigTowerDestroy);
        }
        foreach (Transform t in redBigTowers) {
            var notify = t.GetComponentInChildren<INotifyOnDestroy>();
            notify.AddListener(OnRedBigTowerDestroy);
        }
    }

    public void OnBlueTowerDestroy() {
        blueTowers--;
        if(blueTowers <= 0) {
            print("Game Over, Red Wins");
        }
    }
    public void OnRedTowerDestroy() {
        redTowers--;
        if (redTowers <= 0) {
            print("Game Over, Blue Wins");
        }
    }
    public void OnBlueBigTowerDestroy() {
        blueBigTower--;
        if (blueBigTower <= 0) {
            print("Game Over, Red Wins");
        }
    }
    public void OnRedBigTowerDestroy() {
        redBigTower--;
        if (redBigTower <= 0) {
            print("Game Over, Blue Wins");
        }
    }
}

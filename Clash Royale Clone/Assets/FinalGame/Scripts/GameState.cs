using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private void OnEnable() {
        CountDownTimer.OnTimerRunOut += TimerRunOut;
    }
    private void OnDisable() {
        CountDownTimer.OnTimerRunOut -= TimerRunOut;
    }

    public GameObject gameOverScreen;
    public Text playerWonText;

    int blueTowers;
    int redTowers;
    int blueBigTower;
    int redBigTower;
    
    public Text bluePoints;
    public Text redPoints;

    public Transform[] blueSmallTowers;
    public Transform[] redSmallTowers;
    public Transform[] blueBigTowers;    
    public Transform[] redBigTowers;

    private void Awake() {
        gameOverScreen.SetActive(false);
    }

    private void Start() {
        bluePoints.text = "0";
        redPoints.text = "0";

        blueTowers = blueSmallTowers.Length;
        blueBigTower = blueBigTowers.Length;
        redTowers = redSmallTowers.Length;
        redBigTower = redBigTowers.Length;

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

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void TimerRunOut() {        

        //All the win conditions after timer has ran out, first compare the amount of small towers and then the combined health of all player towers.
        if(blueTowers > redTowers) {
            PlayerWon(1);
        } else if(redTowers > blueTowers) {
            PlayerWon(2);
        } else {
            int blueHealth = 0;
            int redHealth = 0;
            foreach(Transform tower in blueSmallTowers) {
                if(tower != null) {
                    int towerHealth = tower.GetComponentInChildren<IDamageable>().GetHealth();
                    blueHealth += towerHealth;
                }                
            }
            foreach (Transform tower in blueBigTowers) {
                if (tower != null) {
                    int towerHealth = tower.GetComponentInChildren<IDamageable>().GetHealth();
                    blueHealth += towerHealth;
                }
            }
            foreach (Transform tower in redSmallTowers) {
                if (tower != null) {
                    int towerHealth = tower.GetComponentInChildren<IDamageable>().GetHealth();
                    redHealth += towerHealth;
                }
            }
            foreach (Transform tower in redBigTowers) {
                if (tower != null) {
                    int towerHealth = tower.GetComponentInChildren<IDamageable>().GetHealth();
                    redHealth += towerHealth;
                }
            }

            if(blueHealth > redHealth) {
                PlayerWon(1);
            } else if(redHealth >= blueHealth) {
                PlayerWon(2);
            }
        }
    }

    public void StopGameActions() {
        Time.timeScale = 0;
    }

    public void PlayerWon(int i) {
        //StopGameActions();
        string playerWon;
        if(i == 1) {
            winner = new Color(0, 60, 255, 0f);            
            playerWon = "BLUE WON";
        } else {
            winner = new Color(255, 0, 0, 0f);
            playerWon = "RED WON";
        }
        playerWonText.text = playerWon;
        fade = true;
        gameOverScreen.SetActive(true);
    }

    public void OnBlueTowerDestroy() {
        blueTowers--;
        redPoints.text = (2 - blueTowers).ToString();
        if(blueTowers <= 0) {
            PlayerWon(2);
        }
    }
    public void OnRedTowerDestroy() {
        redTowers--;
        bluePoints.text = (2 - redTowers).ToString();
        if (redTowers <= 0) {
            PlayerWon(1);
        }
    }
    public void OnBlueBigTowerDestroy() {
        blueBigTower--;
        if (blueBigTower <= 0) {
            PlayerWon(2);
        }
    }
    public void OnRedBigTowerDestroy() {
        redBigTower--;
        if (redBigTower <= 0) {
            PlayerWon(1);
        }
    }

    private void Update() {
        if (fade) {
            if(timeLerp < 1) {
                timeLerp += Time.deltaTime / 5;
                fadeColorOnScreen();
            }
        }
    }
    bool fade = false;
    Color winner;
    float timeLerp = 0;
    public void fadeColorOnScreen() {
        winner.a = Mathf.Lerp(0, 0.7f, timeLerp);
        gameOverScreen.GetComponent<Image>().color = winner;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GameObject maskBlue;
    public GameObject maskRed;
    IDamageable healthScript;
    bool maxHealthRetrieved = false;

    int maxHealth;
    int currentHealth;
    [Range(0, 1)] float fillAmount;

    private void Update() {

        if (!maxHealthRetrieved) {
            healthScript = gameObject.transform.parent.GetComponentInChildren<IDamageable>();
            maxHealth = healthScript.GetHealth();
            currentHealth = maxHealth;
            maxHealthRetrieved = true;
        }

        currentHealth = healthScript.GetHealth();
        fillAmount = (float)currentHealth / maxHealth;
        print("MaxHealth: " + maxHealth);
        print("CurrentHealth: " + currentHealth);
        maskBlue.transform.localScale = new Vector3(fillAmount, 1, 1);
        maskRed.transform.localScale = new Vector3(fillAmount, 1, 1);
    }
}

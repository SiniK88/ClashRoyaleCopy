using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifyOnDestroy))]
public class TowerHealth : MonoBehaviour, IDamageable {

    [SerializeField] int health;
    
    public void Start() {
        health = GetComponent<IBehaviourStats>().GetHealth();
    }

    private void Update() {
        if(health <= 0) {
            Death();
        }
    }
    public void ApplyDamage(int damage) {
        health -= damage;
    }

    private void Death() {
        GetComponent<NotifyOnDestroy>().Notify();
    }
}

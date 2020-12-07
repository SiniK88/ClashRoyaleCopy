using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifyOnDestroy))] // optional - will add the other component automatically
public class TankHealth : MonoBehaviour, IDamageable {

    public int health = -1;
    public bool isFlying = false; 

    public void ApplyDamage(int amount) {
        health -= amount;
    }

    void Death() {
            GetComponent<NotifyOnDestroy>().Notify();
            Destroy(gameObject);
    }

    public void Start() {
        health = GetComponent<TankBehaviour>().health;
    }

    private void Update() {
        if(health <= 0) {
            Death();
        }
    }
} 

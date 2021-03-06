﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifyOnDestroy))]
[RequireComponent(typeof(UnitTargetInfo))]
public class UnitHealth : MonoBehaviour, IDamageable {

    [SerializeField] int health;
    public Transform particles;
    public Transform hitParticles; 

    public void Start() {
        health = GetComponent<IBehaviourStats>().GetHealth();
    }

    private void Update() {

        if (health <= 0) {
            Death();
        }
    }

    public void ApplyDamage(int damage) {
        health -= damage;

        var hitted = Instantiate(hitParticles, transform.position, transform.rotation);
        Destroy(hitted.gameObject, 1);
    }

    private void Death() {
        var boom = Instantiate(particles, transform.position, transform.rotation);
        Destroy(boom.gameObject, 1);
        GetComponent<NotifyOnDestroy>().Notify();
    }

    public int GetHealth() {
        return health;
    }
}

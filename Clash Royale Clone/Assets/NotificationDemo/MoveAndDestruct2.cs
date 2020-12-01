using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifyOnDestroy))] // optional - will add the other component automatically
public class MoveAndDestruct2 : MonoBehaviour {
    Vector3 initial;
    void Awake() {
        initial = transform.position;
        Invoke("Death", 3f);
    }

    void Death() {
        GetComponent<NotifyOnDestroy>().Notify();
        Destroy(gameObject);
    }

    void Update() {
        transform.position = initial + Vector3.right * Mathf.Sin(Time.time) * 3f;
    }
}

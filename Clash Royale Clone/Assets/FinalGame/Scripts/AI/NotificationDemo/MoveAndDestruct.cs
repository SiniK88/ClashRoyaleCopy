using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveAndDestruct : MonoBehaviour, INotifyOnDestroy {
    UnityAction onDestroy;
    Vector3 initial;

    void Awake() {
        initial = transform.position;
        Invoke("Death", 3f);
    }

    void Death() {
        onDestroy.Invoke();
        Destroy(gameObject);
    }

    public void AddListener(UnityAction callback) {
        onDestroy += callback;
    }
    public void RemoveListener(UnityAction callback) {
        onDestroy -= callback;
    }

    void Update() {
        transform.position = initial + Vector3.right * Mathf.Sin(Time.time) * 3f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotifyOnDestroy : MonoBehaviour, INotifyOnDestroy {
    UnityAction boom;
    public void AddListener(UnityAction callback) {
        boom += callback;
    }
    public void RemoveListener(UnityAction callback) {
        boom -= callback;
    }
    public void Notify() {
        boom.Invoke();
    }
}

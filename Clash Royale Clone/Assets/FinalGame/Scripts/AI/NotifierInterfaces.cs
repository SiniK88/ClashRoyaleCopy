using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface INotifyOnDestroy {
    void AddListener(UnityAction callback);
    void RemoveListener(UnityAction callback);
}
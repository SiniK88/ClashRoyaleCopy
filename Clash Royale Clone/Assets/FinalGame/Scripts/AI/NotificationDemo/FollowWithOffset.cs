using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithOffset : MonoBehaviour {
    //[SerializeField] Transform target;
    //[SerializeField] Vector3 offset;

    //void Start() {
    //    if (target) {
    //        var notify = target.GetComponent<INotifyOnDestroy>();
    //        if (notify != null) {
    //            notify.AddListener(TargetDead);
    //        }               
    //    }
    //}

    //public void TargetDead() {
    //    target = null;
    //    //AIState = Navigate();
    //    //Aggrorange
    //}

    //void Update() {
    //    if (target != null) {
    //        transform.position = target.position + offset;
    //    }
    //}

    //public void TargetDead() {
    //    currentState = AIstate.Navigate;
    //    potentialTargets = targetManager.FindPotentialTargets(transform.position, thisPlayer, targetTypes);
    //}

    //public void SelfDead() {
    //    targetManager.UnregisterUnit(gameObject.transform, thisPlayer);

    //}

    //public void ListenSelf() {
    //    var notify = GetComponent<INotifyOnDestroy>();
    //    if (notify != null) {
    //        notify.AddListener(SelfDead);
    //    }
    //}

    //public void UnListenSelf() {
    //    var notify = GetComponent<INotifyOnDestroy>();
    //    if (notify != null) {
    //        notify.RemoveListener(SelfDead);
    //    }
    //}

    //public void ListenTarget() {
    //    var notify = currentTarget.GetComponent<INotifyOnDestroy>();
    //    if (notify != null) {
    //        notify.AddListener(TargetDead);
    //    }
    //}

    //public void UnListenTarget() {
    //    var notify = currentTarget.GetComponent<INotifyOnDestroy>();
    //    if (notify != null) {
    //        notify.RemoveListener(TargetDead);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerListener : MonoBehaviour
{
    public GameObject leftBlue;
    public GameObject rightBlue;

    public GameObject leftRed;
    public GameObject rightRed;

    PlacementCursor[] cursors;

    private void Start() {
        leftBlue.GetComponentInChildren<INotifyOnDestroy>().AddListener(LeftBlue);
        rightBlue.GetComponentInChildren<INotifyOnDestroy>().AddListener(RightBlue);
        leftRed.GetComponentInChildren<INotifyOnDestroy>().AddListener(LeftRed);
        rightRed.GetComponentInChildren<INotifyOnDestroy>().AddListener(RightRed);

        cursors = FindObjectsOfType<PlacementCursor>();
    }

    public void LeftBlue() {
        foreach(PlacementCursor c in cursors) {
            c.blueLeftDestroyed = true;
            c.TowerDestroyed(2);
        }
    }
    public void RightBlue() {
        foreach (PlacementCursor c in cursors) {
            c.blueRightDestroyed = true;
            c.TowerDestroyed(2);
        }
    }
    public void LeftRed() {
        foreach (PlacementCursor c in cursors) {
            c.redLeftDestroyed = true;
            c.TowerDestroyed(1);
        }
    }
    public void RightRed() {
        foreach (PlacementCursor c in cursors) {
            c.redRightDestroyed = true;
            c.TowerDestroyed(1);
        }
    }
}

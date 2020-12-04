using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetingManager : MonoBehaviour {

    HashSet<Transform> player1Units = new HashSet<Transform>();
    HashSet<Transform> player2Units = new HashSet<Transform>();

    public void RegisterUnit(Transform t, int playerID) {

        if (playerID == 1) {
            if (player1Units.Contains(t)) {
                Debug.LogError("Trying to add the same unit twice");
            }
            player1Units.Add(t);
        } else if (playerID == 2) {
            if (player2Units.Contains(t)) {
                Debug.LogError("Trying to add the same unit twice");
            }
            player2Units.Add(t);
        } else {
            Debug.Log("Tried to add unit to Hashset but it doesn't have a playerID number");
        }
    }
    public void UnregisterUnit(Transform t, int playerID) {
        if (playerID == 1) {
            if (!player1Units.Contains(t)) {
                Debug.LogError("Trying to remove a unit from Hashset, but it isn't there");
            } else {
                player1Units.Remove(t);
            }

        } else if (playerID == 2) {
            if (!player2Units.Contains(t)) {
                Debug.LogError("Trying to remove a unit from Hashset, but it isn't there");
            } else {
                player2Units.Remove(t);
            }

        } else {
            Debug.Log("Tried to remove unit from Hashset but it doesn't have a playerID number");
        }
    }

    public HashSet<Transform> GetHashSet(int playerID) {
        if (playerID == 1) {
            return player1Units;
        } else if (playerID == 2) {
            return player2Units;
        } else {
            Debug.Log("Tried to get HashSet for a unit, but playerID is not 1 or 2");
            return null;
        }
    }


    //public IDamageable FindTarget(Vector3 position, bool targetP1Units, bool flyingAllowed, float maxDistance) {
    //    // from correct HashSet (based on player / flying)
    //    var units = targetP1Units ? player1Units : player2Units;
    //    // find min distance target
    //    // var minT = units.Min(t => Vector2.Distance(position, t.position));
    //    Transform minT = null;
    //    foreach (var t in units) {
    //        if (minT == null || Vector3.Distance(position, t.position) < Vector3.Distance(position, minT.position)) {
    //            minT = t;
    //        }
    //    }

    //    // check if inside maxDistance
    //    if (Vector3.Distance(position, minT.position) <= maxDistance)
    //        return minT.GetComponent<IDamageable>();
    //    return null;
    //}

}

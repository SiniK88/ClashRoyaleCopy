using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetingManager : MonoBehaviour {

    HashSet<Transform> player1Units = new HashSet<Transform>();
    HashSet<Transform> player2Units = new HashSet<Transform>();

    // TODO: take unit radius into account..?

    // every unit + building must register itself
    public void RegisterUnit(Transform t) {
        // TODO: check which player's unit this is


        if (player1Units.Contains(t))
            Debug.LogError("Trying to add same unit twice");
        player1Units.Add(t);
    }
    public void UnregisterUnit(Transform t) {
        // TODO: check which player's unit this is
        player1Units.Remove(t);
    }

    
    public IDamageable FindTarget(Vector2 position, bool targetP1Units, bool flyingAllowed, float maxDistance) {
        // from correct HashSet (based on player / flying)
        var units = targetP1Units ? player1Units : player2Units;
        // find min distance target
        // var minT = units.Min(t => Vector2.Distance(position, t.position));
        Transform minT = null;
        foreach (var t in units) {
            if (minT == null || Vector2.Distance(position, t.position) < Vector2.Distance(position, minT.position)) {
                minT = t;
            }
        }

        // check if inside maxDistance
        if (Vector2.Distance(position, minT.position) <= maxDistance)
            return minT.GetComponent<IDamageable>();
        return null; 
    }

}

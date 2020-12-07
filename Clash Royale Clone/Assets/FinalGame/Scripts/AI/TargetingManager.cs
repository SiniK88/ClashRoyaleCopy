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

    public HashSet<Transform> FindPotentialTargets(Vector3 position, int thisPlayer, int[] targetTypes) {

        HashSet<Transform> playerUnits = (thisPlayer == 1) ? player2Units : player1Units;

        HashSet<Transform> potentialTargets = new HashSet<Transform>();

        foreach (Transform unit in playerUnits) {
            int[] targetees = unit.GetComponent<UnitTargetInfo>().unitCharacteristics;
            bool hasDuplicates = targetees.Intersect(targetTypes).Any(); //Same as: foreach(int targetType in targetTypes) {foreach(int targeteeType in targetees) {if(targetType == targeteeType) { matchBetweenTarget = true;break;}}if (matchBetweenTarget) {break;}}
            if (hasDuplicates) {
                potentialTargets.Add(unit);
            }
        }

        return potentialTargets;
    }

    public Transform FindTarget(Vector3 position, HashSet<Transform> potentialTargets, bool towersOnly) {
        Transform target = null;

        if (towersOnly) { //Returns the closest Tower, if there are any
            foreach(Transform unit in potentialTargets) {
                if (unit.GetComponent<UnitTargetInfo>().unitCharacteristics.Contains(0)) {
                    if (target == null || Vector3.Distance(position, unit.position) < Vector3.Distance(position, target.position)) {
                        target = unit;
                    }
                }                
            }
        } else {
            foreach (Transform unit in potentialTargets) {
                if (target == null || Vector3.Distance(position, unit.position) < Vector3.Distance(position, target.position)) {
                        target = unit;
                }                
            }
        }

        return target;
    }
    //public HashSet<Transform> GetHashSet(int playerID) {
    //    if (playerID == 1) {
    //        return player1Units;
    //    } else if (playerID == 2) {
    //        return player2Units;
    //    } else {
    //        Debug.Log("Tried to get HashSet for a unit, but playerID is not 1 or 2");
    //        return null;
    //    }
    //}

}

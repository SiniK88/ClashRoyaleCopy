using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetingManager : MonoBehaviour {

    //public delegate void UnitsSetChange(Transform t, int playerID, bool addedUnit);
    //public static event UnitsSetChange OnUnitsSetChange;

    HashSet<Transform> player1Units = new HashSet<Transform>();
    HashSet<Transform> player2Units = new HashSet<Transform>();

    public TargetClass towerTargets = TargetClass.Tower;

    public void RegisterUnit(Transform t, int playerID) {

        if (playerID == 1) {
            if (player1Units.Contains(t)) {
                Debug.Log("Trying to add the same unit twice");
            }
            player1Units.Add(t);



        } else if (playerID == 2) {
            if (player2Units.Contains(t)) {
                Debug.Log("Trying to add the same unit twice");
            }
            player2Units.Add(t);
            
        } else {
            Debug.Log("Tried to add unit to Hashset but it doesn't have a playerID number");
        }
    }

    public void UnregisterUnit(Transform t, int playerID) {
        if (playerID == 1) {
            if (!player1Units.Contains(t)) {
                Debug.Log("Trying to remove a unit from Hashset, but it isn't there");
            } else {
                player1Units.Remove(t);
            }

        } else if (playerID == 2) {
            if (!player2Units.Contains(t)) {
                Debug.Log("Trying to remove a unit from Hashset, but it isn't there");
            } else {
                player2Units.Remove(t);
            }

        } else {
            Debug.Log("Tried to remove unit from Hashset but it doesn't have a playerID number");
        }
    }

    public Transform FindClosestTarget(Transform seeker, int thisPlayer, bool towersOnly) {
        Transform closest = null;
        HashSet<Transform> playerUnits = (thisPlayer == 1) ? player2Units : player1Units;

        if (towersOnly) {
            foreach (Transform unit in playerUnits) {
                TargetClass characteristcs = unit.GetComponent<UnitTargetInfo>().characteristcs;
                if ((towerTargets & characteristcs) != 0) {
                    if (closest == null || Vector3.Distance(seeker.position, unit.position) < Vector3.Distance(seeker.position, closest.position)) {
                        closest = unit;
                    }
                }
            }
        } else {
            TargetClass targets = seeker.GetComponent<UnitTargetInfo>().targets; //The seekers own BitMask containing all the possible target-types
            foreach (Transform unit in playerUnits) {
                TargetClass characteristcs = unit.GetComponent<UnitTargetInfo>().characteristcs;
                if ((targets & characteristcs) != 0) {
                    if (closest == null || Vector3.Distance(seeker.position, unit.position) < Vector3.Distance(seeker.position, closest.position)) {
                        closest = unit;
                    }
                }
            }
        }       
        
        return closest;
    }

    public List<Transform> FindAllTargetsWithinRadius(Transform seeker, int thisPlayer, float searchRadius) {
        List<Transform> unitsWithinRadius = new List<Transform>();
        HashSet<Transform> playerUnits = (thisPlayer == 1) ? player2Units : player1Units;
        TargetClass targets = seeker.GetComponent<UnitTargetInfo>().targets;

        foreach (Transform unit in playerUnits) {
            TargetClass characteristcs = unit.GetComponent<UnitTargetInfo>().characteristcs;
            if ((targets & characteristcs) != 0) { //Checks that the Seeker can actually target the unit
                IBehaviourStats behaviourScript = unit.GetComponent<IBehaviourStats>();
                float sizeRadius = behaviourScript.GetSizeRadius();
                if(Vector2.Distance(seeker.position, unit.position) <= searchRadius+sizeRadius) { //Checks that the unit is within a searchRadius centering from Seekers transform, eg. Fireball effect
                    unitsWithinRadius.Add(unit);
                }                
            }
        }
        return unitsWithinRadius;
    }   
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TowerBehaviour : MonoBehaviour {
    public string unitTypeName = "Tower";

    UnitTypeContainer unitContainer;
    List<UnitType> unitTypes;
    UnitType thisUnit;

    NavMeshAgent agent;

    public int thisPlayer = -1;
    public int enemyPlayer = -1;

    UnitTargetInfo targetInfo;

    TargetingManager targetManager;
    List<Transform> targets = new List<Transform>();
    Transform target;
    Vector3 targetPos = Vector3.zero;

    //The same data as in UnitType:    
    public int health;
    public int attackPower;
    public int attackSpeed;
    public float baseSpeed;
    public float sizeDiameter;
    public float viewDiameter;
    public int[] targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
    public int[] targeteeTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit

    private void Awake() {

        //Initialize the correct UnitType, and place it in variable thisUnit
        unitContainer = FindObjectOfType<UnitTypeContainer>();
        unitTypes = unitContainer.unitTypes;
        for (int i = 0; i < unitTypes.Count; i++) { if (unitTypes[i].unitTypeName.Equals(unitTypeName)) { thisUnit = unitTypes[i]; break; } } //Basic For-loop to find the correct UnitType from container

        //Initialize the UnitType stats to this instance
        health = thisUnit.health;
        attackPower = thisUnit.attackPower;
        attackSpeed = thisUnit.attackSpeed;
        baseSpeed = thisUnit.baseSpeed;
        sizeDiameter = thisUnit.sizeDiameter;
        viewDiameter = thisUnit.viewDiameter;
        targetTypes = thisUnit.targetTypes;
        targeteeTypes = thisUnit.targeteeTypes;

        //Initialize the NavMeshAgent and assign stats to the agent's parameters
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        agent.radius = sizeDiameter;

        //Assert the correct player id:
        thisPlayer = GetComponentInParent<PlayerID>().playerID;
        enemyPlayer = (thisPlayer == 1) ? 2 : 1;

        //Assert the correct TargetInformation to this unit
        targetInfo = GetComponent<UnitTargetInfo>();
        targetInfo.SetTargets(targetTypes);
        targetInfo.SetTargetees(targeteeTypes);

        //Add the unit's Transform to the current battlefield units Hashset:
        targetManager = FindObjectOfType<TargetingManager>();
        targetManager.RegisterUnit(gameObject.transform, thisPlayer);
    }

    private void Start() {
        //Assemble the starting targetlist. Note that this Unit's targetlist is dependant on the targetTypes[].
        RecalculateTargetList();

        //Select the closest target
        ClosestTarget();

        //Navigate towards closest target
        NavigateToClosest();
    }

    public void RecalculateTargetList() {
        HashSet<Transform> allTargets = targetManager.GetHashSet(enemyPlayer);
        foreach (Transform unit in allTargets) {
            print(unit.transform.parent.name);
            int[] targetees = unit.GetComponent<UnitTargetInfo>().targeteeTypes;
            bool hasDuplicates = targetees.Intersect(targetTypes).Any(); //foreach(int targetType in targetTypes) {foreach(int targeteeType in targetees) {if(targetType == targeteeType) { matchBetweenTarget = true;break;}}if (matchBetweenTarget) {break;}}
            if (hasDuplicates) {
                targets.Add(unit);
            }
        }
    }

    public Vector3 ClosestTarget() {
        if (targets.Count <= 0) {
            print("Tried to access closest target but the targets.List was empty");
            return Vector3.zero;
        }

        targetPos = targets[0].position; //Initialize the value to something
        float minDistance = Vector3.Distance(transform.position, targets[0].position); //Initialize the value to something      

        foreach (Transform t in targets) {
            if (Vector3.Distance(transform.position, t.position) < minDistance) {
                targetPos = t.position;
            }
        }
        return targetPos;
    }

    public void NavigateToClosest() {
        agent.SetDestination(targetPos);
    }
    private void OnDrawGizmos() {

        Color color;
        if (thisPlayer == 1) {
            color = Color.blue;
        } else if (thisPlayer == 2) {
            color = Color.red;
        } else {
            color = Color.magenta;
        }
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}

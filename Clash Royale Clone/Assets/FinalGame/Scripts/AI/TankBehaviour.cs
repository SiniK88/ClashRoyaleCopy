using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankBehaviour : MonoBehaviour {

    UnitTypeContainer unitContainer;
    List<UnitType> unitTypes;
    UnitType thisUnit;

    NavMeshAgent agent;

    public int player = -1;

    List<Transform> targets = new List<Transform>();

    //The same data as in UnitType:
    public string   unitTypeName = "Tank";
    public int      health;
    public int      attackPower;
    public int      attackSpeed;
    public float    baseSpeed;
    public float    sizeDiameter;
    public float    viewDiameter;
    public int[]    targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
    public int[]    targeteeTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit


    private void Awake() {

        //Initialize the correct UnitType, and place it in variable thisUnit
        unitContainer = FindObjectOfType<UnitTypeContainer>();
        unitTypes = unitContainer.unitTypes;        
        for(int i = 0; i < unitTypes.Count; i++) {if (unitTypes[i].unitTypeName.Equals(unitTypeName)){thisUnit = unitTypes[i];break;}} //Basic For-loop to find the correct UnitType from container

        //Initialize the UnitType stats to this instance
        health              = thisUnit.     health;
        attackPower         = thisUnit.     attackPower;
        attackSpeed         = thisUnit.     attackSpeed;
        baseSpeed           = thisUnit.     baseSpeed;
        sizeDiameter        = thisUnit.     sizeDiameter;
        viewDiameter        = thisUnit.     viewDiameter;
        targetTypes         = thisUnit.     targetTypes;
        targeteeTypes       = thisUnit.     targeteeTypes;


        //Initialize the NavMeshAgent and assign stats to the agent's parameters
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        agent.radius = sizeDiameter;

        //Assert the correct player id:
        player = 0; //Later this will be dealt for example by first 1) asserting the whole gameobject to a parent object Player0, and then 2) checking if the parent.gameobject name is "Player0" or "Player1" 

        //Assemble the starting targetlist and target

    }


}

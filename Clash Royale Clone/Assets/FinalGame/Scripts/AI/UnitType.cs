using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New UnitType", menuName = "Unit")]

public class UnitType : ScriptableObject {

    public string unitTypeName;

    public int health;

    public int attackPower;
    public int attackSpeed;

    public float baseSpeed;

    public float sizeDiameter;

    public float viewDiameter;

    public int[] targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
    public int[] targeteeTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
}

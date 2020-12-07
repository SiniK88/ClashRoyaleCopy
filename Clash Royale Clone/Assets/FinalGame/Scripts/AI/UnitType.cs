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

    public float sizeRadius;

    public float attackRadius;
    public float aggroRadius;

    public int[] targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
    public int[] unitCharacteristics; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
}

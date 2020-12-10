using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags] public enum TargetClass { Tower = 1, Ground = 2, Air = 4 };

[CreateAssetMenu(fileName = "New UnitType", menuName = "Unit")]

public class UnitType : ScriptableObject {

    public string unitTypeName;

    public int health;

    public int attackPower;
    public float attackPerSecond;

    public float baseSpeed;

    public float sizeRadius;

    public float attackRadius;
    public float aggroRadius;
    
    public TargetClass targets;
    public TargetClass characteristcs;
}

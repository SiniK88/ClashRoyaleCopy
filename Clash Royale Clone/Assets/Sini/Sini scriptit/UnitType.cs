using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New UnitType", menuName = "Unit")]

public class UnitType : ScriptableObject {

    public string unitTypeName;
    public int health;
    public int attackPower;
    public string playerID;
}

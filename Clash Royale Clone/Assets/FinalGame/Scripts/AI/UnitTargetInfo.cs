using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetInfo : MonoBehaviour
{
    public int[] targetTypes; //What type-of enemies the unit targets
    public int[] unitCharacteristics; //What type of enemies target this unit

    public void SetTargets(int[] _targetTypes) {
        targetTypes = _targetTypes;
    }

    public void SetTargetees(int[] _unitCharacteristics) {
        unitCharacteristics = _unitCharacteristics;
    }
}

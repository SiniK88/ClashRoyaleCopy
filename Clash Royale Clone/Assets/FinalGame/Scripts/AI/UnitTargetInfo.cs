using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetInfo : MonoBehaviour
{
    public TargetClass targets;
    public TargetClass characteristcs;

    public void SetTargetEnum(TargetClass _targets) {
        targets = _targets;
    }
    public void SetCharacteristicsEnum(TargetClass _characteristcs) {
        characteristcs = _characteristcs;
    }
}

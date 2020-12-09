using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable {
    void ApplyDamage(int amount);
    int GetHealth();
}
public interface IBehaviourStats {
    int GetHealth();
}




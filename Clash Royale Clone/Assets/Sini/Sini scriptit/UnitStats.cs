using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{

    public string unitTypeName;
    public int health;
    public int attackPower;
    public string playerID;

    private Transform transform;

    private void Awake() {

    }

    void Update() {
            if (health <= 0) {

                //GetComponent<MeshRenderer>().enabled = false;
                Destroy(this.gameObject);
            }
    }


}

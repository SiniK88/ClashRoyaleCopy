using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarrelBehaviour : MonoBehaviour {
    public string unitTypeName = "Barrel";

    UnitTypeContainer unitContainer;
    List<UnitType> unitTypes;
    UnitType thisUnit;

    public GameObject barrelMinion;

    public int thisPlayer = -1;
    public int enemyPlayer = -1;

    //The same data as in UnitType:    
    public int health;
    public int attackPower;
    public float attackPerSecond;
    public float baseSpeed;
    public float sizeRadius;
    public float attackRadius;
    public float aggroRadius;

    public TargetClass targets;
    public TargetClass characteristcs;

    UnitTargetInfo targetInfo;

    TargetingManager targetManager;

    private void Awake() {

        //Initialize the correct UnitType, and place it in variable thisUnit
        unitContainer = FindObjectOfType<UnitTypeContainer>();
        unitTypes = unitContainer.unitTypes;
        for (int i = 0; i < unitTypes.Count; i++) { if (unitTypes[i].unitTypeName.Equals(unitTypeName)) { thisUnit = unitTypes[i]; break; } } //Basic For-loop to find the correct UnitType from container

        //Initialize the UnitType stats to this instance
        health = thisUnit.health;
        attackPower = thisUnit.attackPower;
        attackPerSecond = thisUnit.attackPerSecond;
        baseSpeed = thisUnit.baseSpeed;
        sizeRadius = thisUnit.sizeRadius;
        attackRadius = thisUnit.attackRadius;
        aggroRadius = thisUnit.aggroRadius;
        targets = thisUnit.targets;
        characteristcs = thisUnit.characteristcs;

        //Assert the correct player id:
        thisPlayer = GetComponentInParent<PlayerID>().playerID;
        enemyPlayer = (thisPlayer == 1) ? 2 : 1;

        //Assert the correct TargetInformation to this unit
        targetInfo = GetComponent<UnitTargetInfo>();
        targetInfo.SetTargetEnum(targets);
        targetInfo.SetCharacteristicsEnum(characteristcs);

        //Add the unit's Transform to the current battlefield units Hashset:
        targetManager = FindObjectOfType<TargetingManager>();
    }

    private void Start() {
        StartCoroutine(FireballEffect(1f));
    }

    IEnumerator FireballEffect(float visualsTimer) {
        //Visuals or particleFX;


        List<Transform> enemyUnits = targetManager.FindAllTargetsWithinRadius(gameObject.transform, thisPlayer, attackRadius);

        foreach (Transform unit in enemyUnits) {
            IDamageable healthScript = unit.GetComponent<IDamageable>();
            if (healthScript != null) {
                healthScript.ApplyDamage(attackPower);
            }
        }

        for(int i = 0; i <= 2; i++) { //Spawns the minion in a row, each one unit to the right from the previous
            Instantiate(barrelMinion, new Vector3(transform.position.x + i*0.5f, transform.position.y-1, transform.position.z), Quaternion.identity, gameObject.transform.parent.parent);
        }

        GameObject parent = transform.parent.gameObject;
        yield return new WaitForSeconds(visualsTimer);
        Destroy(parent);
    }
}

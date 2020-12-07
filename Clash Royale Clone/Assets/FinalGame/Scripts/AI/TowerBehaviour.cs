using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TowerBehaviour : MonoBehaviour {
    public string unitTypeName = "Tower";

    //private void OnEnable() {
    //    TargetingManager.OnModifyUnitsCol += ModifyTargetCol;
    //}
    //private void OnDisable() {
    //    TargetingManager.OnModifyUnitsCol -= ModifyTargetCol;
    //}

    UnitTypeContainer unitContainer;
    List<UnitType> unitTypes;
    UnitType thisUnit;

    NavMeshAgent agent;

    public int thisPlayer = -1;
    public int enemyPlayer = -1;

    UnitTargetInfo targetInfo;

    TargetingManager targetManager;
    HashSet<Transform> potentialTargets;
    Transform currentTarget;
    Transform closestTarget;

    //The same data as in UnitType:    
    public int health;
    public int attackPower;
    public int attackSpeed;
    public float baseSpeed;
    public float sizeRadius;
    public float attackRadius;
    public float aggroRadius;
    public int[] targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
    public int[] unitCharacteristics; //0 = Tower, 1 = GroundUnit, 2 = AirUnit

    float attackRad;
    float reachRad;

    float attackTimer;
    float timer = 60f;

    enum AIstate { Navigate, Aggro, Attack, Stun, NoState };
    AIstate currentState;
    AIstate previousState;

    private void Awake() {

        //Initialize the correct UnitType, and place it in variable thisUnit
        unitContainer = FindObjectOfType<UnitTypeContainer>();
        unitTypes = unitContainer.unitTypes;
        for (int i = 0; i < unitTypes.Count; i++) { if (unitTypes[i].unitTypeName.Equals(unitTypeName)) { thisUnit = unitTypes[i]; break; } } //Basic For-loop to find the correct UnitType from container

        //Initialize the UnitType stats to this instance
        health = thisUnit.health;
        attackPower = thisUnit.attackPower;
        attackSpeed = thisUnit.attackSpeed;
        baseSpeed = thisUnit.baseSpeed;
        sizeRadius = thisUnit.sizeRadius;
        attackRadius = thisUnit.attackRadius;
        aggroRadius = thisUnit.aggroRadius;
        targetTypes = thisUnit.targetTypes;
        unitCharacteristics = thisUnit.unitCharacteristics;

        //Initialize some private stats as well:
        attackRad = attackRadius; //The tolrance distance for when the enemy starts "Attacking" the target instead of "Navigating" towards it.
        reachRad = attackRad * 1.2f; //Once the "Attacking" has started, we need to enlargen the attackDiameter, so that there won't occur any "following jitter", where the unit stops, but has to start navigating agian, because the enemy is out-of-reach on the next update.

        //Initialize the NavMeshAgent and assign stats to the agent's parameters
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        agent.radius = sizeRadius;

        //Assert the correct player id:
        thisPlayer = GetComponentInParent<PlayerID>().playerID;
        enemyPlayer = (thisPlayer == 1) ? 2 : 1;

        //Assert the correct TargetInformation to this unit
        targetInfo = GetComponent<UnitTargetInfo>();
        targetInfo.SetTargets(targetTypes);
        targetInfo.SetTargetees(unitCharacteristics);

        //Add the unit's Transform to the current battlefield units Hashset:
        targetManager = FindObjectOfType<TargetingManager>();
        targetManager.RegisterUnit(gameObject.transform, thisPlayer);

        //Initialize the Unit States, so the game will orient the unit correctly starting from Update()
        currentState = AIstate.NoState;
        previousState = AIstate.NoState;
    }

    private void Start() {
        //Initialize the potentialTargets hashset
        potentialTargets = targetManager.FindPotentialTargets(transform.position, thisPlayer, targetTypes);
        currentTarget = targetManager.FindTarget(transform.position, potentialTargets, true);
        if (currentTarget == null) {
            Debug.Log("Target was null in start");
        }
    }

    private void Update() {

        if (currentState != previousState) { //When state changes from the previous frame, we should handle it's destination only once, instead of on every frame
            switch (currentState) {
                case AIstate.Navigate:
                    currentTarget = targetManager.FindTarget(transform.position, potentialTargets, true);
                    NavigateToClosest(currentTarget);
                    break;
                case AIstate.Aggro:
                    NavigateToClosest(currentTarget);
                    break;
                case AIstate.Attack:
                    //Listen to DeathNotification from target
                    attackTimer = timer;
                    agent.isStopped = true;
                    break;
                case AIstate.Stun:
                    agent.isStopped = true;
                    break;
                case AIstate.NoState:
                    agent.isStopped = true;
                    break;
            }
        }

        previousState = currentState;

        switch (currentState) { //We could nest this switch inside a Update timer, so that these commands will only be executed every 4 frames , or something like that

            case AIstate.Navigate:
                Navigate();
                break;
            case AIstate.Aggro:
                Aggro();
                break;
            case AIstate.Attack:
                Attack();
                break;
            case AIstate.Stun:
                print(gameObject.name + " is Stunned");
                break;
            case AIstate.NoState:
                NoState();
                break;
        }
    }

    public void Navigate() {
        closestTarget = targetManager.FindTarget(transform.position, potentialTargets, false);

        if (Vector3.Distance(transform.position, closestTarget.position) < attackRad) {
            currentState = AIstate.Attack;
            currentTarget = closestTarget;
        } else if (Vector3.Distance(transform.position, closestTarget.position) < aggroRadius) {
            currentState = AIstate.Aggro;
            currentTarget = closestTarget;
        }
    }

    public void Aggro() {
        closestTarget = targetManager.FindTarget(transform.position, potentialTargets, false);

        if (Vector3.Distance(transform.position, closestTarget.position) < attackRad) {
            currentState = AIstate.Attack;
            currentTarget = closestTarget;
        } else if (Vector3.Distance(transform.position, closestTarget.position) > aggroRadius + 0.1f) { //The enemy target got away from the range, with a slight 0.1f buffer
            currentState = AIstate.Navigate;
            //DeListen target death notification
        }
    }

    public void Attack() {
        //No need to check the closest target, since we should lock our attention to one troop at least as long as it's in the reachRadius

        if (Vector2.Distance(transform.position, currentTarget.position) < reachRad) {
            //Attack the target
            attackTimer -= attackSpeed;
            if (attackTimer <= 0) {
                currentTarget.GetComponent<IDamageable>().ApplyDamage(attackPower);
                attackTimer = timer;
            }

        } else if (Vector2.Distance(transform.position, currentTarget.position) < aggroRadius) {
            currentState = AIstate.Aggro;
        } else {
            currentState = AIstate.Navigate;
            //DeListen target death notification
        }
    }

    public void NoState() {
        StartCoroutine(StunCooldown(1f));
    }

    IEnumerator StunCooldown(float time) {
        currentState = AIstate.Stun;
        yield return new WaitForSeconds(time);
        currentState = AIstate.Navigate;
    }

    public void NavigateToClosest(Transform target) {
        if (target != null) {
            agent.SetDestination(target.position);
        } else {
            Debug.Log(gameObject.name + " tried to Navigate to target, but it was null");
        }
    }

    private void OnDrawGizmos() {
        Color color;
        if (thisPlayer == 1) {
            color = Color.blue;
        } else if (thisPlayer == 2) {
            color = Color.red;
        } else {
            color = Color.magenta;
        }
        Gizmos.color = color;

        if (currentTarget != null) {
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }

        Color stateColor = Color.magenta;

        switch (currentState) {

            case AIstate.Navigate:
                stateColor = Color.green;
                break;
            case AIstate.Aggro:
                stateColor = Color.yellow;
                break;
            case AIstate.Attack:
                stateColor = Color.red;
                break;
            case AIstate.Stun:
                stateColor = Color.grey;
                break;
            case AIstate.NoState:
                stateColor = Color.black;
                break;
        }

        Gizmos.color = stateColor;

        Gizmos.DrawWireSphere(transform.position, 0.4f);
    }
}

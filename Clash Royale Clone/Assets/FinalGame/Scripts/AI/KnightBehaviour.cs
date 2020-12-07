//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.AI;

//public class KnightBehaviour : MonoBehaviour {
//    public string unitTypeName = "Knight";

//    private void OnEnable() {
//        TargetingManager.OnModifyUnitsCol += ModifyTargetCol;
//    }
//    private void OnDisable() {
//        TargetingManager.OnModifyUnitsCol -= ModifyTargetCol;
//    }

//    UnitTypeContainer unitContainer;
//    List<UnitType> unitTypes;
//    UnitType thisUnit;

//    NavMeshAgent agent;

//    public int thisPlayer = -1;
//    public int enemyPlayer = -1;

//    UnitTargetInfo targetInfo;

//    TargetingManager targetManager;
//    HashSet<Transform> targets = new HashSet<Transform>();
//    HashSet<Transform> towers = new HashSet<Transform>();
//    Transform towerTarget = null;
//    Transform enemyTarget = null;
//    Transform currentTarget = null;

//    //The same data as in UnitType:    
//    public int health;
//    public int attackPower;
//    public int attackSpeed;
//    public float baseSpeed;
//    public float sizeRadius;
//    public float attackRadius;
//    public float aggroRadius;
//    public int[] targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
//    public int[] targeteeTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit

//    float attackRad;
//    float reachRad;

//    float attackTimer;
//    float timer = 60f;

//    enum AIstate { Navigate, Aggro, Attack, Stun, NoState };
//    AIstate currentState = AIstate.NoState;
//    AIstate previousState = AIstate.NoState;

//    private void Awake() {

//        //Initialize the correct UnitType, and place it in variable thisUnit
//        unitContainer = FindObjectOfType<UnitTypeContainer>();
//        unitTypes = unitContainer.unitTypes;
//        for (int i = 0; i < unitTypes.Count; i++) { if (unitTypes[i].unitTypeName.Equals(unitTypeName)) { thisUnit = unitTypes[i]; break; } } //Basic For-loop to find the correct UnitType from container

//        //Initialize the UnitType stats to this instance
//        health = thisUnit.health;
//        attackPower = thisUnit.attackPower;
//        attackSpeed = thisUnit.attackSpeed;
//        baseSpeed = thisUnit.baseSpeed;
//        sizeRadius = thisUnit.sizeRadius;
//        attackRadius = thisUnit.attackRadius;
//        aggroRadius = thisUnit.aggroRadius;
//        targetTypes = thisUnit.targetTypes;
//        targeteeTypes = thisUnit.targeteeTypes;

//        //Initialize some private stats as well:
//        attackRad = attackRadius; //The tolrance distance for when the enemy starts "Attacking" the target instead of "Navigating" towards it.
//        reachRad = attackRad * 1.2f; //Once the "Attacking" has started, we need to enlargen the attackDiameter, so that there won't occur any "following jitter", where the unit stops, but has to start navigating agian, because the enemy is out-of-reach on the next update.

//        //Initialize the NavMeshAgent and assign stats to the agent's parameters
//        agent = GetComponent<NavMeshAgent>();
//        agent.speed = baseSpeed;
//        agent.radius = sizeRadius;

//        //Assert the correct player id:
//        thisPlayer = GetComponentInParent<PlayerID>().playerID;
//        enemyPlayer = (thisPlayer == 1) ? 2 : 1;

//        //Assert the correct TargetInformation to this unit
//        targetInfo = GetComponent<UnitTargetInfo>();
//        targetInfo.SetTargets(targetTypes);
//        targetInfo.SetTargetees(targeteeTypes);

//        //Add the unit's Transform to the current battlefield units Hashset:
//        targetManager = FindObjectOfType<TargetingManager>();
//        targetManager.RegisterUnit(gameObject.transform, thisPlayer);
//    }

//    private void Start() {
//        //Assemble the list of enemy towers, if the unit targets them
//        CalculateTowerCol();

//        //Assemble the starting Enemy target list, excluding towers. Note that this Unit's targetlist is dependant on the targetTypes[].
//        CalculateTargetCol();

//        //Select the closest target
//        ClosestTower();

//        //Navigate towards closest target
//        NavigateToClosest(towerTarget);

//    }

//    private void Update() {

//        if (currentState != previousState) { //When state changes from the previous frame, we should handle it's destination only once, instead of on every frame
//            switch (currentState) {
//                case AIstate.Navigate:
//                    ClosestTower();
//                    agent.SetDestination(towerTarget.position);
//                    break;
//                case AIstate.Aggro:
//                    agent.SetDestination(enemyTarget.position);
//                    break;
//                case AIstate.Attack:
//                    //Listen to DeathNotification from target
//                    attackTimer = timer;
//                    agent.isStopped = true;
//                    break;
//                case AIstate.Stun:
//                    agent.isStopped = true;
//                    break;
//                case AIstate.NoState:
//                    agent.isStopped = true;
//                    break;
//            }
//        }

//        previousState = currentState;

//        switch (currentState) { //We could nest this switch inside a Update timer, so that these commands will only be executed every 4 frames , or something like that

//            case AIstate.Navigate:
//                Navigate();
//                break;
//            case AIstate.Aggro:
//                Aggro();
//                break;
//            case AIstate.Attack:
//                Attack();
//                break;
//            case AIstate.Stun:
//                print(gameObject.name + " is Stunned");
//                break;
//            case AIstate.NoState:
//                NoState();
//                break;
//        }
//    }

//    public void Navigate() {
//        ClosestTarget();

//        if (Vector2.Distance(transform.position, enemyTarget.position) < attackRad) {
//            currentState = AIstate.Attack;
//        } else if (Vector2.Distance(transform.position, enemyTarget.position) < aggroRadius) {
//            currentState = AIstate.Aggro;
//        }
//    }

//    public void Aggro() {
//        ClosestTarget();

//        if (Vector2.Distance(transform.position, enemyTarget.position) < attackRad) {
//            currentState = AIstate.Attack;
//        } else if (Vector2.Distance(transform.position, enemyTarget.position) > aggroRadius + 0.1f) { //The enemy target got away from the range, with a slight 0.1f buffer
//            currentState = AIstate.Navigate;
//            //DeListen target death notification
//        }
//    }

//    public void Attack() {
//        //No need to check the ClosestTarget(), since we should lock our attention to one troop at least as long as it's in the reachRadius

//        if (Vector2.Distance(transform.position, enemyTarget.position) < reachRad) {
//            //Attack the target
//            attackTimer -= attackSpeed;
//            if (attackTimer <= 0) {
//                enemyTarget.GetComponent<IDamageable>().ApplyDamage(attackPower);
//                attackTimer = timer;
//            }

//        } else if (Vector2.Distance(transform.position, enemyTarget.position) < aggroRadius) {
//            currentState = AIstate.Aggro;
//        } else {
//            currentState = AIstate.Navigate;
//            //DeListen target death notification
//        }
//    }

//    public void NoState() {
//        StartCoroutine(StunCooldown(1f));
//    }

//    IEnumerator StunCooldown(float time) {
//        currentState = AIstate.Stun;
//        yield return new WaitForSeconds(time);
//        currentState = AIstate.Navigate;
//    }
//    public void CalculateTowerCol() {
//        if (targetTypes.Contains(0)) { //1 = TowerType
//            HashSet<Transform> allTargets = targetManager.GetHashSet(enemyPlayer);
//            foreach (Transform unit in allTargets) {
//                if (unit.GetComponent<UnitTargetInfo>().targeteeTypes.Contains(0)) { //1 = TowerType
//                    towers.Add(unit);
//                }
//            }
//        }
//    }

//    public void CalculateTargetCol() {
//        HashSet<Transform> allTargets = targetManager.GetHashSet(enemyPlayer);
//        foreach (Transform unit in allTargets) {
//            print(unit.transform.parent.name);
//            int[] targetees = unit.GetComponent<UnitTargetInfo>().targeteeTypes;
//            bool hasDuplicates = targetees.Intersect(targetTypes).Any(); //foreach(int targetType in targetTypes) {foreach(int targeteeType in targetees) {if(targetType == targeteeType) { matchBetweenTarget = true;break;}}if (matchBetweenTarget) {break;}}
//            if (hasDuplicates) {
//                targets.Add(unit);
//            }
//        }
//    }

//    public void ModifyTargetCol(int playerID, Transform t) {
//        int[] targetees = t.GetComponent<UnitTargetInfo>().targeteeTypes;
//        bool isCorrectType = targetees.Intersect(targetTypes).Any();
//        if (isCorrectType) {
//            if (targets.Contains(t)) {
//                targets.Remove(t);
//            } else {
//                targets.Add(t);
//            }
//        }
//    }

//    public Transform ClosestTower() {
//        if (towers.Count <= 0) {
//            print("Tried to access closest tower but the towers collection was empty");
//            return null;
//        }

//        towerTarget = null;

//        foreach (Transform t in towers) {
//            if (towerTarget == null || Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, towerTarget.position)) {
//                towerTarget = t;
//            }
//        }
//        return towerTarget;
//    }

//    public Transform ClosestTarget() { //The return type is not really used, since targetPos variable is already public
//        if (targets.Count <= 0) {
//            print("Tried to access closest target but the targets collection was empty");
//            return null;
//        }

//        enemyTarget = null;

//        foreach (Transform t in targets) {
//            if (enemyTarget == null || Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, enemyTarget.position)) {
//                enemyTarget = t;
//            }
//        }
//        return enemyTarget;
//    }

//    public void NavigateToClosest(Transform target) {
//        agent.SetDestination(target.position);
//    }

//    private void OnDrawGizmos() {
//        Color color;
//        if (thisPlayer == 1) {
//            color = Color.blue;
//        } else if (thisPlayer == 2) {
//            color = Color.red;
//        } else {
//            color = Color.magenta;
//        }
//        Gizmos.color = color;

//        if (enemyTarget != null) {
//            Gizmos.DrawLine(transform.position, enemyTarget.position);
//        }

//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, attackRad);
//    }
//}

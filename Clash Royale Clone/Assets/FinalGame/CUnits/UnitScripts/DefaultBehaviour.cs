using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//README: This is the universal A.I. behaviour model. You can give this to a specific unit by editing lines 7 and 8, and giving the correct names.
public class DefaultBehaviour : MonoBehaviour, IBehaviourStats {
    public string unitTypeName = "Default";

    //private void OnEnable() {
    //    TargetingManager.OnUnitsSetChange += RefreshUnitsSet;
    //}
    //private void OnDisable() {
    //    TargetingManager.OnUnitsSetChange -= RefreshUnitsSet;
    //}

    public int GetHealth() {
        return health;
    }

    UnitTypeContainer unitContainer;
    List<UnitType> unitTypes;
    UnitType thisUnit;

    NavMeshAgent agent;

    public int thisPlayer = -1;
    public int enemyPlayer = -1;

    UnitTargetInfo targetInfo;

    TargetingManager targetManager;
    Transform currentTarget;
    Transform closestTarget;

    //The same data as in UnitType:    
    public int health;
    public int attackPower;
    public float attackPerSecond;
    public float baseSpeed;
    public float sizeRadius;
    public float attackRadius;
    public float aggroRadius;
    public int[] targetTypes; //0 = Tower, 1 = GroundUnit, 2 = AirUnit
    public int[] unitCharacteristics; //0 = Tower, 1 = GroundUnit, 2 = AirUnit

    public TargetClass targets;
    public TargetClass characteristcs;

    float attackRad;
    float reachRad;

    float attackTimer;

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
        attackPerSecond = thisUnit.attackPerSecond;
        baseSpeed = thisUnit.baseSpeed;
        sizeRadius = thisUnit.sizeRadius;
        attackRadius = thisUnit.attackRadius;
        aggroRadius = thisUnit.aggroRadius;
        targets = thisUnit.targets;
        characteristcs = thisUnit.characteristcs;

        //Initialize some private stats as well:
        attackRad = attackRadius; //The tolrance distance for when the enemy starts "Attacking" the target instead of "Navigating" towards it.
        reachRad = attackRad * 1.2f; //Once the "Attacking" has started, we need to enlargen the attackDiameter, so that there won't occur any "following jitter", where the unit stops, but has to start navigating agian, because the enemy is out-of-reach on the next update.
        attackTimer = attackPerSecond;

        //Initialize the NavMeshAgent and assign stats to the agent's parameters
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        agent.radius = sizeRadius;

        //Assert the correct player id:
        thisPlayer = GetComponentInParent<PlayerID>().playerID;
        enemyPlayer = (thisPlayer == 1) ? 2 : 1;

        //Assert the correct TargetInformation to this unit
        targetInfo = GetComponent<UnitTargetInfo>();
        targetInfo.SetTargetEnum(targets);
        targetInfo.SetCharacteristicsEnum(characteristcs);

        //Add the unit's Transform to the current battlefield units Hashset:
        targetManager = FindObjectOfType<TargetingManager>();
        targetManager.RegisterUnit(gameObject.transform, thisPlayer);

        //Initialize the Unit States, so the game will orient the unit correctly starting from Update()
        currentState = AIstate.NoState;
        previousState = AIstate.NoState;
    }

    public void OnDeath() {
        targetManager.UnregisterUnit(gameObject.transform, thisPlayer);
        GameObject parent = transform.parent.gameObject;
        Destroy(parent);
    }

    public void OnTargetDeath() {
        currentState = AIstate.Navigate;
    }

    public void ListenSelf() {
        var notify = GetComponent<INotifyOnDestroy>();
        if (notify != null) {
            notify.AddListener(OnDeath);
        }
    }

    public void UnListenSelf() {
        var notify = GetComponent<INotifyOnDestroy>();
        if (notify != null) {
            notify.RemoveListener(OnDeath);
        }
    }

    public void ListenTarget() {
        var notify = currentTarget.GetComponent<INotifyOnDestroy>();
        if (notify != null) {
            notify.AddListener(OnTargetDeath);
        }
    }

    public void UnListenTarget() {
        var notify = currentTarget.GetComponent<INotifyOnDestroy>();
        if (notify != null) {
            notify.RemoveListener(OnTargetDeath);
        }
    }

    private void Start() {
        //Initialize the potentialTargets hashset
        currentTarget = targetManager.FindClosestTarget(transform, thisPlayer, true);
        if (currentTarget == null) {
            Debug.Log("Target was null in start");
        }
        ListenSelf();
    }

    private void Update() {

        if (currentTarget == null) {
            currentTarget = gameObject.transform;
        }

        if (currentState != previousState) { //When state changes from the previous frame, we should handle it's destination only once, instead of on every frame
            switch (currentState) {
                case AIstate.Navigate:
                    currentTarget = targetManager.FindClosestTarget(transform, thisPlayer, true);
                    NavigateToClosest(currentTarget);
                    break;
                case AIstate.Aggro:
                    NavigateToClosest(currentTarget);
                    break;
                case AIstate.Attack:
                    ListenTarget();
                    attackTimer = attackPerSecond;
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
                break;
            case AIstate.NoState:
                NoState();
                break;
        }
    }

    public void Navigate() {
        closestTarget = targetManager.FindClosestTarget(transform, thisPlayer, false);
        if (closestTarget == null) {
            closestTarget = gameObject.transform;
        }

        if (Vector3.Distance(transform.position, closestTarget.position) < attackRad) {
            currentState = AIstate.Attack;
            currentTarget = closestTarget;
        } else if (Vector3.Distance(transform.position, closestTarget.position) < aggroRadius) {
            currentState = AIstate.Aggro;
            currentTarget = closestTarget;
        }
    }

    public void Aggro() {
        closestTarget = targetManager.FindClosestTarget(transform, thisPlayer, false);
        if (closestTarget == null) {
            closestTarget = gameObject.transform;
        }

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

        if (Vector2.Distance(transform.position, currentTarget.position) < reachRad && currentTarget != gameObject.transform) {
            //Attack the target
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) {
                currentTarget.GetComponent<IDamageable>().ApplyDamage(attackPower);
                attackTimer += attackPerSecond;
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
            agent.isStopped = false;
            agent.SetDestination(target.position);
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

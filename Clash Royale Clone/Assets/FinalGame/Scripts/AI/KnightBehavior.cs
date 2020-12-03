using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum KnightState { Move, Attack, Chase, Investigate };

public class KnightBehavior : MonoBehaviour {
    // similar to agents code. Looks for close enemies. At this point, always goes for closest enemy, even if there already is an enemy
    KnightState currentState = KnightState.Move;
    private NavMeshAgent agent;
    public List<Transform> waypoints;
    int nextPoint = 0;
    public int attackpower = 2;
    public float patrolSpeed = 1f;
    float waypointTolerance = 2f;
    public float hitTime = 1; //time in seconds between each hit
    float curTime = 0; //time in seconds since last hit
    public float investigationTime = 0.01f;
    float investigationTimer = 0;

    private Towers towerhp;


    EnemyPlayers enem;

    void Awake() {
        enem = GetComponent<EnemyPlayers>();
        agent = GetComponent<NavMeshAgent>();
        nextPoint = ClosestPoint();
        towerhp = waypoints[nextPoint].GetComponent<Towers>();
        //unitStats = gameObject.GetComponent<UnitStats>(); // unitStats from different script
        //agent.stoppingDistance = 0;
        ContinuePatrol();
    }

    void ContinuePatrol() {
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[nextPoint].position);
    }


    bool CloseEnoughToWaypoint() {

        return Vector3.Distance(transform.position, waypoints[nextPoint].position)
            < waypointTolerance;
    }


    public int ClosestPoint() {
        // this works, finds the closest point
        int j = 0;
        var smallest = Vector3.Distance(agent.transform.position, waypoints[0].transform.position);
        for (var i = 1; i < waypoints.Count; i++) {
            var dist = Vector3.Distance(agent.transform.position, waypoints[i].transform.position);
            if (dist < smallest) {
                smallest = dist;
                j = i;
            }
        }
        return j;
    }

    Transform FindClosest(List<Transform> targets) {
        Transform closest = targets[0];
        foreach (var t in targets) {
            if (Vector3.Distance(transform.position, t.position) <
               Vector3.Distance(transform.position, closest.position)) {
                closest = t;
            }
        }
        return closest;
    }


    void Update() {
        var enemies = enem.CloseEnemies();
        if (enemies.Count > 0) {
            currentState = KnightState.Chase;
            agent.enabled = true;
        } else if (currentState == KnightState.Investigate) {
            if (investigationTimer > 0) {
                investigationTimer -= Time.deltaTime;
                if (investigationTimer < 0) {
                    agent.enabled = true;
                    currentState = KnightState.Move;
                }
            }
        } else if (currentState == KnightState.Chase) {
            currentState = KnightState.Investigate;
            investigationTimer = investigationTime;
            agent.enabled = false;
        }


        if (currentState == KnightState.Move) {
            if (CloseEnoughToWaypoint()) {
                agent.velocity = Vector3.zero;
                //agent.transform.position = waypoints[nextPoint].position;
                if (towerhp.towerMaxHP <= 0) {
                    // we have decided that last tower is element 2. Not the best way, could be good to redo this at some point
                    nextPoint = 2;
                    if (nextPoint > waypoints.Count - 1) {
                        nextPoint = 0;
                    }
                    towerhp = waypoints[nextPoint].GetComponent<Towers>();
                }
                curTime += Time.deltaTime;
                if (curTime >= hitTime && towerhp.towerMaxHP > 0) {
                    towerhp.towerMaxHP -= attackpower;
                    curTime = curTime - hitTime;
                }
            }
            ContinuePatrol();


        } else if (currentState == KnightState.Chase) {
            
            var closest = FindClosest(enemies);
            if (closest == null) {

                currentState = KnightState.Move;
            }

            var enemyHealth = closest.GetComponent<TankHealth>().health;

            if (closest) {
                var notify = closest.GetComponent<INotifyOnDestroy>();
                if (notify != null)
                    notify.AddListener(TargetDead);
            }

            void TargetDead() {
                closest = null;
            }

            if (closest != null) {
                agent.SetDestination(closest.position);
                if (Vector3.Distance(transform.position, closest.position) < waypointTolerance) {
                    print("is there any enemy health close" + enemyHealth);

                    if (closest.GetComponent<TankHealth>().health <= 0) {
                        
                        closest = null;
                        currentState = KnightState.Move;
                    }

                    curTime += Time.deltaTime;
                    if (curTime >= hitTime && enemyHealth > 0) {
                        IDamageable daobject = closest.GetComponent<IDamageable>();
                        if (daobject != null) {
                            daobject.ApplyDamage(attackpower);
                        }
                        //closest.GetComponent<UnitStats>().TakeDamge(attackpower);
                        // vanha closest.GetComponent<UnitStats>().health -= unitStats.attackPower;
                        curTime = curTime - hitTime;
                    }
                }
            }

        }


        if (currentState == KnightState.Attack) {

        }
    }

    void attack() {
        print(" starts attacking");
        //int towerhp = waypointsGo[nextPoint].GetComponent<Towers>().towercurHP;
        //towerhp -= giantAttackPower;
        // start to attack waypoint that you are closest to
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, waypointTolerance);
    //}




}// class

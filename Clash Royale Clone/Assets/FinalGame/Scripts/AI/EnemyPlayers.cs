using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayers : MonoBehaviour
{
    //List<Transform> enemies = new List<Transform>();
    List<Transform> closeEnemies = new List<Transform>();
    List<Transform> allEnemies = new List<Transform>();

    public GameObject[] enemies;
    public float maxSightDistance = 1.3f;
    public float maxSightAngle = 180f;


    void Start()
    {
        // finding enemies
        enemies = GameObject.FindGameObjectsWithTag("P1"); // find every enemy with tag. This needs to be in update
        foreach (var pc in enemies) {
            allEnemies.Add(pc.transform); // if it was something else than transform, we would use get component  
        }
    }

    public List<Transform> CloseEnemies() {
        return new List<Transform>(closeEnemies); // copy of list  
    }


    // Update is called once per frame
    void Update()
    {
        UpdateAllEnemies();
        UpdateCloseEnemies(); 
    }

    void UpdateAllEnemies() {
        allEnemies.Clear();
        // finding enemies
        enemies = GameObject.FindGameObjectsWithTag("P1"); // find every enemy with tag. This needs to be in update
        foreach (var pc in enemies) {
            allEnemies.Add(pc.transform); // if it was something else than transform, we would use get component  
        }
    }

    void UpdateCloseEnemies() {
        closeEnemies.Clear();
        foreach (var t in allEnemies) {
            if (TooFar(t))
                continue; // we go back at the loop beginning
            if (OutsideVisionCone(t))
                continue;
            closeEnemies.Add(t);
        }
    }


    bool TooFar(Transform target) {
        var difference = (target.position - transform.position);
        float lenght = difference.magnitude; // magnitude is lenght of the vector

        return lenght > maxSightDistance;

    }

    bool OutsideVisionCone(Transform target) {
        float angle = Vector3.Angle(target.position - transform.position, transform.forward);
        return angle > maxSightAngle;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue; // gizmos show in editor
        Gizmos.DrawWireSphere(transform.position, maxSightDistance);
    }

}

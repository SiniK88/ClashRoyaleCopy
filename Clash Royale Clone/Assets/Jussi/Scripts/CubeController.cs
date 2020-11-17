using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{

    Vector2 movement;

    private void OnPlace(InputValue value) {
        Debug.Log("Moving!");
        movement = value.Get<Vector2>();
    }

    private void Update() {
        Move();
        
    }

    private void Move() {
        Vector3 i_movement = new Vector3(movement.x, 0, movement.y) * Time.deltaTime;
        transform.position += i_movement;
    }
}

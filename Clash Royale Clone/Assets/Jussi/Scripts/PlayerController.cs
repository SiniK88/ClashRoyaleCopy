using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public string playerID;

    public delegate void NavigateAction(int i, string id);
    public static event NavigateAction OnNavigate;

    private void Awake() {
    }    

    public void OnSelectionPlus(InputValue input) {
        print("Pressed RB");
        OnNavigate(1, playerID);
    }

    public void OnSelectionMinus(InputValue input) {
        print("Pressed LB");
        OnNavigate(-1, playerID);
    }

    public void AssignPlayerID() {
        playerID = gameObject.name;
    }


}

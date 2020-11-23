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

    public delegate void ClickAction(string id);
    public static event ClickAction OnClickA;

    public delegate void CancelAction(string id);
    public static event CancelAction OnCancelB;

    public delegate void PlaceAction(Vector2 move, string id);
    public static event PlaceAction OnPlacement;

    private void Awake() {
    }  

    public void OnCancel(InputValue input) {
        OnCancelB(playerID);
    }

    public void OnPlace(InputValue input) {
        Vector2 cursorMove = input.Get<Vector2>();
        OnPlacement(cursorMove, playerID);
    }

    public void OnClick(InputValue input) {
        OnClickA(playerID);
    }

    public void OnSelectionPlus(InputValue input) {
        OnNavigate(1, playerID);
    }

    public void OnSelectionMinus(InputValue input) {
        OnNavigate(-1, playerID);
    }

}

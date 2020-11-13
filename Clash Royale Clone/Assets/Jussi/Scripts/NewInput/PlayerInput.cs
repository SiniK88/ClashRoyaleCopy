using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour {

    InputMaster controls;
    public Vector2 placementPos;

    public delegate void NavigateAction(int i);
    public static event NavigateAction OnNavigate;

    public delegate void SelectAction();
    public static event SelectAction OnSelect;

    public delegate void CancelAction();
    public static event CancelAction OnCancel;

    public delegate void PlaceAction(Vector2 place);
    public static event PlaceAction OnPlacement;

    private void Awake() {        

        controls = new InputMaster();
        controls.Player.Select.performed += ctx => Select();
        controls.Player.Cancel.performed += ctx => Cancel();
        controls.Player.SelectionPlus.performed += ctx => Navigate(1);
        controls.Player.SelectionMinus.performed += ctx => Navigate(-1);

        controls.Player.Place.performed += ctx => Place(ctx.ReadValue<Vector2>());
        controls.Player.Place.canceled += ctx => placementPos = Vector2.zero;
    }    

    public void Place(Vector2 place) {
        print("Moved left stick");
        placementPos = place;
        OnPlacement(placementPos);
    }

    public void Navigate(int i) {
        print("Pressed button RB or LB");
        OnNavigate(i);        
    }

    public void Select() {
        print("Pressed button A");
        OnSelect();
    }

    public void Cancel() {
        print("Pressed button B");
        OnCancel();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }


}

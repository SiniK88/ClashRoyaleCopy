using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour {

    InputMaster controls;
    Vector2 movePos;
    public Player player = null;

    public delegate void NavigateAction(int i, Player player);
    public static event NavigateAction OnNavigate;

    public delegate void SelectAction(Player player);
    public static event SelectAction OnSelect;

    public delegate void CancelAction(Player player);
    public static event CancelAction OnCancel;

    //public delegate void PlaceAction(Vector2 place);
    //public static event PlaceAction OnPlacement;

    private void Awake() {        

        controls = new InputMaster();
        controls.Player.Select.performed += ctx => Select(player);
        controls.Player.Cancel.performed += ctx => Cancel(player);
        controls.Player.SelectionPlus.performed += ctx => Navigate(1, player);
        controls.Player.SelectionMinus.performed += ctx => Navigate(-1, player);

        controls.Player.Place.performed += ctx => Place(ctx.ReadValue<Vector2>());
        controls.Player.Place.canceled += ctx => movePos = Vector2.zero;        
    }

    private void Update() {
        transform.position += new Vector3(movePos.x, 0, movePos.y) * Time.deltaTime;
        print(player);


    }

    public void SetPlayer(Player _player) {
        player = _player;
    }

    public void Place(Vector2 place) {
        print("Moved left stick");
        movePos = place;
        //OnPlacement(movePos);
    }

    public void Navigate(int i, Player _player) {
        print("Pressed button RB or LB");
        OnNavigate(i, _player);        
    }


    public void Select(Player _player) {
        print("Pressed button A");
        OnSelect(_player);
    }

    public void Cancel(Player _player) {
        print("Pressed button B");
        OnCancel(_player);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }



}

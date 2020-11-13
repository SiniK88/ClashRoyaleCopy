using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour {
    private void OnEnable() {
        PlayerInput.OnSelect += SelectCard;
        //PlayerInput.OnCancel += CancelSelection;
        PlayerInput.OnPlacement += PlacementSpeed;

        PlayerController.HighlightActivated += Highlight;
        PlayerController.HighlightDeActivated += DeHighlight;
    }

    private void OnDisable() {
        PlayerInput.OnSelect -= SelectCard;
        //PlayerInput.OnCancel -= CancelSelection;
        PlayerInput.OnPlacement -= PlacementSpeed;

        PlayerController.HighlightActivated -= Highlight;
        PlayerController.HighlightDeActivated -= DeHighlight;
    }

    public bool isActivatable = false;
    public bool isActive = false;
    public bool attemptPlacement = false;

    Vector3 startingPos;
    Vector3 speed = Vector3.zero;

    Renderer rend;
    Color initialColor;
    Color highlightColor = Color.yellow;
    Color selectionColor = Color.red;

    private void Awake() {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
        startingPos = transform.position;
    }

    private void Update() {
        if (!isActive) {
            return;
        }

        if (isActive) {
            if (!attemptPlacement) {
                transform.Translate(speed, Space.World);
            } else if (attemptPlacement) {
                //check if placement is succesful and in that case, somehow remove it from the list
                //if the placement is not successful, call a function called ResetCard()
                attemptPlacement = false;
                isActive = false;
            }
        }
    }

    public void PlacementSpeed(Vector2 direction) {
        speed = new Vector3(direction.x, 0, direction.y) * 0.05f;
    }

    public void SelectCard() {
        if (!isActivatable) {
            return;
        } else if (isActivatable && !isActive) {
            startingPos = transform.position;
            rend.material.color = selectionColor;
            isActive = true;
        } else if(isActivatable && isActive) {
            attemptPlacement = true;
            rend.material.color = initialColor;
        }
    }

    public void Highlight(GameObject go) {
        if (go == gameObject) {
            rend.material.color = highlightColor;
            isActivatable = true;
        }
        
    }

    public void DeHighlight(GameObject go) {
        if(go == gameObject) {
            rend.material.color = initialColor;
            isActivatable = false;
        }
        
    }



}

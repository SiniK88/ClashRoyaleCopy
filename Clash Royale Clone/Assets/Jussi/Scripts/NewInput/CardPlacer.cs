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

    public Transform defaultPosTransform;
    Vector3 defaultPos;

    public GameObject placerGfx;
    public GameObject cardGfx;
    public GameObject minionGfx;

    private void Awake() {
        defaultPos = defaultPosTransform.position;
        rend = cardGfx.GetComponent<Renderer>();
        initialColor = rend.material.color;
        startingPos = transform.position;
    }

    private void Update() {
        if (!isActive) {
            return;
        }

        if (isActive) {
            if (!attemptPlacement) {

                transform.position += speed;
                //Vector3 discretePos = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), Mathf.Floor(transform.position.z));
                //placerGfx.transform.position = discretePos;


            } else if (attemptPlacement) {
                attemptPlacement = false;
                isActive = false;
            }
        }
    }

    //public Vector3 UpdateObjectPos() {        
    //    Vector3 discretePosition = Vector3.zero;
    //    Vector3 position = new Vector3(transform.position)

    //    return discretePosition;
    //}

    public void UpdateGraphics() {

    }

    public void PlacementSpeed(Vector2 direction) {
        speed = new Vector3(direction.x, 0, direction.y) * Time.deltaTime * 20f;
    }

    public void SelectCard() {
        if (!isActivatable) {
            return;
        } else if (isActivatable && !isActive) {
            startingPos = transform.position;
            isActive = true;
            transform.position = defaultPos;
            cardGfx.SetActive(false);
            placerGfx.SetActive(true);
        } else if(isActivatable && isActive) {
            attemptPlacement = true;
            placerGfx.SetActive(false);
            minionGfx.SetActive(true);
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

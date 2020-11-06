using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    Renderer rend;
    Color initialColor;
    Color dragColor = Color.red;
    Color highlightColor = Color.yellow;
    DragInput dragInput;
    public bool isDragging = false;

    public GameObject cardView;
    public GameObject placementView;
    public GameObject minionView;

    private void Awake() {
        dragInput = FindObjectOfType<DragInput>();
        rend = cardView.GetComponent<Renderer>();
        initialColor = rend.material.color;
    }

    private void OnMouseDown() {
        dragInput.CardSelected(this);
        isDragging = true;
        rend.material.color = dragColor;
    }

    private void OnMouseUp() {
        isDragging = false;
        rend.material.color = initialColor;
    }

    private void OnMouseOver() {
        //highlight
        if (!isDragging) {
            rend.material.color = highlightColor;
        }
    }

    private void OnMouseExit() {
        //exit highlight
        if(rend.material.color == highlightColor) {
            rend.material.color = initialColor;
        }
    }

    public void UpdateGraphics(RaycastHit hit) {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("CardSet")) {
            print("Layer: CardSet" + hit.collider.gameObject.layer);
            SetGraphics(cardView);
        } else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlacementGrid")) {
            print("Layer: PlacementGrid" + hit.collider.gameObject.layer);
            SetGraphics(placementView);
            //placementView.SetActive(true);
        } 
        
        //else {
        //    print("Layer: " + hit.collider.gameObject.layer);
        //}
    }

    void SetGraphics(GameObject current) {
        cardView.SetActive(false);
        placementView.SetActive(false);
        minionView.SetActive(false);
        current.SetActive(true);
    }

    //private void OnMouseDrag() {
    //    isDragging = true;
    //    rend.material.color = dragColor;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; //why do we need this?

public class DragInput : MonoBehaviour
{
    //Object initialization
    Vector3 startingPos;
    public DraggableObject selected;


    //Raycast stuff    
    Ray ray;
    RaycastHit hit;
    bool rayHitGrid;
    public Camera cam;
    public LayerMask placableGrid;

    //float y_Offset; for later use, in-case we want to have the placement be made from above

    public void CardSelected(DraggableObject newSelection) {
        selected = newSelection;
        startingPos = selected.transform.position;
    }

    private void Update() {

        if (selected != null && selected.isDragging == false) {

            if (rayHitGrid == false || hit.collider.gameObject.name == "CardSet") {    //Player attempts to place the object outside of the grid or back inside the UI element "CardSet"
                selected.transform.position = startingPos;                              //therefore we return the object to it's original place
            } else {
                selected = null;                                                        //The object was placed within the grid, so we no longer want to move it in relation to mousePosition
            }
        }

        if (selected != null && selected.isDragging == true) {

            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (rayHitGrid = Physics.Raycast(ray, out hit, Mathf.Infinity, placableGrid)) {
                selected.transform.position = hit.point;    //Object follows the mousePosition
                selected.UpdateGraphics(hit);
            } 
        }        
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Selectable : MonoBehaviour
{
    public Selectable next;
    public Selectable previous;

    public int currentIndex;

    public string playerID;

    public bool isSelectable = true;
    private bool isSelected = false;

    private bool isClicked = false; //When this Selectable is selected and also clicked with A- button.

    SortingGroup layerGroup;
    int layer;

    private void Awake() {
        layerGroup = GetComponent<SortingGroup>();
        layer = layerGroup.sortingOrder;
    }

    private void SelectionVisual() {
        
        if (isSelected) {
            layerGroup.sortingOrder = 5; //Arbitrary number, as long as it's above 4, which is the highest sorting group in the game atm.
            this.transform.position += Vector3.back * 1f;
            this.transform.localScale = Vector3.one * 1.1f;
        } else {
            layerGroup.sortingOrder = layer;
            this.transform.position -= Vector3.back * 1f;
            this.transform.localScale = Vector3.one;
        }
    }

    private void ClickVisual() {
        if (isClicked) {
            this.transform.position += Vector3.up * 0.5f;
        } else {
            this.transform.position -= Vector3.up * 0.5f;
        }
    }

    public bool IsSelected { 
        get => isSelected;
        set {
            isSelected = value;
            SelectionVisual();
        }
    }

    public bool IsClicked {
        get => isClicked;
        set {
            isClicked = value;
            ClickVisual();
        }
    }
}

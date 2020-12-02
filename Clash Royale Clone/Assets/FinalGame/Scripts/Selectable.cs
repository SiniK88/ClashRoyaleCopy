using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Selectable next;
    public Selectable previous;

    public int currentIndex;

    public string playerID;

    public bool isSelectable = true;
    private bool isSelected = false;

    private bool isClicked = false; //When this Selectable is selected and also clicked with A- button.

    private void SelectionVisual() {
        if (isSelected) {
            this.transform.position += Vector3.back * 1f;
            this.transform.localScale = Vector3.one * 1.1f;
        } else {
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

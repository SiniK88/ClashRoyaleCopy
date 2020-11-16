using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Selectable next;
    public Selectable previous;

    public bool isSelectable = true;
    private bool isSelected = false;

    private void VisualUpdate() {
        if (isSelected) {
            this.transform.localScale = Vector3.one * 1.3f;
        } else {
            this.transform.localScale = Vector3.one;
        }
    }
    public bool IsSelected { 
        get => isSelected;
        set {
            isSelected = value;
            VisualUpdate();
        }
    }
}

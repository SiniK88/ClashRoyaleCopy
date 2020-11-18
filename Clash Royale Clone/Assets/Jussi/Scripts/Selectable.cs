using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Selectable next;
    public Selectable previous;

    public string playerID;

    public bool isSelectable = true;
    private bool isSelected = false;

    private void VisualUpdate() {
        if (isSelected) {
            this.transform.localScale = Vector3.one * 0.13f;
        } else {
            this.transform.localScale = Vector3.one * 0.1f;
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

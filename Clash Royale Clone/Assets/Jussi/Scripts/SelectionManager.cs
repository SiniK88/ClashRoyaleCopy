using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    GameInitializer gameInit;

    private void OnEnable() {
        PlayerController.OnNavigate += OnChangeSelection; //Pressed RB or LB
    }
    private void OnDisable() {
        PlayerController.OnNavigate -= OnChangeSelection;
    }

    Selectable lastSelected = null;
    Selectable[] selectables;
    Selectable lastSelectedP1;
    Selectable lastSelectedP2;

    void Start() {        
        selectables = GameObject.FindObjectsOfType<Selectable>();
        lastSelectedP1 = Array.Find(selectables, selectable => selectable.isSelectable && selectable.playerID.Equals("Player1"));
        lastSelectedP2 = Array.Find(selectables, selectable => selectable.isSelectable && selectable.playerID.Equals("Player2"));
        gameInit = FindObjectOfType<GameInitializer>();
    }
    

    public void OnChangeSelection(int dir, string playerID) {
        Selectable selected = null; //this changes along the method
        Selectable lastSelected = null; //this stays the same until the last part

        if(playerID.Equals("Player1")) {
            print("Player 1 pressed a button");
            selected = lastSelectedP1;
            lastSelected = lastSelectedP1;
        } else if(playerID.Equals("Player2")) {
            print("Player 2 pressed a button");
            selected = lastSelectedP2;
            lastSelected = lastSelectedP2;
        }
 
        if (selected != null && selected.IsSelected) { //This is the normal situation: we have a selection active (non-null) and said selection IsSelected. 
            HashSet<Selectable> visited = new HashSet<Selectable>();
            if (dir > 0) {
                visited.Add(selected);
                selected = selected.next;
                while (selected != null && selected.isSelectable == false && (!visited.Contains(selected))) { 
                    /*We basically make sure that the "selected.next" is actually selectable (isSelectable == true), 
                    or else we skip to the next one. The Hashset shouldn't also contain the selected itself, meaning that it will choose itself if it's the only one available:
                    The loop ends when the selected itself is in-fact in the Hashset, and thus keep the selection as itself. Meaning that selected is the same as selected.next*/
                    visited.Add(selected);
                    selected = selected.next;
                }
            }
            if (dir < 0) {
                visited.Add(selected);
                selected = selected.previous;
                while (selected != null && selected.isSelectable == false && (!visited.Contains(selected))) {
                    visited.Add(selected);
                    selected = selected.previous;
                }
            }
        }

        if (selected == null || (selected.isSelectable == false)) {
            // if we don't have a valid next selectable, select first item in array of all selectables
            RefreshSelectables();
            selected = Array.Find<Selectable>(selectables, selectable => selectable.isSelectable && selectable.playerID.Equals(playerID));
        }
        if (selected != null) {
            if (lastSelected != null) {
                lastSelected.IsSelected = false;
            }
            selected.IsSelected = true;
            lastSelected = selected;
        }

        if (playerID.Equals("Player1")) {
            lastSelectedP1 = lastSelected;
        } else if (playerID.Equals("Player2")) {
            lastSelectedP2 = lastSelected;
        }

    }

    public void RefreshSelectables() {
        selectables = GameObject.FindObjectsOfType<Selectable>();
    }
}

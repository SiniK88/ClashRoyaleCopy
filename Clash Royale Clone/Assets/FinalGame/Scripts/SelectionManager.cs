using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    //This is a player-specific selection manager!! Each player has it's own, meaning, that we don't have to cross-check PlayerID's all the time. Each SelectionManager gets initialized in GameInitializer

    public delegate void CardPlaced(int playerIndex, int cardIndex);
    public static event CardPlaced OnPlaceCard;

    private void OnEnable() {
        PlayerController.OnNavigate += OnChangeSelection; //Pressed RB or LB
        PlayerController.OnClickA += OnClickAction;
        PlayerController.OnCancelB += OnCancelAction;
    }
    private void OnDisable() {
        PlayerController.OnNavigate -= OnChangeSelection;
        PlayerController.OnClickA -= OnClickAction;
        PlayerController.OnCancelB -= OnCancelAction;
    }

    public string _playerID;

    GameInitializer gameInit;
    List<Player> players;
    CardTypeContainer cardTypeContainer;
    List<CardType> cardTypes;
    PlacementCursor cursor;

    Selectable selected = null;
    Selectable lastSelected = null;
    Selectable[] selectables;   

    bool clickedCard = false;


    void Start() { //Most of the stuff below need to happen in Start(). That's because a lot of stuff is initialized in Awake()
        gameInit = FindObjectOfType<GameInitializer>();
        players = gameInit.players; //List of all the Player-objects.
        cardTypeContainer = FindObjectOfType<CardTypeContainer>();
        cardTypes = cardTypeContainer.cardTypes;

        PlacementCursor[] cursors = FindObjectsOfType<PlacementCursor>();
        foreach(PlacementCursor c in cursors) {
            if (c.playerID.Equals(_playerID)) {
                cursor = c;
                break;
            }
        }

        RefreshSelectables();
        lastSelected = Array.Find(selectables, selectable => selectable.isSelectable);
    }
    

    public void OnChangeSelection(int dir, string playerID) { //I still couldn't read the if-statements below in clear english. Good luck!

        if (playerID.Equals(_playerID)) {

            if (clickedCard) {
                UndoClickOperations();
            }

            selected = lastSelected;

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
                selected = Array.Find<Selectable>(selectables, selectable => selectable.isSelectable);
            }
            if (selected != null) {
                if (lastSelected != null) {
                    lastSelected.IsSelected = false;
                }
                selected.IsSelected = true;
                lastSelected = selected;
            }
        }        
    }

    public void OnClickAction(string playerID) { //Handles all the logic behind pressing the "A" button on controller

        if (playerID.Equals(_playerID)) {

            int playerIndex = (playerID.Equals("Player1") ? 0 : 1);
            Selectable currentSelection = Array.Find(selectables, selectable => selectable.IsSelected);
            CardType currentCardType = GetCardType(currentSelection, playerIndex);

            if (!clickedCard) {              
                if(currentSelection != null && currentCardType.manaCost < players[playerIndex].GetMana()) { //Manacost condition needs to be fitted to some PlayerStat
                    currentSelection.IsClicked = true;
                    clickedCard = true;                    
                    cursor.AddCursorObject(currentCardType.placerVisuals, currentCardType.placerGhostVisuals); //One more parameter needs to handle the LayerMask
                } else {
                    print("Not Enough Mana! Current mana is: " + players[playerIndex].GetMana() + "\nManacost is: " + currentCardType.manaCost);
                }                
            } else if (clickedCard) {
                //Initialize the spawn
                GameObject finalForm = currentCardType.finalForm;
                Vector3 spawnPos = cursor.GetNodePosition();
                UndoClickOperations(); //Cursor can be neglected / removed from here.

                //Instantiate the spawn at correct location
                Instantiate(finalForm, spawnPos, Quaternion.identity); //Fix rotation. Also somehow give a player ID to the minion

                //Substract the card manacost from player manapool
                players[playerIndex].RemoveMana(currentCardType.manaCost);

                //Remove current card from hand and draw a new card
                OnPlaceCard(playerIndex, currentSelection.currentIndex);

            }
        }        
    }

    public void OnCancelAction(string playerID) {
        Selectable currentSelection = Array.Find(selectables, selectable => selectable.IsSelected);

        if (playerID.Equals(_playerID)) {
            if (clickedCard) {
                UndoClickOperations();
            }
        }
    }

    public void UndoClickOperations() {
        Selectable currentSelection = Array.Find(selectables, selectable => selectable.IsSelected);
        cursor.DeleteCursorObjects();
        cursor.ResetCursor();
        currentSelection.IsClicked = false;
        clickedCard = false;
    }

    public void RefreshSelectables() {
        Selectable[] allSelectables = GameObject.FindObjectsOfType<Selectable>();
        List<Selectable> playerSelectables = new List<Selectable>();
        foreach(Selectable s in allSelectables) {
            if (s.playerID.Equals(_playerID)) {
                playerSelectables.Add(s);
            }
        }
        selectables = playerSelectables.ToArray();
    }

    public CardType GetCardType(Selectable _currentSelection, int _playerIndex) {        

        int selectableIndex = _currentSelection.currentIndex;
        Card currentCard = players[_playerIndex].handState.GetCardInIndex(selectableIndex);
        CardType _currentCardType = null;

        for (int i = 0; i < cardTypes.Count; i++) { 
            if(currentCard.effect == cardTypes[i].cardType) { //Finds the CardType which has the same Effect enum state as the Card.
                _currentCardType = cardTypes[i];
                break; //no need to loop the rest, since only one instance of each cardtype should exist
            } 
        }
        return _currentCardType;
    }
}



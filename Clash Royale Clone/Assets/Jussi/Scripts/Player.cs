using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public DeckState deckState;
    public HandState handState;

    private string playerID = "DefaultPlayerID";
    public bool isHuman;    

    public Player(bool _isHuman) {
        isHuman = _isHuman;
        deckState = new DeckState();
        handState = new HandState(deckState);
    }

    public void SetPlayerID(string _playerID) {
        this.playerID = _playerID;
    }

}

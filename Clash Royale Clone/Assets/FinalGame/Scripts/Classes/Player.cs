using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public DeckState deckState;
    public HandState handState;

    private string playerID = "DefaultPlayerID";
    public bool isHuman;
    private float mana = 70f;
    public static readonly float MAX_MANA = 100f;    

    public Player(bool _isHuman, List<Card> deckCards) {
        isHuman = _isHuman;
        deckState = new DeckState(deckCards);
        handState = new HandState(deckState);
    }

    public void SetPlayerID(string _playerID) {
        this.playerID = _playerID;
    }

    public void AddMana(float _manaIncrease) {        
        mana = Mathf.Clamp(mana + _manaIncrease, 0f, MAX_MANA);        
    }

    public void RemoveMana(float _manaCost) {
        mana -= _manaCost;
    }

    public float GetMana() {
        return mana;
    }
}

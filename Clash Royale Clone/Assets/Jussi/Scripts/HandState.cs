using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandState
{
    public static readonly int CARDS_INITIALLY = 4;
    public static readonly int MAX_CARDS_IN_HAND = CARDS_INITIALLY;

    private List<Card> cards;
    private Card selected;
    private DeckState deckState;
    public Card GetSelected() {
        return selected;
    }
    public HandState (DeckState _deckState) {
        deckState = _deckState;
        cards = new List<Card>();

        for (int i = 0; i< CARDS_INITIALLY; i++) {
            Card card = deckState.NextCardFromDeck();
            card.state = Card.State.IN_HAND;
            cards.Add(card);
        }
    }

    public HandState() { // Need empty constructor, or else comes error "There is no argument given that corresponds to the required formal parameter"

    }

    public Card GetCardInIndex(int index) {
        return cards[index];
    }

    public int GetMaxCards() {
        return MAX_CARDS_IN_HAND;
    }    

    public void RemoveCardFromIndex(int cardIndex) {
        cards.RemoveAt(cardIndex);
    }

    public void DrawCardIntoIndex(int cardIndex, Card card) {
        card.state = Card.State.IN_HAND;
        cards.Insert(cardIndex, card);
    }

}

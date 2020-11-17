using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckState {

    public static readonly int MAX_CARDS_IN_DECK = 25;
    public List<Card> cards;
    
    public DeckState() {
        //For now, this creates a deck consisting of 25 random cards
        List<Card> randomCards = new List<Card>();
        for (int i = 0; i < MAX_CARDS_IN_DECK; i++) {
            randomCards.Add(Card.RandomCard());
        }

        cards = randomCards;
    }

    public Card RandomCardFromDeck() {
        int random = Random.Range(0, cards.Count);
        Card handCard = cards[random];
        cards.RemoveAt(random);
        return handCard;        
    }

    public void FillDeckWithCards(List<Card> _cards) {
        this.cards = _cards;
    }
}

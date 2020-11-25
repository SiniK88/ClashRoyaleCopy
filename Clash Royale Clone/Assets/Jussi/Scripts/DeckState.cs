using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckState {

    public static readonly int MAX_CARDS_IN_DECK = 5;
    public List<Card> cards;
    
    public DeckState(List<Card> deckCards) {
        cards = deckCards;
    }

    public Card NextCardFromDeck() {
        Card nextCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return nextCard;
    }

    public void InsertPlacedCardIntoDeck(Card card) {
        card.state = Card.State.IN_DECK;
        cards.Insert(0, card);
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

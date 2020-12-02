using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualizer : MonoBehaviour
{
    GameInitializer gameInit;    
    CardTypeContainer cardTypeContainer;
    List<CardType> cardTypes;

    public GameObject genericCardVisuals;
    public Sprite blueCard;
    public Sprite redCard;

    public Sprite[] numbers;

    public List<Player> players;

    List<List<GameObject>> playerCardPositions = new List<List<GameObject>>();
    public List<GameObject> cardPositionsP1;
    public List<GameObject> cardPositionsP2;

    List<List<CardVisuals>> cardVisuals = new List<List<CardVisuals>>();
    List<CardVisuals> cardVisualsP1 = new List<CardVisuals>();
    List<CardVisuals> cardVisualsP2 = new List<CardVisuals>();

    private void Awake() {
        cardTypeContainer = FindObjectOfType<CardTypeContainer>();
        cardTypes = cardTypeContainer.cardTypes;
    }

    private void Start() {
        //Initializing list of lists
        playerCardPositions.Add(cardPositionsP1);
        playerCardPositions.Add(cardPositionsP2);
        cardVisuals.Add(cardVisualsP1);
        cardVisuals.Add(cardVisualsP2);
   

        gameInit = FindObjectOfType<GameInitializer>();
        players = gameInit.players;

        for(int i = 0; i < players.Count; i++) {

            int MAX_CARDS_IN_HAND = players[i].handState.GetMaxCards();

            for (int k = 0; k < MAX_CARDS_IN_HAND; k++) {
                GameObject cardPlace = playerCardPositions[i][k];
                cardPlace.AddComponent<Selectable>();
                GameObject cardVisual = Instantiate(genericCardVisuals, cardPlace.transform); //This GameObject contains easy access to all the Sprites and Sprite Renderers
                cardVisuals[i].Add(cardVisual.GetComponent<CardVisuals>());
            }
            for (int k = 0; k < MAX_CARDS_IN_HAND; k++) {
                string playerID = "Player" + (i + 1).ToString();
                Selectable s = playerCardPositions[i][k].GetComponent<Selectable>();
                s.currentIndex = k;
                s.playerID = playerID;
                s.previous = playerCardPositions[i][(k - 1 + MAX_CARDS_IN_HAND) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
                s.next = playerCardPositions[i][(k + 1) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
            }
        }    
        UpdateCardVisuals();        
    }    

    public void UpdateCardVisuals() {

        for(int i = 0; i < players.Count; i++) {
            int MAX_CARDS_IN_HAND = players[i].handState.GetMaxCards();

            for (int k = 0; k < MAX_CARDS_IN_HAND; k++) {
                Card card = players[i].handState.GetCardInIndex(k);
                Card.Effect currentEffect = card.effect;
                CardType currentCard = null;
                for(int j = 0; j < cardTypes.Count; j++) {
                    if(cardTypes[j].cardType == currentEffect) {
                        currentCard = cardTypes[j];
                        break;
                    } else {
                        continue;
                    }
                }
                if(currentCard != null) {
                    Sprite _cardArt = null;
                    Sprite _artwork = null;
                    Sprite _manaCost = null;

                    //Card visuals, red or blue:
                    if(i == 0) {
                        _cardArt = blueCard;
                    } else if (i == 1) {
                        _cardArt = redCard;
                    }

                    //Card artwork visuals:
                    _artwork = currentCard.artwork;

                    //Manacost visuals:
                    int manaCost = Mathf.FloorToInt(currentCard.manaCost)/10;
                    _manaCost = (Sprite)numbers.GetValue(manaCost);
                    cardVisuals[i][k].RefreshCard(_cardArt, _artwork, _manaCost);
                } else {
                    Debug.Log("Currentcard is null");
                }
            }
        }        
    }

    public void UpdateSinceCardVisuals(int _playerIndex, int cardIndex) {
        Card card = players[_playerIndex].handState.GetCardInIndex(cardIndex);
        Card.Effect newEffect = card.effect;
        CardType newCard = null;
        for (int j = 0; j < cardTypes.Count; j++) {
            if (cardTypes[j].cardType == newEffect) {
                newCard = cardTypes[j];
                break;
            } else {
                continue;
            }
        }
        if (newCard != null) {
            Sprite _cardArt = null;
            Sprite _artwork = null;
            Sprite _manaCost = null;

            //Card visuals, red or blue:
            if (_playerIndex == 0) {
                _cardArt = blueCard;
            } else if (_playerIndex == 1) {
                _cardArt = redCard;
            }

            //Card artwork visuals:
            _artwork = newCard.artwork;

            //Manacost visuals:
            int manaCost = Mathf.FloorToInt(newCard.manaCost) / 10;
            _manaCost = (Sprite)numbers.GetValue(manaCost);
            cardVisuals[_playerIndex][cardIndex].RefreshCard(_cardArt, _artwork, _manaCost);
        } else {
            Debug.Log("Currentcard is null");
        }
    }
}

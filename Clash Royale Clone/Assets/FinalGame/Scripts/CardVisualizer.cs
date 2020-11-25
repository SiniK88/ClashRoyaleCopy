using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualizer : MonoBehaviour
{
    GameInitializer gameInit;    
    CardTypeContainer cardTypeContainer;
    List<CardType> cardTypes;

    public List<Player> players;
    public List<GameObject> cardPositionsP1;
    public List<GameObject> cardPositionsP2;
    List<List<GameObject>> playerCardPositions = new List<List<GameObject>>();
    public List<SpriteRenderer> renderersP1;
    public List<SpriteRenderer> renderersP2;
    List<List<SpriteRenderer>> playerCardRenderers = new List<List<SpriteRenderer>>();

    private void Awake() {
        cardTypeContainer = FindObjectOfType<CardTypeContainer>();
        cardTypes = cardTypeContainer.cardTypes;
    }

    private void Start() {
        playerCardPositions.Add(cardPositionsP1);
        playerCardPositions.Add(cardPositionsP2);
        playerCardRenderers.Add(renderersP1);
        playerCardRenderers.Add(renderersP2);

        gameInit = FindObjectOfType<GameInitializer>();
        players = gameInit.players;

        for(int i = 0; i < players.Count; i++) {

            int MAX_CARDS_IN_HAND = players[i].handState.GetMaxCards();

            for (int k = 0; k < MAX_CARDS_IN_HAND; k++) {
                GameObject go = playerCardPositions[i][k];
                go.AddComponent<Selectable>();

                playerCardRenderers[i].Add(go.GetComponent<SpriteRenderer>());
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
                    playerCardRenderers[i][k].sprite = currentCard.artwork;
                } else {
                    Debug.Log("Currentcard is null");
                }
            }
        }        
    }

    //private Color GetCardColor(Card.Effect effect) {
    //    switch (effect) {
    //        case Card.Effect.Tank: {
    //                return Color.red;
    //            }
    //        case Card.Effect.Knight: {
    //                return Color.yellow;
    //            }
    //        case Card.Effect.Archer: {
    //                return Color.green;
    //            }
    //        case Card.Effect.DarkKnight: {
    //                return Color.blue;
    //            }
    //        default:
    //            return Color.white;
    //    }
    //}
}

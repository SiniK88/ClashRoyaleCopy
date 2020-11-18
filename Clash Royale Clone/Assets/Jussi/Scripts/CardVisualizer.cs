using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualizer : MonoBehaviour
{
    GameInitializer gameInit;

    public List<Player> players;
    public List<GameObject> cardPositionsP1;
    public List<GameObject> cardPositionsP2;
    List<List<GameObject>> playerCardPositions = new List<List<GameObject>>();
    public List<SpriteRenderer> renderersP1;
    public List<SpriteRenderer> renderersP2;
    List<List<SpriteRenderer>> playerCardRenderers = new List<List<SpriteRenderer>>();

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
                s.playerID = playerID;
                s.previous = playerCardPositions[i][(k - 1 + MAX_CARDS_IN_HAND) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
                s.next = playerCardPositions[i][(k + 1) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
            }
        }

        

        UpdateCardVisuals();
        
    }

    private Color GetCardColor(Card.Effect effect) {
        switch (effect) {
            case Card.Effect.A: {
                    return Color.red;
                }
            case Card.Effect.B: {
                    return Color.yellow;
                }
            case Card.Effect.C: {
                    return Color.green;
                }
            case Card.Effect.D: {
                    return Color.blue;
                }
            default:
                return Color.white;
        }
    }

    public void UpdateCardVisuals() {

        for(int i = 0; i < players.Count; i++) {
            int MAX_CARDS_IN_HAND = players[i].handState.GetMaxCards();

            for (int k = 0; k < MAX_CARDS_IN_HAND; k++) {
                Card card = players[i].handState.GetCardInIndex(k);
                playerCardRenderers[i][k].material.color = GetCardColor(card.effect); //This could be the card image or any other instance visual
            }
        }
        
    }



    //public List<GameObject> cardPositions;
    //public List<Renderer> renderers;
    //public HandState handState;

    //private Color GetCardColor(Card.Effect effect) {
    //    switch(effect) {
    //        case Card.Effect.A:
    //            {
    //                return Color.red;
    //                break;
    //            }
    //        case Card.Effect.B: {
    //                return Color.green;
    //                break;
    //            }
    //        case Card.Effect.C: {
    //                return Color.blue;
    //                break;
    //            }
    //        default:
    //            return Color.white;
    //    }
    //}

    //// Start is called before the first frame update
    //void Start() {
    //    handState = new HandState();
    //    int MAX_CARDS_IN_HAND = HandState.MAX_CARDS_IN_HAND;
    //    for (int i = 0; i < MAX_CARDS_IN_HAND; i++) {
    //        GameObject gameObject = cardPositions[i];
    //        gameObject.AddComponent<Selectable>();
    //        renderers.Add(gameObject.GetComponent<Renderer>());
    //    }
    //    for (int i = 0; i < MAX_CARDS_IN_HAND; i++) {
    //        Selectable s = cardPositions[i].GetComponent<Selectable>();
    //        s.previous = cardPositions[(i - 1 + MAX_CARDS_IN_HAND) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
    //        s.next = cardPositions[(i + 1) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    for (int i = 0; i< HandState.MAX_CARDS_IN_HAND; i++) {
    //        Card card = handState.GetCardInIndex(i);
    //        Renderer renderer = renderers[i];
    //        if (card != null) {
    //            renderer.enabled = true;
    //            renderer.material.color = GetCardColor(card.effect);
    //        }
    //        else {
    //            renderer.enabled = false;
    //        }
    //    }
    //}
}

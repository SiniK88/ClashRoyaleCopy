using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualizer : MonoBehaviour
{
    GameInitializer gameInit;

    public List<Player> players;
    public List<GameObject> cardPositions;
    public List<Renderer> renderers;

    int MAX_CARDS_IN_HAND;

    private void Start() {
        gameInit = FindObjectOfType<GameInitializer>();
        players = gameInit.players;

        MAX_CARDS_IN_HAND = players[0].handState.GetMaxCards();

        for(int i = 0; i < MAX_CARDS_IN_HAND; i++) {
            GameObject go = cardPositions[i];
            go.AddComponent<Selectable>();
            renderers.Add(go.GetComponent<Renderer>());
        }
        for (int i = 0; i < MAX_CARDS_IN_HAND; i++) {
            Selectable s = cardPositions[i].GetComponent<Selectable>();
            s.previous = cardPositions[(i - 1 + MAX_CARDS_IN_HAND) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
            s.next = cardPositions[(i + 1) % MAX_CARDS_IN_HAND].GetComponent<Selectable>();
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
        for(int i = 0; i < MAX_CARDS_IN_HAND; i++) {
            Card card = players[0].handState.GetCardInIndex(i);
            renderers[i].material.color = GetCardColor(card.effect); //This could be the card image or any other instance visual
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

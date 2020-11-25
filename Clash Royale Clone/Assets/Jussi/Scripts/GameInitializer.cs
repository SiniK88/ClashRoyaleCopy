using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInitializer : MonoBehaviour
{
    List<Card> player1cards = new List<Card>();
    List<Card> player2cards = new List<Card>();

    public Player player1;
    public Player player2;
    public List<Player> players = new List<Player>();
    public GameObject playerController;
    public GameObject playerCursor;

    bool playersIsHuman = true;
    CardVisualizer cardVisualizer;


    private void Awake() {

        //We create the Player Decks here manually. In the end product, the list is already made when we reach this Scene, and we can directly use those lists instead of creating here
        player1cards.Add(new Card(Card.Effect.Tank,             Card.State.IN_DECK));
        player1cards.Add(new Card(Card.Effect.Knight,           Card.State.IN_DECK));
        player1cards.Add(new Card(Card.Effect.Archer,           Card.State.IN_DECK));
        player1cards.Add(new Card(Card.Effect.DarkKnight,       Card.State.IN_DECK));
        player1cards.Add(new Card(Card.Effect.InfernoDragon,    Card.State.IN_DECK));

        player2cards.Add(new Card(Card.Effect.Tank,             Card.State.IN_DECK));
        player2cards.Add(new Card(Card.Effect.Knight,           Card.State.IN_DECK));
        player2cards.Add(new Card(Card.Effect.Archer,           Card.State.IN_DECK));
        player2cards.Add(new Card(Card.Effect.DarkKnight,       Card.State.IN_DECK));
        player2cards.Add(new Card(Card.Effect.InfernoDragon,    Card.State.IN_DECK));


        player1 = new Player(playersIsHuman, player1cards);
        player2 = new Player(playersIsHuman, player2cards);
        players.Add(player1);
        players.Add(player2);


        for (int i = 0; i < players.Count; i++) {

            //Set the playerID in the Player-object
            string newPlayerID = ("Player" + (i + 1).ToString());
            players[i].SetPlayerID(newPlayerID);
            
            //Create an individual input system for each player
            GameObject newPlayer = Instantiate(playerController);
            PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();
            newPlayer.name = newPlayerID;
            newPlayerController.playerID = newPlayerID;

            //Add a SelectionManager component to each player
            newPlayer.AddComponent<SelectionManager>();
            newPlayer.GetComponent<SelectionManager>()._playerID = newPlayerID;

            //Create an individual cursor for each player
            GameObject newCursor = Instantiate(playerCursor);
            PlacementCursor placementCursor = newCursor.GetComponent<PlacementCursor>();
            newCursor.name = "CursorP" + (i + 1).ToString();
            placementCursor.playerID = newPlayerID;
        }

        cardVisualizer = FindObjectOfType<CardVisualizer>();
    }

    private void OnEnable() {
        SelectionManager.OnPlaceCard += OnCardPlacement; 
    }
    private void OnDisable() {
        SelectionManager.OnPlaceCard -= OnCardPlacement;
    }

    public void OnCardPlacement(int _playerIndex, int _cardIndex, int _manaCost) {

        print("Now wwe operate!");

        //Manacost operations here...

        //Gamelogic handling the card rotation: manipulates the handstate and deckstate
        Player currentPlayer = players[_playerIndex];
        Card placedCard = currentPlayer.handState.GetCardInIndex(_cardIndex);

        currentPlayer.handState.RemoveCardFromIndex(_cardIndex);
        currentPlayer.deckState.InsertPlacedCardIntoDeck(placedCard);
        Card nextCard = currentPlayer.deckState.NextCardFromDeck();
        print(nextCard.effect);
        currentPlayer.handState.DrawCardIntoIndex(_cardIndex, nextCard);

        //Now visual stuff
        cardVisualizer.UpdateCardVisuals();

    }
}

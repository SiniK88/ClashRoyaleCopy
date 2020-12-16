using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInitializer : MonoBehaviour
{
    List<Card> differentCards = new List<Card>();

    List<Card> player1cards = new List<Card>();
    List<Card> player2cards = new List<Card>();

    public Player player1;
    public Player player2;
    public List<Player> players = new List<Player>();
    public GameObject playerCursor;
    public GameObject playerInput;



    bool playersIsHuman = true;
    CardVisualizer cardVisualizer;

    float manaRegenSpeed = 5f;

    private void Start() {
        var player1 = PlayerInput.all[0];
        var player2 = PlayerInput.all[1];

        // Discard existing assignments.
        player1.user.UnpairDevices();
        player2.user.UnpairDevices();

        // Assign devices and control schemes.
        var gamepadCount = Gamepad.all.Count;
        if (gamepadCount >= 2) {


            UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(Gamepad.all[0], user: player1.user);
            UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(Gamepad.all[1], user: player2.user);

            player1.user.ActivateControlScheme("Gamepad");
            player2.user.ActivateControlScheme("Gamepad");
        } else if (gamepadCount == 1) {
            UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(Gamepad.all[0], user: player1.user);
            UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(Keyboard.current, user: player2.user);

            player1.user.ActivateControlScheme("Gamepad");
            player2.user.ActivateControlScheme("KeyboardLeftSide");
        } else {
            UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(Keyboard.current, user: player1.user);
            UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(Keyboard.current, user: player2.user);

            player1.user.ActivateControlScheme("KeyboardLeftSide");
            player2.user.ActivateControlScheme("KeyboardRightSide");
        }
    }

    public void CreateRandomDeck(List<Card> playerList) {
        List<int> guessedNumbers = new List<int>();
        while (playerList.Count < differentCards.Count) {
            int randomNumber = Random.Range(0, differentCards.Count);
            if (!guessedNumbers.Contains(randomNumber)) {
                guessedNumbers.Add(randomNumber);
                playerList.Add(differentCards[randomNumber]);
            }
        }
    }

    private void Awake() {

        differentCards.Add(new Card(Card.Effect.Tank,             Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.Knight,           Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.DarkKnight,       Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.InfernoDragon,    Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.FireBall,         Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.IceKnife,         Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.Barrel,           Card.State.IN_DECK));
        differentCards.Add(new Card(Card.Effect.Archer,           Card.State.IN_DECK));

        CreateRandomDeck(player1cards);
        CreateRandomDeck(player2cards);

        player1 = new Player(playersIsHuman, player1cards);
        player2 = new Player(playersIsHuman, player2cards);
        players.Add(player1);
        players.Add(player2);        

        for (int i = 0; i < players.Count; i++) {

            //Set the playerID in the Player-object
            string newPlayerID = ("Player" + (i + 1).ToString());
            players[i].SetPlayerID(newPlayerID);

            ////Create an individual input system for each player
            //GameObject newPlayer = Instantiate(playerInput);
            //var newPlayer = PlayerInput.Instantiate(playerInput,i+1,controlScheme: "")
            //PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();
            //newPlayer.name = newPlayerID;
            //newPlayerController.playerID = newPlayerID;
            //PlayerInput input = newPlayerController.transform.GetComponent<PlayerInput>();
            //input.currentControlScheme 

            ////Add a SelectionManager component to each player
            //newPlayer.AddComponent<SelectionManager>();
            //newPlayer.GetComponent<SelectionManager>()._playerID = newPlayerID;

            ////Create an individual cursor for each player
            //GameObject newCursor = Instantiate(playerCursor);
            //PlacementCursor placementCursor = newCursor.GetComponent<PlacementCursor>();
            //newCursor.name = "CursorP" + (i + 1).ToString();
            //placementCursor.playerID = newPlayerID;
        }

        cardVisualizer = FindObjectOfType<CardVisualizer>();
    }

    private void OnEnable() {
        SelectionManager.OnPlaceCard += OnCardPlacement; 
    }
    private void OnDisable() {
        SelectionManager.OnPlaceCard -= OnCardPlacement;
    }

    public void OnCardPlacement(int _playerIndex, int _cardIndex) {

        //Gamelogic handling the card rotation: manipulates the handstate and deckstate
        Player currentPlayer = players[_playerIndex];
        Card placedCard = currentPlayer.handState.GetCardInIndex(_cardIndex);

        currentPlayer.handState.RemoveCardFromIndex(_cardIndex);
        currentPlayer.deckState.InsertPlacedCardIntoDeck(placedCard);
        Card nextCard = currentPlayer.deckState.NextCardFromDeck();
        currentPlayer.handState.DrawCardIntoIndex(_cardIndex, nextCard);

        //Now visual stuff
        cardVisualizer.UpdateSinceCardVisuals(_playerIndex, _cardIndex);

    }

    private void Update() {

        for(int i = 0; i < players.Count; i++) {
            float manaAddition = manaRegenSpeed * Time.deltaTime;
            players[i].AddMana(manaAddition);
        }
    }
}

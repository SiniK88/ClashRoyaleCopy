using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInitializer : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public List<Player> players = new List<Player>();
    public GameObject playerController;
    public GameObject playerCursor;

    bool playersIsHuman = true;   


    private void Awake() {
        player1 = new Player(playersIsHuman);
        player2 = new Player(playersIsHuman);
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
    }
}

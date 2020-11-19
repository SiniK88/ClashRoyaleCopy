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
            
            //Create an individual input system for each player
            GameObject newPlayer = Instantiate(playerController);
            PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();
            newPlayer.name = "Player" + (i+1).ToString();
            newPlayerController.playerID = "Player" + (i + 1).ToString();

            //Create an individual cursor for each player
            GameObject newCursor = Instantiate(playerCursor);
            PlacementCursor placementCursor = newCursor.GetComponent<PlacementCursor>();
            newCursor.name = "CursorP" + (i + 1).ToString();
            placementCursor.playerID = "Player" + (i + 1).ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInitializer : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public List<Player> players = new List<Player>();
    public GameObject playerInput;
    PlayerInputManager playerInputManager;

    bool playersIsHuman = true;

    private void Awake() {
        player1 = new Player(playersIsHuman);
        player2 = new Player(playersIsHuman);
        players.Add(player1);
        players.Add(player2);


        for (int i = 0; i < players.Count; i++) {
            GameObject newPlayer = PlayerInput.Instantiate(playerInput);
            PlayerInput newPlayerInput = newPlayer.GetComponent<PlayerInput>();
            newPlayerInput.SetPlayer(players[i]);
            print("Playerobject: " + players[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public List<Player> players = new List<Player>();

    bool playersIsHuman = true;

    private void Awake() {
        player1 = new Player(playersIsHuman);
        player2 = new Player(playersIsHuman);
        players.Add(player1);
        players.Add(player2);

    }
}

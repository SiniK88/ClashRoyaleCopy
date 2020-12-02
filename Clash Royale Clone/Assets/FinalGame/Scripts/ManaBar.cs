using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    GameInitializer gameInit;
    List<Player> players;

    public GameObject mask;
    [Range(0,1)] public int playerIndex; 

    float maxMana;
    float currentMana;
    [Range(0, 1)] float fillAmount;

    private void Awake() {
        maxMana = Player.MAX_MANA;
        gameInit = FindObjectOfType<GameInitializer>();
        players = gameInit.players;
    }

    private void Update() {
        currentMana = players[playerIndex].GetMana();
        fillAmount = players[playerIndex].GetMana() / maxMana;
        mask.transform.localScale = new Vector3(1, fillAmount, 1);
    }
}

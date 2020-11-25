using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New CardType", menuName = "Card")]

public class CardType : ScriptableObject {

    public Card.Effect cardType;
    public string cardName;

    public Sprite artwork;

    public int manaCost;
    public LayerMask disallowedPlacements; //Where the object can be placed

    public GameObject placerVisuals; //How the object looks mid-placement
    public GameObject finalForm; //The actual minion or spell
}

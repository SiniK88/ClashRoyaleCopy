using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New CardType", menuName = "Card")]

public class CardType : ScriptableObject {

    public Card.Effect cardType;
    public string cardName;

    public Sprite artwork;

    public float manaCost;
    public LayerMask disallowedPlacements; //Where the object can be placed

    public GameObject placerVisuals; //Continuous cursor, which is limited only by the borders
    public GameObject placerGhostVisuals; //Discrete cursor, which shows where the unit will be dropped
    public GameObject finalForm; //The actual minion or spell
}

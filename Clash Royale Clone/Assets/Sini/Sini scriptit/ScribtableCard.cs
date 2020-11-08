using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = " Minion")]
public class ScribtableCard : ScriptableObject
{

    public string cardname;
    public int manaCost;

    public Sprite artwork;
    public Sprite borderArt;
    // layermask stuff

    // placer prefab, here or different script?


}

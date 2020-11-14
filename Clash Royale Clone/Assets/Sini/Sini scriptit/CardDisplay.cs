using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // should be able to update these while game is running
    public ScribtableCard card;

    public Image artworkImage;
    public Image borderImage;
    public Text manaText; 
   
    void Start()
    {
        Debug.Log(card.name);
        artworkImage.sprite = card.artwork;
        borderImage.sprite = card.borderArt;
        manaText.text = card.manaCost.ToString();

    }




}

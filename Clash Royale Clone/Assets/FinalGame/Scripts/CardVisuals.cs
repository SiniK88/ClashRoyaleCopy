using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisuals : MonoBehaviour
{
    SpriteRenderer cardArtRend;
    SpriteRenderer artworkRend;
    SpriteRenderer manaCostRend;

    public Sprite cardArt;
    public Sprite artwork;
    public Sprite manaCost;

    private void Awake() {
        cardArtRend = transform.Find("CardArt").GetComponent<SpriteRenderer>();
        artworkRend = transform.Find("Artwork").GetComponent<SpriteRenderer>();
        manaCostRend = transform.Find("ManaCost").GetComponent<SpriteRenderer>();

        cardArtRend.sprite = cardArt;
        artworkRend.sprite = artwork;
        manaCostRend.sprite = manaCost;
    }

    public void RefreshCard(Sprite _cardArt, Sprite _artwork, Sprite _manaCost) {
        cardArtRend.sprite = _cardArt;
        artworkRend.sprite = _artwork;
        manaCostRend.sprite = _manaCost;
    }
}

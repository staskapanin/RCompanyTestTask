using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsPresenter : MonoBehaviour
{
    [SerializeField] SpriteRenderer _cardSpriteRenderer;

    public void ShowCard(byte[] bytes)
    {
        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(bytes);

        _cardSpriteRenderer.sprite =
            Sprite.Create(texture2D, new Rect(0, 0, texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));
    }
    
}

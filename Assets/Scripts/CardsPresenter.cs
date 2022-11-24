using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsPresenter : MonoBehaviour
{
    const float CellWidth = 3f;

    [SerializeField] CardScript cardPrefab;

    CardScript[] _cards;
    int _cardsCount;
    
    public void InitializePresenter(int cardsCount)
    {
        _cardsCount = cardsCount;
        GenerateCards();
    }
    
    public void ShowCard(int index, byte[] imageBytes)
    {
        Sprite sprite = SpriteFactory(imageBytes);
        _cards[index].Show(sprite);
    }

    public void PlayDownloadAnimationAll()
    {
        foreach (var card in _cards)
            card.PlayDownloadAnimation();
    }

    private void GenerateCards()
    {
        _cards = new CardScript[_cardsCount];

        for (int i = 0; i < _cards.Length; i++)
        {
            CardScript cs = Instantiate(cardPrefab);
            _cards[i] = cs;
            cs.transform.SetParent(transform);

            float x = (i - (_cardsCount - 1) / 2f) * CellWidth;
            cs.transform.localPosition = new Vector2(x, 0);
        }
    }

    private Sprite SpriteFactory(byte[] bytes)
    {
        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(bytes);

        Sprite result =
            Sprite.Create(texture2D, new Rect(0, 0, texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));

        return result;
    }
}

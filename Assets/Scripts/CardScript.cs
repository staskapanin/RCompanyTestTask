using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public void Show(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    
}

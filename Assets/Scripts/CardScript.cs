using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    const float QuarterRotationTime = 0.15f;

    [SerializeField] SpriteRenderer spriteRenderer;
    Sequence _downloadSequence;
    public void Show(Sprite sprite)
    {
        _downloadSequence.Kill();
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = sprite;
    }
    
    public void PlayDownloadAnimation()
    {
        spriteRenderer.sprite = null;

        _downloadSequence = DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 90f, 0), QuarterRotationTime))
            .Append(transform.DORotate(new Vector3(0, 180f, 0), QuarterRotationTime))
            .Append(transform.DORotate(new Vector3(0, 270f, 0), QuarterRotationTime))
            .Append(transform.DORotate(new Vector3(0, 0f, 0), QuarterRotationTime))
            .SetLoops(-1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] CardsPresenter cardsPresenter;
    private ImageDownloader _imageDownloader;

    private void Awake()
    {
        _imageDownloader = new ImageDownloader();
    }

    public async void ShowCard()
    {
        var bytes = await _imageDownloader.DownloadImageClient();
        
        cardsPresenter.ShowCard(bytes);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] CardsPresenter cardsPresenter;
    private ImageDownloader _imageDownloader;

    private void Awake()
    {
        _imageDownloader = new ImageDownloader();
    }

    public async Task ShowCard()
    {
        var bytes = await _imageDownloader.DownloadImageClient();
        
        cardsPresenter.ShowCard(bytes);
    }

    public async Task ShowAllAtOnce()
    {
        Debug.Log("AllAtOnce");
    }

    public async Task ShowWhenReady()
    {
        Debug.Log("WhenReady");
    }

    public async Task ShowOneByOne()
    {
        Debug.Log("OneByOne");
    }
}

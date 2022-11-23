using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField][Range(4, 6)] int cardsCount;

    [SerializeField] CardsPresenter cardsPresenter;
    
    private ImageDownloader _imageDownloader;

    private void Awake()
    {
        _imageDownloader = new ImageDownloader();
    }

    private void Start()
    {
        cardsPresenter.InitializePresenter(cardsCount);
    }

    public async Task ShowAllAtOnce()
    {
        Sprite[] sprites = new Sprite[cardsCount];
        byte[][] bytesPerCard = new byte[cardsCount][];

        for (int i = 0; i < sprites.Length; i++)
            bytesPerCard[i] = await _imageDownloader.DownloadRandomImageAsync();

        for (int i = 0; i < sprites.Length; i++)
            cardsPresenter.ShowCard(i, bytesPerCard[i]);
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

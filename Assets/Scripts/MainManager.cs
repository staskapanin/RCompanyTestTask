using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField][Range(4, 6)] int cardsCount;

    [SerializeField] CardsPresenter cardsPresenter;
    [SerializeField] UIController uiController;
    
    private ImageDownloader _imageDownloader;

    //For drawing in main thread in ShowWhenReady method
    private Queue<(int, byte[])> cardsDrawQueue = new Queue<(int, byte[])>();
    object locker = new object();

    private void Awake()
    {
        _imageDownloader = new ImageDownloader();
    }

    private void Start()
    {
        cardsPresenter.InitializePresenter(cardsCount);
    }

    private void Update()
    {
        DrawCardsInQueue();
    }

    public async Task ShowAllAtOnce()
    {
        uiController.DisableButtons();
        cardsPresenter.PlayDownloadAnimationAll();
        var tasks = new List<Task<(int, byte[])>>();

        for (int i = 0; i < cardsCount; i++)
        {
            int index = i;
            tasks.Add(Task.Run<(int, byte[])>(async () =>
            {
                byte[] bs = await _imageDownloader.DownloadRandomImageAsync();
                return (index, bs);
            }));
        }

        Task<(int, byte[])[]> tAll = Task.WhenAll(tasks);
        var results = await tAll;

        foreach(var r in results)
            cardsPresenter.ShowCard(r.Item1, r.Item2);

        uiController.EnableButtons();
    }

    public async Task ShowWhenReady()
    {
        uiController.DisableButtons();
        cardsPresenter.PlayDownloadAnimationAll();
        var tasks = new List<Task>();

        for (int i = 0; i < cardsCount; i++)
        {
            int index = i;
            tasks.Add(Task.Run(async () =>
            {
                var bs = await _imageDownloader.DownloadRandomImageAsync();
                lock(locker)
                {
                    cardsDrawQueue.Enqueue((index, bs));
                }
            }));
        }

        await Task.WhenAll(tasks);
        uiController.EnableButtons();
    }

    public async Task ShowOneByOne()
    {
        uiController.DisableButtons();
        cardsPresenter.PlayDownloadAnimationAll();

        for (int i = 0; i < cardsCount; i++)
        {
            byte[] bytes = await _imageDownloader.DownloadRandomImageAsync();
            cardsPresenter.ShowCard(i, bytes);
        }

        uiController.EnableButtons();
    }

    private void DrawCardsInQueue()
    {
        lock (locker)
        {
            while (cardsDrawQueue.Count > 0)
            {
                (int, byte[]) t = cardsDrawQueue.Dequeue();
                cardsPresenter.ShowCard(t.Item1, t.Item2);
            }
        }
    }
}

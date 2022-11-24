using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField][Range(4, 6)] int cardsCount;

    [SerializeField] CardsPresenter cardsPresenter;
    
    private ImageDownloader _imageDownloader;
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
        lock (locker)
        {
            while (cardsDrawQueue.Count > 0)
            {
                (int,byte[]) t = cardsDrawQueue.Dequeue();
                cardsPresenter.ShowCard(t.Item1, t.Item2);
            } 
        }
    }

    public async Task ShowAllAtOnce()
    {
        cardsPresenter.PlayDownloadAnimationAll();
        List<Task<(int, byte[])>> tasks = new List<Task<(int, byte[])>>();

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
    }

    public async Task ShowWhenReady()
    {
        cardsPresenter.PlayDownloadAnimationAll();

        for (int i = 0; i < cardsCount; i++)
        {
            int index = i;
            Task.Run(async () =>
            {
                var bs = await _imageDownloader.DownloadRandomImageAsync();
                lock(locker)
                {
                    cardsDrawQueue.Enqueue((index, bs));
                }
            });
        }
    }

    public async Task ShowOneByOne()
    {
        cardsPresenter.PlayDownloadAnimationAll();

        for (int i = 0; i < cardsCount; i++)
        {
            byte[] bytes = await _imageDownloader.DownloadRandomImageAsync();
            cardsPresenter.ShowCard(i, bytes);
        }
    }
}

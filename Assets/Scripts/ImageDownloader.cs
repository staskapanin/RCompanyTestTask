using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http.Headers;

public class ImageDownloader
{
    HttpClient client = new HttpClient();
    public ImageDownloader()
    {
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyBestApp" , "0.1"));
    }

    public async Task<byte[]> DownloadImageClient()
    {
        var imageBytes = new byte[0];

        try
        {
            using HttpResponseMessage res = await client.GetAsync("https://picsum.photos/200/300");

            imageBytes = await res.Content.ReadAsByteArrayAsync();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Download image error: " + e);
        }

        return imageBytes;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class RestAPI
{
    private static double timeout = 5.0f;

    private static async UniTaskVoid RequestAsync(UnityWebRequest req, RestAPIClass<string> reVal)
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfterSlim(TimeSpan.FromSeconds(timeout));

        try
        {
            var res = await req.SendWebRequest().WithCancellation(cts.Token);

            var results = res.downloadHandler.data;
            var message = Encoding.UTF8.GetString(results);
            
            reVal.OnComplete?.Invoke(message);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(req.responseCode);
        }
        
        req.Dispose();
    }

    public static RestAPIClass<string> Get(string uri, string data)
    {
        var url = $"Main Url/{uri}";
        RestAPIClass<string> reVal = new RestAPIClass<string>();

        //byte[] bodyRow = null;
        var request = new UnityWebRequest(url, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        
        // Token Set
        // reqeust.SetRequestHeader("Authorization",token)
        
        request.SetRequestHeader("Content-Type","application/json");
        
        RequestAsync(request,reVal).Forget();
        
        return reVal;
    }
    
    public static RestAPIClass<string> Post(string uri, string data)
    {
        var url = $"Main Url/{uri}";
        RestAPIClass<string> reVal = new RestAPIClass<string>();

        byte[] bodyRow = null;
        if (data != null) bodyRow = Encoding.UTF8.GetBytes(data);
        
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRow);
        request.downloadHandler = new DownloadHandlerBuffer();
        
        // Token Set
        // reqeust.SetRequestHeader("Authorization",token)
        
        request.SetRequestHeader("Content-Type","application/json");
        RequestAsync(request, reVal).Forget();

        return reVal;
    }
    
    public static RestAPIClass<string> Put(string url)
    {
        return null;
    }
    
    public static RestAPIClass<string> Delete(string url)
    {
        return null;
    }
}

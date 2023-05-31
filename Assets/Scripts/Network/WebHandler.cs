
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class WebHandler
    {
    #region Public Methods

    public WebHandler(NetworkModule context)
    {
        _context = context;
    }

    #endregion

    #region Public Methods


    /// <summary>
    /// 웹서버에서 Json데이터를 받아오는 함수, 서버의 응답에 대한 처리도 실시한다
    /// </summary>
    public void Get(string url, Action<string> callback)
    {
        _context.StartCoroutine(GetRequestCoroutine(url, callback));
    }
    

    /// <summary>
    /// 웹서버에 Json데이터를 보내는 함수, 서버의 응답에 대한 처리도 실시한다
    /// </summary>
    public void Post(string url, string jsonData, Action<string> callback)
    {
        _context.StartCoroutine(PostRequestCoroutine(url, jsonData, callback));
    }

    #endregion
    
    #region Private values

    private NetworkModule _context;

    #endregion

    #region Private Methods
    
    private IEnumerator GetRequestCoroutine(string url, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("GET request error: " + www.error);
            callback?.Invoke(null);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("GET request successful!");
            Debug.Log("Response: " + response);
            callback?.Invoke(response);
        }
        
        www.Dispose();
    }
    
    private IEnumerator PostRequestCoroutine(string url, string jsonData, Action<string> callback)
    {
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        www.uploadHandler = new UploadHandlerRaw(jsonBytes);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST request error: " + www.error);
            callback?.Invoke(null);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("POST request successful!");
            Debug.Log("Response: " + response);
            callback?.Invoke(response);
        }
        
        www.Dispose();
    }

    #endregion
    }
}


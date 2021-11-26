using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient : MonoBehaviour
{
    public const int TIME_OUT = 15;

    public string Data { get; set; }
    public int Status { get; set; }
    public string ErrorMsg { get; set; }

    public HttpClientBuilder httpClientBuilder;
    private UnityWebRequest www;
    private bool isStartRequest;

    public Action OnError { get; set; }
    public Action OnSuccess { get; set; }
    public Action OnPost { get; set; }
    public bool IsEncode;

    public static HttpClient Create(GameObject gameObject, HttpClientBuilder httpClientBuilder)
    {
        HttpClient httpClient = gameObject.AddComponent<HttpClient>();
        httpClient.httpClientBuilder = httpClientBuilder;
        return httpClient;
    }

    public void Request()
    {
        if (!isStartRequest)
        {
            isStartRequest = true;
            switch (httpClientBuilder.httpMethod)
            {
                case HttpMethod.Get:
                    StartCoroutine(Get());
                    break;
                case HttpMethod.Post:
                    StartCoroutine(Post());
                    break;
                case HttpMethod.Put:
                    StartCoroutine(Put());
                    break;
                default:
                    StartCoroutine(Get());
                    break;
            }
        }
    }

    private IEnumerator Get()
    {
        string url = httpClientBuilder.Build();
        if (!string.IsNullOrEmpty(httpClientBuilder.body))
        {
            www = UnityWebRequest.Post(url, httpClientBuilder.body);
            byte[] bodyBytes = Encoding.ASCII.GetBytes(httpClientBuilder.body);
            www.uploadHandler = new UploadHandlerRaw(bodyBytes);
        }

        www = UnityWebRequest.Get(url);
        www.timeout = TIME_OUT;
        SetHeaders();

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            HandleError();
        }
        else
        {
            HandleResponse();
        }
    }

    private IEnumerator Post()
    {
        OnPost?.Invoke();

        string url = httpClientBuilder.Build();
        if (!string.IsNullOrEmpty(httpClientBuilder.body))
        {
            www = UnityWebRequest.Post(url, httpClientBuilder.body);
            byte[] bodyBytes = Encoding.UTF8.GetBytes(httpClientBuilder.body);
            www.uploadHandler = new UploadHandlerRaw(bodyBytes);
        }
        else if (httpClientBuilder.multiPartBodyData.Count > 0)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            foreach (IMultipartFormSection section in httpClientBuilder.multiPartBodyData)
            {
                formData.Add(section);
            }

            www = UnityWebRequest.Post(url, formData);
        }

        www.timeout = TIME_OUT;
        SetHeaders();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            HandleError();
        }
        else
        {
            HandleResponse();
        }
    }

    private IEnumerator Put()
    {
        string url = httpClientBuilder.Build();
        www = UnityWebRequest.Put(url, httpClientBuilder.uploadBytes);
        www.timeout = TIME_OUT;
        SetHeaders();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            HandleError();
        }
        else
        {
            HandleResponse();
        }
    }

    private void SetHeaders()
    {
        // Custom
        if (httpClientBuilder.headers.Count > 0)
        {
            foreach (KeyValuePair<string, object> kvp in httpClientBuilder.headers)
            {
                www.SetRequestHeader(kvp.Key, kvp.Value.ToString());
            }
        }

        // Content type
        if (!string.IsNullOrEmpty(httpClientBuilder.contentType))
        {
            www.SetRequestHeader("Content-Type", httpClientBuilder.contentType);
        }
    }

    private void HandleResponse()
    {
        Status = (int) www.responseCode;
        if (www.isNetworkError || www.isHttpError)
        {
            Data = null;
            ErrorMsg = www.error;
            OnError?.Invoke();
        }
        else
        {
            byte[] results = www.downloadHandler.data;
            if (IsEncode)
            {
                Data = Convert.ToBase64String(results);
            }
            else
            {
                Data = Encoding.UTF8.GetString(results);
            }

            OnSuccess?.Invoke();
        }

        // Destroy
        Destroy(this);
    }

    private void HandleError()
    {
        Debug.Log(www.error);
        OnError?.Invoke();

        // Destroy
        Destroy(this);
    }

    public void Stop()
    {
        if (www != null && !www.isDone)
        {
            www.Dispose();

            // destroy
            Destroy(this);
        }
    }
}
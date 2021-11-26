using System;
using System.Text;
using SimpleJSON;
using UnityEngine;

public enum ApiRequestType
{
    Common
}

public abstract class BaseAPI
{
    public HttpClient httpClient { get; set; }
    public Action<string> OnSuccess { get; set; }
    public Action<string, int> OnError { get; set; }
    public Action<HttpClient, int> OnOtherParm { get; set; }
    public Action<string> OnCacheRestore { get; set; }
    public Action OnPost { get; set; }
    public int Expired { get; set; } //时间段  period

    public int ExpiredTime { get; set; } //时间戳

    public bool ForceRequest { get; set; }
    public bool isEncode { get; set; }

    public ApiRequestType RequestType { get; set; }

    protected GameObject gameObject;

    protected HttpClientBuilder httpClientBuilder;

    public BaseAPI(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    protected void SendRequest(HttpClientBuilder httpClientBuilder)
    {
        this.httpClientBuilder = httpClientBuilder;

        DoRequest(httpClientBuilder);
    }

    private void DoRequest(HttpClientBuilder httpClientBuilder)
    {
        string basicHeader = Reg.PlatformAPI.GetHttpHeader();

        if (!string.IsNullOrEmpty(basicHeader))
        {
            httpClientBuilder.Header("Basic-Info", basicHeader);
        }

        httpClient = HttpClient.Create(gameObject, httpClientBuilder);
        httpClient.OnError = OnHttpError;
        httpClient.OnSuccess = OnHttpSuccess;
        httpClient.OnPost = OnHttpPost;
        httpClient.IsEncode = isEncode;
        httpClient.Request();
    }

    protected void OnHttpError()
    {
#if UNITY_EDITOR
        string body = httpClient.httpClientBuilder.body;
        if (!string.IsNullOrEmpty(body))
        {
            body = e392323d23a.eud(body);
        }
        
        Debug.Log(
            $"{httpClient.httpClientBuilder.Build()} | body:{body}\nOnHttpError >>>>>>>\n\n{httpClient.ErrorMsg}\n");
#endif

        OnError?.Invoke(httpClient.ErrorMsg, -1);
    }

    protected void OnHttpSuccess()
    {
#if UNITY_EDITOR
        string body = httpClient.httpClientBuilder.body;
        if (!string.IsNullOrEmpty(body))
        {
            body = e392323d23a.eud(body);
        }
        
        Debug.Log(
            $"{httpClient.httpClientBuilder.Build()} | body:{body}\nOnHttpSuccess >>>>>>>\n\n{(isEncode ? Reg.PlatformAPI.eud(httpClient.Data) : httpClient.Data)}\n");
#endif

        if (string.IsNullOrEmpty(httpClient.Data))
        {
            OnError?.Invoke("Data is empty!", 0);
            return;
        }

        // Debug.Log("????   "+httpClient.Data);
        
        try
        {
            JSONNode jsonNode;
            if (isEncode)
            {
                string str = Reg.PlatformAPI.eud(httpClient.Data);
                if (string.IsNullOrEmpty(str))
                {
                    OnError("encode fail", 1);
                    return;
                }
                else
                {
                    jsonNode = JSONNode.Parse(str); //解析报错！！！！！！！！！！！！！！！！！！！！！！！！
                    if (jsonNode["code"] != null && jsonNode["code"].AsInt > 0)
                    {
                        OnError?.Invoke(jsonNode["msg"], jsonNode["code"]);

                        OnOtherParm?.Invoke(httpClient, jsonNode["code"]);
                        return;
                    }
                }
            }
            else
            {
                jsonNode = JSONNode.Parse(httpClient.Data);
                if (jsonNode["code"] != null && jsonNode["code"].AsInt > 0)
                {
                    OnError?.Invoke(jsonNode["msg"], jsonNode["code"]);

                    return;
                }
            }

            // 获取过期时间戳
            ExpiredTime = ParseExpiredTime(jsonNode);
        }
        catch (Exception e)
        {
            var label = httpClient.httpClientBuilder.paths.Count > 0 ? httpClient.httpClientBuilder.paths[0] : "";
            Reg.PlatformAPI.ReportRealTimeEvent("api", "parse error", label);
            OnError?.Invoke("parse error", -1);
            return;
        }

        // 回调成功
        OnSuccess?.Invoke(httpClient.Data);
    }

    /// <summary>
    /// 从接口返回数据中，获取服务器指定的缓存过期时间戳
    /// </summary>
    /// <param name="rootNode">json的根结点</param>
    /// <returns>过期时间戳</returns>
    private int ParseExpiredTime(JSONNode rootNode)
    {
        if (null != rootNode && rootNode.HasKey("data"))
        {
            JSONNode dataNode = rootNode["data"];
            if (null != dataNode && dataNode.HasKey("expireTime"))
            {
                return dataNode["expireTime"].AsInt;
            }
        }

        return 0;
    }

    protected void OnHttpPost()
    {
        OnPost?.Invoke();
    }

    public void Stop()
    {
        if (httpClient != null)
        {
            httpClient.Stop();
        }
    }
}
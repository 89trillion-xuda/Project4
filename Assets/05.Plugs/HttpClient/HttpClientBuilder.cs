using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

public enum HttpMethod
{
    Get,
    Post,
    Put
}

public enum DomainType
{
    Api,
    Pvp,
    Pve,
    TestPve,
    Socket,
    User,
    SDA,
    Chat,
    Guild,
    None
    // TestApi,
}

public class HttpClientBuilder
{
    public string baseUrl;
    public List<string> paths = new List<string>();
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    public Dictionary<string, object> headers = new Dictionary<string, object>();
    public string body;
    public List<IMultipartFormSection> multiPartBodyData = new List<IMultipartFormSection>();
    public byte[] uploadBytes;
    public HttpMethod httpMethod = HttpMethod.Get;
    public string contentType;

    public HttpClientBuilder(DomainType type = DomainType.Api)
    {
        switch (type)
        {
            case DomainType.Api:
                this.baseUrl = Reg.Domain;
                break;
            case DomainType.Pvp:
                this.baseUrl = Reg.PvpDomain;
                break;
            case DomainType.Pve:
                this.baseUrl = Reg.PveDomain;
                break;
            case DomainType.Socket:
                this.baseUrl = Reg.SocketDomain;
                break;
            case DomainType.User:
                this.baseUrl = Reg.UserDomain;
                break;
            case DomainType.SDA:
                this.baseUrl = Reg.SDA;
                break;
            case DomainType.Chat:
                this.baseUrl = Reg.Chat;
                break;
            case DomainType.Guild:
                this.baseUrl = Reg.Guild;
                break;
            // case DomainType.TestApi:
            //     this.baseUrl = Reg.TestDomain;
            //     break;
                
        }
    }

    public HttpClientBuilder(string baseUrl)
    {
        this.baseUrl = baseUrl;
    }

    /// <summary>
    /// Add a path or paths separating with '/', eg, path1/path2
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public HttpClientBuilder Path(string path)
    {
        this.paths.Add(path);
        return this;
    }

    /// <summary>
    /// Add parameter with key and value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public HttpClientBuilder Param(string key, object value)
    {
        this.parameters.Add(key, value);
        return this;
    }

    public HttpClientBuilder Body(string body)
    {
        this.body = body;
        return this;
    }

    public HttpClientBuilder Bytes(byte[] bytes)
    {
        this.uploadBytes = bytes;
        return this;
    }

    public HttpClientBuilder Method(HttpMethod method)
    {
        this.httpMethod = method;
        return this;
    }

    public HttpClientBuilder ContentType(string contentType)
    {
        this.contentType = contentType;
        return this;
    }

    public HttpClientBuilder Header(string field, object value)
    {
        this.headers.Add(field, value);
        return this;
    }

    /// <summary>
    /// Add one part of MultipartFormDataSection
    /// </summary>
    /// <param name="key"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public HttpClientBuilder MultiPartBody(IMultipartFormSection multipartFormSection)
    {
        this.multiPartBodyData.Add(multipartFormSection);
        return this;
    }

    /// <summary>
    /// Create a full URL.
    /// </summary>
    /// <returns>The URL with params</returns>
    public string Build(bool isIgnoreVid = false)
    {
        StringBuilder urlBuilder = new StringBuilder();
        urlBuilder.Append(baseUrl);

        // Paths
        if (paths.Count > 0)
        {
            foreach (var path in paths)
            {
                urlBuilder.Append("/").Append(path);
            }
        }

        // Params
        if (parameters.Count > 0)
        {
            urlBuilder.Append("?");
            int index = 0;
            foreach (KeyValuePair<string, object> kvp in parameters)
            {
                if (isIgnoreVid && kvp.Key.Equals("vid"))
                {
                    continue;
                }

                if (index > 0)
                {
                    urlBuilder.Append("&");
                }

                urlBuilder.Append(kvp.Key + "=" + kvp.Value.ToString());
                index++;
            }
        }

        return urlBuilder.ToString();
    }
}
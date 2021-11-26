using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankListRequest : BaseAPI
{
    public RankListRequest(GameObject gameObject) : base(gameObject)
    {
        ForceRequest = false;
    }

    public void Request(int type, int season, int page, bool isForceRequest)
    {
        var httpClientBuilder = CreateHttpClientBuilder(type, season, page, isForceRequest);
        //发送请求
        SendRequest(httpClientBuilder);
    }
    
    //创建请求链接
    private HttpClientBuilder CreateHttpClientBuilder(int type, int season, int page, bool isForceRequest)
    {
        ForceRequest = isForceRequest;
        //isEncode = false;
        
        //http://api-s2.artofwarconquest.com/admin/rankList
        //?type=1&page=1&season=18&token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI0MzY4NjY1MjcifQ.drFj2OtLEjgE452sgtHPG73xU-yQ-OXvbz4Utxl2M1k
        //DomainType.Pvp
        string path = "http://api-s2.artofwarconquest.com/admin/rankList";
         HttpClientBuilder httpClientBuilder = new HttpClientBuilder(path)
            //.Path(path)
            .Param("type", type)
            .Param("page", page)
            .Param("season", season)
            .Param("token","eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI0MzY4NjY1MjcifQ.drFj2OtLEjgE452sgtHPG73xU-yQ-OXvbz4Utxl2M1k")
            .Method(HttpMethod.Get);

        return httpClientBuilder;
    }
}

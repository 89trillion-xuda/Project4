using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using RankData;

namespace HttpRequest
{
    /// <summary>
    /// 调用请求的入口类，调用请求方法，传入请求参数，执行回调函数做其他的业务逻辑处理
    /// </summary>
    public class RankListApiManager : MonoBehaviour
    {

        public void RequestNewRankList(int type, int season, int page, bool isForceRequest)
        {
            RankListRequest api = new RankListRequest(gameObject);
            //定义请求成功的回调函数，得到Json数据
            api.OnSuccess = data => { OnRankApiSuccess(data);};
            api.OnError = (s, i) => {Debug.Log("message:" + s + "  num:" + i); };
            //调用请求函数执行请求
            api.Request( type, season, page, isForceRequest);
        }

        //解析Json数据
        public void OnRankApiSuccess(string data)
        {
            //调用解析Json数据的类中的方法
            GetRankssListModels getRankListModels = new GetRankssListModels();
            getRankListModels.GetJsonData(data);
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using SimpleJSON;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;


namespace RankData
{
    /// <summary>
    /// 数据准备：
    /// 通过SimpleJson解析json格式的数据，存储在List类型的 RankListModels 中
    /// </summary>
    public class GetRankssListModels
    {
        public static List<RankListModel> RankListModels = new List<RankListModel>();//存储得到的Json数据
        public static double countDown = 2048;//倒计时的秒数时间戳
        public static Action EventWriteDataSuccess;

        //通过SimpleJson解析，得到排名数据
        public void GetJsonData(string data)
        {
            //转为Json格式
            JSONNode jsonNode = JSON.Parse(data);
            //深入查找到数组内的数据
            JSONNode jsonNode1 = jsonNode[2];
            JSONNode jsonNode2 = jsonNode1[0];

            //循环遍历json数据，并映射到实体类DailyProduct中
            for (int  i = 0; i < jsonNode2.Count; i++)
            {
                //实例化一个商品对象，用于映射json数据
                RankListModel rankListModel = new RankListModel();
                rankListModel.Uid = jsonNode2[i]["uid"];
                rankListModel.NickName = jsonNode2[i]["nickName"];
                rankListModel.Avatar = jsonNode2[i]["avatar"];
                rankListModel.Trophy = jsonNode2[i]["trophy"];

                //通过数组存储数据
                RankListModels.Add(rankListModel);
            }
            //排序：排序规则在实体类 RankListModel 中的 CompareTo（）方法
            RankListModels.Sort();

            if (EventWriteDataSuccess!=null)
            {
                EventWriteDataSuccess();
            }
        
        }

    }
}
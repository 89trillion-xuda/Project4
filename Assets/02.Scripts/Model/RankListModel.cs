using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RankData
{
    /// <summary>
    /// 排名信息实体类
    /// </summary>
    public class RankListModel : IComparable<RankListModel>
    {
        public long Uid { get; set; }

        public string NickName { get; set; }

        public int Avatar { get; set; }

        public int Trophy { get; set; }

        public int CompareTo(RankListModel other)
        {
            //排序逻辑 ：this在后面，意味着指定按照这个属性降序
            return other.Trophy.CompareTo(this.Trophy);
            //this在前面，意味着指定按照这个属性生序
        }
    }

}
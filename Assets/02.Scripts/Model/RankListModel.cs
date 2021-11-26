using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 排名信息实体类
/// </summary>
public class RankListModel : IComparable<RankListModel>
{
    public long uid;
    public string nickName;
    public int avatar;
    public int trophy;

    public long Uid
    {
        get => uid;
        set => uid = value;
    }

    public string NickName
    {
        get => nickName;
        set => nickName = value;
    }

    public int Avatar
    {
        get => avatar;
        set => avatar = value;
    }

    public int Trophy
    {
        get => trophy;
        set => trophy = value;
    }

    //
    public int CompareTo(RankListModel other)
    {
        //排序逻辑 ：this在后面，意味着指定按照这个属性降序
        return other.trophy.CompareTo(this.trophy);
        //this在前面，意味着指定按照这个属性生序
        //return this.trophy.CompareTo(other.trophy);
    }
}

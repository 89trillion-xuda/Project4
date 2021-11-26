using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制视图的显示和隐藏，来实现视图跳转
/// </summary>
public class ViewSkipController : MonoBehaviour
{

    [SerializeField] private GameObject BeginObjectCLone;
    [SerializeField] private GameObject PopupsObjectClone;

    //进入排行榜方法
    public void RankEnter()
    {
        //隐藏开始界面
        BeginObjectCLone.SetActive(false);
    }

    //退出排行榜方法
    public void RankOut()
    {
        //隐藏弹窗
        PopupsObjectClone.SetActive(false);
        //显示开始界面
        BeginObjectCLone.SetActive(true);
    }
    
    //退出弹窗方法
    public void PopupsOut()
    {
        //隐藏弹窗
        PopupsObjectClone.SetActive(false);
    }
}

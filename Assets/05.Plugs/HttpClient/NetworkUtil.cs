using UnityEngine;

public class NetworkUtil
{
    /// <summary>
    /// 判断网络是否链接
    /// </summary>
    /// <returns></returns>
    public static bool IsInternetConnected()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    /// <summary>
    /// 展示断网Tip提示
    /// </summary>
    public static void ShowNetworkDisconnectTip()
    {
        Debug.LogError("connect_error_msg_new");
    }


    /// <summary>
    /// 统一处理网络错误码
    /// </summary>
    /// <param name="errorCode">错误代码</param>
    public static void ShowNetworkErrorTip(int errorCode)
    {
        // todo
        switch (errorCode)
        {
            case 301:


            case 302:


            case 303:


            default:
                break;
        }
    }
}
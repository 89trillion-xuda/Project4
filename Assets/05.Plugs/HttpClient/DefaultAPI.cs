using Random = UnityEngine.Random;
#if UNITY_EDITOR || UNITY_STANDALONE
using System;
using System.IO;
using UnityEngine;
using System.Net.NetworkInformation;

public class DefaultAPI : IPlatformAPI
{
    public static string DefaultTestDeviceId = string.Empty;

    public static string deviceIdKey = "DeviceIdKey";

    /// <summary>
    /// 获取测试DeviceId，如果没有则根据硬件信息生成。
    /// </summary>
    public static string TestDeviceId
    {
        get
        {
            if (string.IsNullOrEmpty(DefaultTestDeviceId))
            {
                var str = PlayerPrefs.GetString(deviceIdKey);
                if (string.IsNullOrEmpty(str))
                {
                    DefaultTestDeviceId = GenerateDeviceId();
                    PlayerPrefs.SetString(deviceIdKey, DefaultTestDeviceId);
                }
                else
                {
                    DefaultTestDeviceId = str;
                }
            }

            return DefaultTestDeviceId;
        }
    }

    public void Initialize()
    {
    }

    public string GetHttpHeader()
    {
        //FIXME
        return
            "eyJzdmMiOjI1LCJjdmMiOjM4ODAsImRldmljZSI6IlNNLU45NTAwIiwidWlkIjoiZmZmZmZmZmYtZWJiNS05MTVmLTAwMDAtMDAwMDAwMDAwMDAwIiwiY2hhbiI6IjEwMDAxIiwiYWRpZCI6IiIsImdhaWQiOiJjMWM4N2FiNS00NmQzLTRiNjgtOTI0MS1kMmQ4NzM3OTc0ODMiLCJuZXR3b3JrIjoid2lmaSIsImxhbmciOiJlbl9VUyJ9";
    }

    public string GetSocketHeader()
    {
//return "{\"gaid\":\"f77647a8-8e41-435d-a558-1304455f7b9\",\"cvc\":292,\"device\":\"SM-N9750\",\"simcode\":\"46001\",\"network\":\"wifi\",\"lang\":\"en_US\",\"platform\":\"android\",\"systemver\":29}";
//    522
        return "{\"gaid\":\"" + TestDeviceId +
               "\",\"cvc\":3880,\"device\":\"SM-N9750\",\"simcode\":\"46001\",\"network\":\"wifi\",\"lang\":\"en_US\",\"platform\":\"android\",\"systemver\":29}";
//       return "{\"gaid\":\"f77647a8-8e41-435d-a558-456463d52222\",\"cvc\":292,\"device\":\"SM-N9750\",\"simcode\":\"46001\",\"network\":\"wifi\",\"lang\":\"en_US\",\"platform\":\"android\",\"systemver\":29}";;
//       return "{\"gaid\":\"3c39c523-d1f4-4576-b285-10ca1ad1abe1\",\"cvc\":292,\"device\":\"SM-N9750\",\"simcode\":\"46001\",\"network\":\"wifi\",\"lang\":\"en_US\",\"platform\":\"android\",\"systemver\":29}";;
//       return "{\"gaid\":\"3c39c523-d1f4-4576-b285-10ca1ad12323\",\"cvc\":292,\"device\":\"SM-N9750\",\"simcode\":\"46001\",\"network\":\"wifi\",\"lang\":\"en_US\",\"platform\":\"android\",\"systemver\":29}";;
//        return
//            "{\"gaid\":\"f77647a8-8e41-435d-a558-412363d51101\",\"cvc\":3100,\"device\":\"SM-N9750\",\"simcode\":\"46001\",\"network\":\"wifi\",\"lang\":\"en_US\",\"platform\":\"android\",\"systemver\":29}";
    }

    public string GetAppId()
    {
        return "223";
    }

    public string GetDeviceId()
    {
//        return "{\"deviceId\":\"f77647a8-8e41-435d-a558-43392677b9\"}";//--->token:CjEEAnQBWHMxSUQ\/QTsxf3twA3V+AXwxSTFwfXMBfnhzMQw=
        return "{\"deviceId\":\"" + TestDeviceId + "\"}"; //--->token://522
//        return "{\"deviceId\":\"f77647a8-8e41-435d-a558-456463d52222\"}";//--->token:
//        return "{\"deviceId\":\"3c39c523-d1f4-4576-b285-10ca1ad1abe1\"}";//--->token:
//        return "{\"deviceId\":\"3c39c523-d1f4-4576-b285-10ca1ad12323\"}";//--->token:
//        return "{\"deviceId\":\"f77647a8-8e41-435d-a558-412363d51101\"}"; //--->token:
    }

    public string GetPushToken()
    {
        return "Token is false";
    }

    public string GetPushDeviceId()
    {
        return "DeviceId is false";
    }

    public string GetPushCountryZipCode()
    {
        return "Country is false";
    }

    public string GetReferKeyword()
    {
        //todo remove
        return null;
    }

    public string GetVersionName()
    {
        throw new NotImplementedException();
    }


    public int GetVersionCode()
    {
        return 3100;
    }

    public string GetLicenceId()
    {
        return Reg.LicenceId;
    }

    public void PerformHapticFeedback()
    {
    }

    public string GetSkuPrice(string sku)
    {
        return "";
    }

    public void SendGuildInvitationUrl(string url)
    {
    }

    public string GetStoredGuildInvitationMsg()
    {
        return "";
    }

    public void ResetGuildRetMsg()
    {
    }

    public void MakePurchase(string sku)
    {
        var delay = Random.Range(1f, 3.5f);

//        if (RandomUtil.GetBool(0.9f))
//        {

//        }
//        else
//        {
//            Timer.Schedule(delay,
//                () =>
//                {
//                    PaymentController.Instance.SendMessage("PaymentFailure", sku,
//                        SendMessageOptions.DontRequireReceiver);
//                }, PaymentController.Instance);
//        }
    }

    public bool IsAdFreeUser()
    {
        return false;
    }

    public string GetViewConfig(int viewId)
    {
        return null;
    }

    public void ContactUs()
    {
        Debug.Log("ContactUs");
    }

    public void Vibrate(int vibrateTime)
    {
    }

    public void RateUs()
    {
    }

    public bool IsAppInstalled(string appStoreId)
    {
        return false;
    }

    public bool OpenInMarket(string appStoreId)
    {
        return false;
    }

    public string GetLaunchUrlString()
    {
        return null;
    }

    public string GetConfigString(bool isForce)
    {
        return null;
    }


    public void ShowFAQ()
    {
    }

    public void PrintLog(string msg)
    {
        if (msg.Contains("=== socket ==="))
        {
            Debug.LogError(msg);
        }
        else
        {
            Debug.Log(msg);
        }
    }

    public string eud(string str)
    {
        try
        {
            var bytes = Convert.FromBase64String(str);
            return e392323d23a.eud(bytes);
        }
        catch (Exception e)
        {
        }

        return str;
    }

    public string eud(byte[] bytes)
    {
        return e392323d23a.eud(bytes);
    }

    public string eudd(string str)
    {
        return e392323d23a.eudd(str);
    }

    public void FollowSocialPlatform(string url)
    {
    }

    public void OpenVideoInYoutube(string url)
    {
        Application.OpenURL(url);
    }

    public void SetLanguage(string language)
    {
    }

    public void CopyToClipboard(string input)
    {
        TextEditor textEditor = new TextEditor();
        textEditor.content = new GUIContent(input);
        textEditor.OnFocus();
        textEditor.Copy();
    }

    public void ReportEvent(string category, object action, object label)
    {
        throw new NotImplementedException();
    }

    public void ReportRealTimeEvent(string category, object action, string label)
    {
        throw new NotImplementedException();
    }

    public void ReportBasicAnalytics(string type)
    {
        throw new NotImplementedException();
    }

    public bool IsUserBehaviorLogEnable()
    {
        throw new NotImplementedException();
    }

    public void ReportEventByFireBase(string eventName, string paramsJson)
    {
        throw new NotImplementedException();
    }

    public void RecordFbAppEvent(string name)
    {
        throw new NotImplementedException();
    }

    public void RecordFbAppEvent(string name, string valueToSumString, string paramsString)
    {
        throw new NotImplementedException();
    }

    public void RecordPurchaseSuccess(string valueToSumString, string paramsString)
    {
        throw new NotImplementedException();
    }

    public bool IsFucEnableByRemoteKey(string key)
    {
        throw new NotImplementedException();
    }

    public int GetValueByRemoteKey(string key, int defaultValue)
    {
        throw new NotImplementedException();
    }

    public string GetStringByRemoteKey(string key)
    {
        throw new NotImplementedException();
    }

    public void SaveUserId(string userId)
    {
        throw new NotImplementedException();
    }

    public void SaveUserInfo(string userId, string token)
    {
        throw new NotImplementedException();
    }


    #region VIP

    public void BuySubscription(string sku)
    {
    }

    public void RestorePurchases()
    {
    }

    public bool IsSubscriptionAvailable()
    {
        throw new NotImplementedException();
    }


    public bool IsNotificationOn()
    {
        throw new NotImplementedException();
    }

    public void SetIsNotificationOn(bool isOn)
    {
        throw new NotImplementedException();
    }

    public bool IsLaunchFromNotification()
    {
        throw new NotImplementedException();
    }

    public void RequestNotification()
    {
        throw new NotImplementedException();
    }

    public void RecordOfflineRewardsStatus(int remainCoins, int maxCoins, int nextFullTime)
    {
        throw new NotImplementedException();
    }

    public void RecordDailyTaskStatus(bool isTaskFinished, bool isHasTaskReward)
    {
        throw new NotImplementedException();
    }

    public void RecordChestStatus(bool isHasLockingChest, bool isChestCanOpen, int nextOpenTime)
    {
        throw new NotImplementedException();
    }

    public void RecordMaxPassedLevel(int level)
    {
        throw new NotImplementedException();
    }

    public void RecordPVPHistoryRecordLastShowTime(int lastShowTime)
    {
        throw new NotImplementedException();
    }

    public void RecordUserToken(string userToken)
    {
        throw new NotImplementedException();
    }

    public bool IsFromPVPAtkNotification()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Firebase Remote Test

    public int GetAdLimitTestType()
    {
        return 1;
    }

    public int GetAdLimitCount()
    {
        return 4;
    }

    public int GetAdLimitTime()
    {
        return 30;
    }

    public int GetIAPGuideTestType()
    {
        return 1;
    }

    public int GetPurchasedGuideTestType()
    {
        return 1;
    }

    #endregion

    #region Test

    public string GetTestConfig()
    {
        return "";
//        return "{\"defaultConfig\":{\"is_vip_icon_new\":\"0\",\"is_pvp_login_guide\":\"0\",\"is_cards_lock_new\":\"0\",\"is_shop_head_new\":\"0\",\"is_notification_new\":\"0\",\"is_4coin_card\":\"0\"},\"testConfig\":{\"is_vip_icon_new\":\"2\",\"is_4coin_card\":\"1\", \"is_guild_declare_war\":\"2\"}}";//FIXME
    }

    public string GetTestGroup()
    {
        return "A";
    }

    public void ForceLoadConfig()
    {
    }

    public string GetActConfig()
    {
        return "";
    }

    public void CheckIAPStatus()
    {
    }

    public int GetCheckIAPStatusTimes()
    {
        return 0;
    }

    #endregion

    public int GetUpdateState()
    {
#if UNITY_EDITOR
        return 0;
#endif

        return 0;
    }

    #region Addressable

    public void SetAddressableMsg(string msg)
    {
        Debug.Log($"SetAddressableMsg => {msg}");
    }

    public void SetAddressablePro(int progress)
    {
        Debug.Log($"SetAddressablePro => {progress}");
    }

    #endregion


    #region Splash Control

    public void HidePlatformSplash()
    {
        Debug.Log("########## HidePlatformSplash");
    }

    public void RestartShowSplash()
    {
        Debug.LogError($"RestartShowSplash");
    }

    public void EndPreLoadingAnim()
    {
    }

    #endregion

    #region IAP

    public void PurchaseSuccess(string sku, string token, string orderId)
    {
    }

    public void PurchaseFailure(string sku, string token)
    {
    }

    #endregion

    #region Game Center Login

    public void LoginGameCenter()
    {
    }

    #endregion


    public static string GetMacAddress()
    {
        string physicalAddress = "";
        NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adaper in nice)
        {
            if (adaper.Description == "en0")
            {
                physicalAddress = adaper.GetPhysicalAddress().ToString();
                break;
            }
            else
            {
                physicalAddress = adaper.GetPhysicalAddress().ToString();
                if (physicalAddress != "")
                {
                    break;
                }
            }
        }

        return physicalAddress;
    }

    public static string GenerateDeviceId()
    {
        var addr = GetMacAddress();
        var hashCode = addr.GetHashCode();
        var prefix = "";
        var id = "";
#if UNITY_EDITOR
        prefix = "unity000";
#elif UNITY_STANDALONE_OSX
        prefix = "macos000";
#endif
        string strCode = Math.Abs(hashCode).ToString();
        // 不足9位的时候补0到9位
        if (strCode.Length < 9)
        {
            for (int i = strCode.Length; i < 9; i++)
            {
                strCode = $"0{strCode}";
            }
        }

        id = string.Format("{0}-0000-0000-0000-000{1}", prefix, strCode);
        return id;
    }
}

#endif
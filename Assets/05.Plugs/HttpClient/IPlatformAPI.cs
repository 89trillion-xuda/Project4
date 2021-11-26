public interface IPlatformAPI
{
    void Initialize();

    string GetHttpHeader();
    
    string GetSocketHeader();
    
    string GetDeviceId();

    string GetAppId();

    string GetPushToken();
    
    string GetPushDeviceId();

    string GetPushCountryZipCode();

    string GetReferKeyword();

    string GetVersionName();

    int GetVersionCode();

    string GetLicenceId();

    void PerformHapticFeedback();

    string GetSkuPrice(string sku);

    void SendGuildInvitationUrl(string url);

    string GetStoredGuildInvitationMsg();

    void ResetGuildRetMsg();

    void MakePurchase(string sku);

    bool IsAdFreeUser();

    string GetViewConfig(int viewId);

    void ContactUs();

    void RateUs();
    
    bool IsAppInstalled(string appStoreId);

    bool OpenInMarket(string appStoreId);

    string GetLaunchUrlString();

    string GetConfigString(bool force);

    void PrintLog(string msg);

    void Vibrate(int vibrateTime);

    void ShowFAQ();
    string eud(string str);

    string eud(byte[] bytes);
    
    string eudd(string str);
    
    void FollowSocialPlatform(string url);
    void OpenVideoInYoutube(string url);
    void SetLanguage(string language);

    void CopyToClipboard(string input);
    #region Analytics

    void ReportEvent(string category, object action, object label);

    void ReportRealTimeEvent(string category, object action, string label);
    
    void ReportBasicAnalytics(string type);

    bool IsUserBehaviorLogEnable();

    void ReportEventByFireBase(string eventName, string paramsJson);

    void RecordFbAppEvent(string name);

    void RecordFbAppEvent(string name, string valueToSumString, string paramsString);

    void RecordPurchaseSuccess(string valueToSumString, string paramsString);

    bool IsFucEnableByRemoteKey(string key);

    int GetValueByRemoteKey(string key, int defaultValue);
    
    string GetStringByRemoteKey(string key);

    void SaveUserId(string userId);
    
    void SaveUserInfo(string userId, string token);

    #endregion

    #region VIP

    void BuySubscription(string sku);

    void RestorePurchases();

    bool IsSubscriptionAvailable();
    
    #endregion

    #region notification

    bool IsNotificationOn();

    void SetIsNotificationOn(bool isOn);

    bool IsLaunchFromNotification();

    void RequestNotification();

    void RecordOfflineRewardsStatus(int remainCoins, int maxCoins, int nextFullTime);
    
    void RecordDailyTaskStatus(bool isTaskFinished, bool isHasTaskReward);
    
    void RecordChestStatus(bool isHasLockingChest, bool isChestCanOpen, int nextOpenTime);
    
    void RecordMaxPassedLevel(int level);
    
    void RecordPVPHistoryRecordLastShowTime(int lastShowTime);

    void RecordUserToken(string userToken);
    
    bool IsFromPVPAtkNotification();

    #endregion

    #region Firebase Remote Test
    int GetAdLimitTestType();
    int GetAdLimitCount();
    int GetAdLimitTime();
    int GetIAPGuideTestType();
    int GetPurchasedGuideTestType();
    #endregion
    
    #region User Group Test

    string GetTestConfig();
    string GetTestGroup();
    void ForceLoadConfig();
    string GetActConfig();
    void CheckIAPStatus();
    int GetCheckIAPStatusTimes();

    #endregion

    int GetUpdateState();

    #region Addressable

    void SetAddressableMsg(string msg);

    void SetAddressablePro(int progress);

    #endregion


    #region Splash Control

    void HidePlatformSplash();

    void RestartShowSplash();

    void EndPreLoadingAnim();

    #endregion

    #region IAP

    void PurchaseSuccess(string sku, string token, string orderId);
    void PurchaseFailure(string sku, string token);

    #endregion

    #region Game Center Login
    void LoginGameCenter();

    #endregion
}
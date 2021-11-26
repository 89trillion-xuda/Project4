public static class Reg
{
    public static string Domain = OfficialDomain;
    public static string PvpDomain = OfficialPvpDomain;
    public static string PveDomain = OfficialPveDomain;
    public static string SocketDomain = OfficialSocketDomain;
    public static string UserDomain = OfficialUserDomain;
    public static string SDA = OfficialSDA;
    public static string Chat = OfficialChat;
    public static string Guild = OfficialGuild;


    public const string OfficialDomain = "https://api.artofwarios.com";
    public const string OfficialPvpDomain = "https://pvp.artofwarios.com";
    public const string OfficialPveDomain = "https://pve.artofwarios.com";
    public const string OfficialSocketDomain = "https://gate1.artofwarios.com";
    public const string OfficialUserDomain = "https://user.artofwarios.com";
    public const string OfficialSDA = "https://sda.artofwarios.com";
    public const string OfficialChat = "https://chat.artofwarios.com";
    public const string OfficialMail = "https://mail.artofwarios.com";

    public const string OfficialGuild = "https://team.artofwarios.com";

    // Licence id
    private const string licenceId = "ZGRpY3RpdmUuc3RyYXRlMmM3MzA4MjAxYWZhMDAz";
    public static string LicenceId => licenceId;


#if UNITY_EDITOR
    public static readonly IPlatformAPI PlatformAPI = new DefaultAPI();
#elif UNITY_STANDALONE
    public static readonly IPlatformAPI PlatformAPI = new DefaultAPI();
#elif UNITY_ANDROID
    public static readonly IPlatformAPI PlatformAPI = new AndroidAPI();
#else
    public static readonly IPlatformAPI PlatformAPI = new IosAPI();
#endif
}
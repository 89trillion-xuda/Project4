using System.Collections.Generic;
using System.IO;

public class ContentTypeUtil
{
    public static Dictionary<string, string> MimeTypes = new Dictionary<string, string>
    {
        {"jpe", "image/jpeg"},
        {"jpeg", "image/jpeg"},
        {"jpg", "image/jpeg"},
        {"png", "image/png"}
    };

    public static string GetFileContentType(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return "";
        }

        FileInfo fileInfo = new FileInfo(path);
        if (string.IsNullOrEmpty(fileInfo.Extension))
        {
            return "";
        }

        string extensionName = fileInfo.Extension.Split('.')[1];
        return MimeTypes[extensionName.ToLower()];
    }
}
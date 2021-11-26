using System;
using System.Text;

public class e392323d23a
{
    
    public static string eud(string s)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        byte[] res = e(bytes, -15);
        return Encoding.UTF8.GetString(res);
    }
    
    public static string eud(byte[]  bytes)
    {
        byte[] res = e(bytes, -15);
        return Encoding.UTF8.GetString(res);
    }

    public static string eudd(string s)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        byte[] res = e(bytes, 15);
        return Encoding.UTF8.GetString(res);
    }

    public static byte[] e(byte[] s, int f)
    {
        int l = s.Length;
        byte[] o = new byte[l];

        for (int i = 0; i < l; ++i)
        {
            byte c = s[i];
            int ci = (int) c;
            int result = ci + f;
            if (result < 0)
            {
                result += 128;
            }

            int moved = (result % 128);
            byte encoded_chr = (byte) moved;
            o[i] = encoded_chr;
        }

        return o;
    }
}
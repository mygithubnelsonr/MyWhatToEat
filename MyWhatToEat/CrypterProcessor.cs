using System;
using System.Text;

namespace MyWhatToEat
{
    public static class CrypterProcessor
    {
        const string hash = "cwBzAGgALgBzAHQAcgBhAHQAbwAuAGQAZQAsADIAMgAsAHcAdwB3AC4AcwB5AHMALQBzAGUAcgB2AGkAYwBlAC4AZABlACwAIQBiAGsANwBhADEAYgBiAGsANwBhADEAYgA/AA==";
        //const string connection = "ssh.strato.de,22,www.sys-service.de,!bk7a1bbk7a1b?";

        //public static string Encode(string plaintext)
        //{
        //    string encyptedText = Convert.ToBase64String(Encoding.Unicode.GetBytes(connection));

        //    return encyptedText;
        //}

        public static string Decode()
        {
            string retval = Encoding.Unicode.GetString(Convert.FromBase64String(hash));
            return retval;
        }
    }
}

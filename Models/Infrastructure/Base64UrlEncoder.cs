using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ProcsDLL.Models.Infrastructure
{
    public static class Base64UrlEncoder
    {
        public static string Encode(byte[] data)
        {
            var base64Raw = Convert.ToBase64String(data);
            var trimmed = base64Raw.TrimEnd('='); //trim equal padding chars
            return trimmed
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
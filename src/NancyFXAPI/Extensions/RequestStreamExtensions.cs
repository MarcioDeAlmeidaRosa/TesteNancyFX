using Nancy.IO;
using System.IO;
using System.Text;

namespace NancyFXAPI.Extensions
{
    public static class RequestStreamExtensions
    {
        public static string AsString(this RequestStream stream, Encoding encoding = null)
        {
            using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        }
        public static T GetJson<T>(this ISession session, string key)
        {
            byte[] arr = null;
            var sessionData = session.TryGetValue(key,out arr);
            
            var carts = JsonSerializer.Deserialize<T>(arr);
            return carts;
        }
    }
}

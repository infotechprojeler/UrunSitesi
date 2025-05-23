﻿using System.Text.Json;

namespace UrunSitesi.MVCWebUI.Extensions
{
    public static class SessionExtension
    {
        public static void SetJson(this ISession session, string key, object value) 
        {
            session.SetString(key, JsonSerializer.Serialize(value)); 
        }
        public static T? GetJson<T>(this ISession session, string key) where T : class 
        {
            var data = session.GetString(key);
            return data == null ?
                default : JsonSerializer.Deserialize<T>(data);
        }
    }
}

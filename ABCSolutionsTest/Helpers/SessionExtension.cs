using ABCSolutionsTest.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCSolutionsTest.Helpers
{
    public static class SessionExtension
    {


        public static bool IsAuthenticated(ISession session)
        {
            User user = GetCurrentUser(session);
            if (user == null)
                return false;
            return true;
        }

        public static User GetCurrentUser(ISession session)
        {
            return session.GetObjectFromJson<User>("CurrentUser");
        }

        public static void SetCurrentUser(ISession session, User newUser)
        {
            session.SetObjectAsJson("CurrentUser", newUser);
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

    }
}

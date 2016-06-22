using Newtonsoft.Json.Linq;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace j1Lib.VTC
{
    public class info : IDisposable
    {
        private string token;
        private static REST api = new REST("https://mobile.vtc.edu.hk");
        private class VTC_
        {
            public int errorCode { get; set; }
            public string errorMsg { get; set; }
            public bool isSuccess { get; set; }
            public string payload { get; set; }
        }
        public class VTC
        {
            public VTC(int errorCode, string errorMsg, bool isSuccess, dynamic payload)
            {
                this.errorCode = errorCode;
                this.errorMsg = errorMsg;
                this.isSuccess = isSuccess;
                this.payload = payload;
            }
            public int errorCode;
            public string errorMsg;
            public bool isSuccess;
            public dynamic payload;
        }
        private static VTC ParseVTC(RestSharp.IRestResponse data)
        {
            VTC_ temp = new JsonDeserializer().Deserialize<VTC_>(data);
            return new VTC(temp.errorCode, temp.errorMsg, temp.isSuccess, JObject.Parse(temp.payload));
        }

        private static void cmd(string cmd, Action<VTC> callback)
        {
            api.GET("api?cmd=" + cmd, null, (data) =>
              {
                  callback?.Invoke(ParseVTC(data));
              });
        }
        private static void cmd_timestamp(string cmd_, int timestamp, Action<VTC> callback)
        {
            cmd(cmd_ + "&timestamp=" + timestamp, callback);
        }
        private void cmd_token(string cmd_, Action<VTC> callback)
        {
            cmd(cmd_ + "&token=" + token, callback);
        }
        private static void cmd_token(string cmd_, string token, Action<VTC> callback)
        {
            cmd(cmd_ + "&token=" + token, callback);
        }
        private void cmd_token_timestamp(string cmd_, int timestamp, Action<VTC> callback)
        {
            cmd_timestamp(cmd_ + "&token=" + token, timestamp, callback);
        }
        private static void cmd_token_timestamp(string cmd_, string token, int timestamp, Action<VTC> callback)
        {
            cmd_timestamp(cmd_ + "&token=" + token, timestamp, callback);
        }
        private static void cmd_token(string cmd_, string token, int timestamp, Action<VTC> callback)
        {
            cmd_timestamp(cmd_ + "&token=" + token, timestamp, callback);
        }
        public info(Action<VTC> callback)
        {
            api.GET("api?cmd=createGuest", null, (data) =>
            {
                VTC temp = ParseVTC(data);
                token = temp.payload.token;
                callback?.Invoke(temp);
            });
        }
        public info(string vtcID, string password, Action<VTC> callback)
        {
            api.GET("api?cmd=loginMobile&vtcID=" + vtcID + "&password=" + password, null, (data) =>
            {
                VTC temp = ParseVTC(data);
                token = temp.payload.token;
                callback(temp);
            });
        }
        public void Dispose()
        {
            logout(null);
            GC.SuppressFinalize(this);
        }
        public static void checkAndroidVersion(Action<VTC> callback)
        {
            cmd("checkAndroidVersion", callback);
        }
        public static void checkIOSVersion(Action<VTC> callback)
        {
            cmd("checkIOSVersion", callback);
        }
        public void logout(Action<VTC> callback)
        {
            cmd("logoutMobile&token=" + token, callback);
        }
        public static void getAboutVTC(Action<VTC> callback)
        {
            cmd("getAboutVTC", callback);
        }
        public static void getAboutVTC(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getAboutVTC", timestamp, callback);
        }
        public static void getAppList(Action<VTC> callback)
        {
            cmd("getAppList", callback);
        }
        public static void getAppList(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getAppList", timestamp, callback);
        }
        public static void getWebsiteList(Action<VTC> callback)
        {
            cmd("getWebsiteList", callback);
        }
        public static void getWebsiteList(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getWebsiteList", timestamp, callback);
        }
        public static void getNotificationList(Action<VTC> callback)
        {
            cmd("getNotificationList", callback);
        }
        public static void getNotificationList(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getNotificationList", timestamp, callback);
        }
        public static void getRssList(Action<VTC> callback)
        {
            cmd("getRssList", callback);
        }
        public static void getRssList(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getRssList", timestamp, callback);
        }
        public static void getContactsList(Action<VTC> callback)
        {
            cmd("getContactsList", callback);
        }
        public static void getContactsList(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getContactsList", timestamp, callback);
        }
        public static void getNewsList(Action<VTC> callback)
        {
            cmd("getNewsList", callback);
        }
        public static void getNewsListt(int timestamp, Action<VTC> callback)
        {
            cmd_timestamp("getNewsList", timestamp, callback);
        }
        public void getUserInfo(Action<VTC> callback)
        {
            cmd_token("getUserInfo", callback);
        }
        public static void getUserInfo(string token, Action<VTC> callback)
        {
            cmd_token("getUserInfo", token, callback);
        }
        public void checkAccessToken(Action<VTC> callback)
        {
            cmd_token("checkAccessToken", callback);
        }
        public static void checkAccessToken(string token, Action<VTC> callback)
        {
            cmd_token("checkAccessToken", token, callback);
        }
        public void getTimeTableList(int year, int month, Action<VTC> callback)
        {
            cmd_token("getTimeTableList&year=" + year + "&month=" + month, callback);
        }
        public void getTimeTableList(int year, int month, int timestamp, Action<VTC> callback)
        {
            cmd_token_timestamp("getTimeTableList&year=" + year + "&month=" + month, timestamp, callback);
        }
        public static void getTimeTableList(string token, int year, int month, Action<VTC> callback)
        {
            cmd_token("getTimeTableList&year=" + year + "&month=" + month, token, callback);
        }
        public static void getTimeTableList(string token, int year, int month, int timestamp, Action<VTC> callback)
        {
            cmd_token_timestamp("getTimeTableList&year=" + year + "&month=" + month, token, timestamp, callback);
        }
    }
}

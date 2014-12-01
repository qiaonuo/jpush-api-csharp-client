﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.report;
using cn.jpush.api.common;
using cn.jpush.api.util;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
namespace JpushApiClientExample
{
    class JPushApiExample
    {
        public static String TITLE = "Test from API example";
        public static String ALERT = "Test from API Example - alert";
        public static String MSG_CONTENT = "Test from API Example - msgContent";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String TAG = "tag_api";
        public static String app_key = "997f28c1cea5a9f17d82079a";
        public static String master_secret = "47d264a3c02a6a5a4a256a45";

        static void Main(string[] args)
        {
            Console.WriteLine("*****开始发送******");
            JPushClient client = new JPushClient(app_key, master_secret);
            PushPayload payloadMessage = PushObject_All_All_Alert();
            try
            {
                var result = client.SendPush(payloadMessage);
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
                var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
            }
            catch (APIRequestException)
            {
            }
            Console.WriteLine("*****结束发送******");
        }
        public static PushPayload PushObject_All_All_Alert()
        {
            return PushPayload.AlertAll(ALERT);
        }
        public static PushPayload buildPushObject_all_alias_alert()
        {
            return new PushPayload(Platform.all(),
                                  Audience.s_alias("alias1"),
                                  new Notification().setAlert(ALERT),
                                  null,
                                  new Options());
        }
        public static PushPayload PushObject_All_All_Alias_Alert(string alert)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform =  Platform.all();

            pushPayload.audience = Audience.s_alias("alias1", "alias2");
            pushPayload.notification = new Notification().setAlert(alert);

            return pushPayload;
        }
        public static PushPayload PushObject_Android_Tag_AlertWithTitle()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.ios();
           
            pushPayload.audience = Audience.s_tag("tag1"); 
            pushPayload.notification =  Notification.android(ALERT,TITLE);

            return pushPayload;
        }
        public static PushPayload PushObject_android_and_ios()
        {
            PushPayload pushPayload = new PushPayload();

            pushPayload.platform = Platform.android_ios();
            var audience = Audience.s_tag("tag1");
            pushPayload.audience = audience;
           
            var notification = new Notification().setAlert("alert content");
            notification.AndroidNotification = new AndroidNotification().setTitle("Android Title");
            notification.IosNotification = new IosNotification();
            notification.IosNotification.incrBadge(1);
            notification.IosNotification.AddExtra("extra_key", "extra_value");

            pushPayload.notification = notification.Check(); 
      

            return pushPayload;
        }
        public static PushPayload buildPushObject_ios_tagAnd_alertWithExtrasAndMessage()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = Audience.s_tag("tag1", "tag2").alias("alias1", "alias2"); 
            var notification = new Notification();
            notification.IosNotification = new IosNotification();
            notification.IosNotification.setAlert("alert");
            notification.IosNotification.disableSound().setSound("");

            var winphone = new WinphoneNotification().setOpenPage("SettingPage.xaml").AddExtra("string","aaa");

            notification.WinphoneNotification = winphone;

            return pushPayload;

        }

    }
}

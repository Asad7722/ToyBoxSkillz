using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class NotificationsSetup : MonoBehaviour
{

    string orgNotificationListText;
    public Text pendingNotificationList =null;
    public static NotificationsSetup Instance { get; private set; }      
    //private NotificationRepeat repeatType = NotificationRepeat.EveryDay;
   


    void Start()
    {

        //bool RepeatCalled= Configuration.instance.RepeatCalled;
        //if (!RepeatCalled && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{
        //    if (Notifications.IsInitialized())
        //    {
        //        if(Configuration.instance.RepeatCall)
        //        {
        //            RepeatCall();
        //        }                    
        //        NotificationRetention1Call(3, "call1");
        //        NotificationRetention2Call(7, "call2");
        //        NotificationRetention3Call(14, "call3");
        //    }           

        //}
        //orgNotificationListText = pendingNotificationList.text;
        //InvokeRepeating("UpdatePendingNotificationList", 1, 1);
        //UpdatePendingNotificationList();
    }
    
    // Construct the content of a new notification for scheduling.   
    public void RepeatCall()
    {
        //if (!InitCheck())
        //    return;
        //Notifications.CancelPendingLocalNotification("RepeatCall");

        //if (Configuration.instance.RepeatCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)            
        //{

        //    NotificationContent notifDinner = new NotificationContent();

        //    // Provide the notification title.
        //    notifDinner.title = Configuration.instance.repeatTitle;

        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    //notifDinner.subtitle = Configuration.instance.repeatSubtitle;

        //    // Provide the notification message.
        //    notifDinner.body = Configuration.instance.repeatMessage;

        //    if(Configuration.instance.Amazon)
        //    {
        //        repeatType = NotificationRepeat.EveryDay;
        //    }
        //    else
        //    {
        //        try
        //        {
                   
        //           repeatType = NotificationRepeat.EveryDay;
                   
        //        }
        //        catch { }
                
        //    }
           

            // You can optionally attach custom user information to the notification
            // in form of a key-value dictionary.
        //    notifDinner.userInfo = new Dictionary<string, object>();
        //    notifDinner.userInfo.Add("string", "OK");
        //    notifDinner.userInfo.Add("number", 3);
        //    notifDinner.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notifDinner.categoryId = EM_NotificationsConstants.UserCategory_notification_category_RepeatCall;
        //    // Set the delivery time.  

        //    // Increase badge number (iOS only)
        //    notifDinner.badge = Notifications.GetAppIconBadgeNumber() + 1;

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //    notifDinner.smallIcon = "ic_stat_em_default";
        //    notifDinner.largeIcon = "ic_large_em_default";

        //    // return notif;

        //    TimeSpan currentTime = DateTime.Now.TimeOfDay;
        //    Debug.Log("currentTime:" + currentTime);

        //    if (currentTime.Hours < Configuration.instance.RepeatHour)
        //    {
        //        TimeSpan DinnerTime = new TimeSpan(Configuration.instance.RepeatHour - currentTime.Hours - 1, 60 - currentTime.Minutes - 1, 60 - currentTime.Seconds);
        //        Debug.Log("Repeat Call: " + DinnerTime);
        //        Notifications.ScheduleLocalNotification("RepeatCall", DinnerTime, notifDinner, repeatType);
        //        Configuration.instance.RepeatCalled = true;
        //    }
        //    else
        //    {

        //        TimeSpan DinnerTime = new TimeSpan(24 - currentTime.Hours + Configuration.instance.RepeatHour - 1, 60 - currentTime.Minutes - 1, 60 - currentTime.Seconds);
        //        Debug.Log("Repeat Call: " + DinnerTime);
        //        Notifications.ScheduleLocalNotification("RepeatCall",DinnerTime, notifDinner, repeatType);
        //        Configuration.instance.RepeatCalled = true;
        //    }
        //}
       

    }
    public static void NotificationLifeCall()
    {
        //Notifications.CancelPendingLocalNotification("LifeCall");

        //if (Configuration.instance.LifeCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{

        //    int DeltaLife = NewLife.instance.maxLives - NewLife.instance.lives;

        //    float timeLeft = Configuration.instance.lifeReplenishTime - (float)NewLife.instance.timerForLife;
        //    int hour = Mathf.FloorToInt((timeLeft * DeltaLife) / 3600);            
        //    int min = Mathf.FloorToInt((timeLeft * DeltaLife) / 60);
        //    int sec = Mathf.FloorToInt((timeLeft * DeltaLife) % 60);

            
           

        //    NotificationContent notifLife = new NotificationContent();

        //    // Provide the notification title.           
        //     notifLife.title = "Life is Full"; 
            

        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    //notifLife.subtitle = "Life is Full";

        //    // Provide the notification message.
        //    notifLife.body = "Come and Continue Play.";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notifLife.userInfo = new Dictionary<string, object>();
        //    notifLife.userInfo.Add("string", "OK");
        //    notifLife.userInfo.Add("number", 3);
        //    notifLife.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notifLife.categoryId = EM_NotificationsConstants.UserCategory_notification_category_LifeCall;

        //    // Increase badge number (iOS only)
        //    notifLife.badge = Notifications.GetAppIconBadgeNumber() + 1;


        //    TimeSpan DeltaLifeTime = new TimeSpan(hour, min, sec);

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //   notifLife.smallIcon = "ic_stat_em_default";
        //   notifLife.largeIcon = "ic_large_em_default";

        //    // return notif;

        //    Notifications.ScheduleLocalNotification("LifeCall",new TimeSpan(hour, min, sec), notifLife);
        //    Debug.Log("NotificationLifeCall: " + DeltaLifeTime);
        //}

    }
    public static void NotificationStarChallengeCall()
    {
        //Notifications.CancelPendingLocalNotification("StarCall");

        //if (Configuration.instance.StarChallengeCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{

        //    NotificationContent notifStar = new NotificationContent();

        //    // Provide the notification title.
            
        //     notifStar.title = "Toy Box Party Time";
           



        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    //notifStar.subtitle = "Star Challenge";

        //    // Provide the notification message.
        //    notifStar.body = "The Star Challenge is about to expire. Keep collecting the stars.";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notifStar.userInfo = new Dictionary<string, object>();
        //    notifStar.userInfo.Add("string", "OK");
        //    notifStar.userInfo.Add("number", 3);
        //    notifStar.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notifStar.categoryId = EM_NotificationsConstants.UserCategory_notification_category_StarCall;

        //    // Increase badge number (iOS only)
        //    notifStar.badge = Notifications.GetAppIconBadgeNumber() + 1;

        //    TimeSpan DeltaTime = new TimeSpan(1, 5, 0, 0);
        //  //For Test
        //  //  TimeSpan DeltaTime = new TimeSpan(0,0,5 ,0);

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //    notifStar.smallIcon = "ic_stat_em_default";
        //    notifStar.largeIcon = "ic_large_em_default";

        //    // return notif;

        //    Notifications.ScheduleLocalNotification("StarCall",DeltaTime, notifStar);
        //    Debug.Log("NotificationStarChallengeCall: " + DeltaTime);
        //}

    }
    public static void NotificationDailyGiftCall()
    {
        //Notifications.CancelPendingLocalNotification("DailyGiftCall");

        //if (Configuration.instance.DailyGiftCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{           

        //    NotificationContent notifDailyGift = new NotificationContent();

        //    // Provide the notification title.
           
        //     notifDailyGift.title = "Toy Box Party Time"; 
            
        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    notifDailyGift.subtitle = "Get the free gift now!";

        //    // Provide the notification message.
        //    notifDailyGift.body = "Daily Gift ready! Get the free gift now!";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notifDailyGift.userInfo = new Dictionary<string, object>();
        //    notifDailyGift.userInfo.Add("string", "OK");
        //    notifDailyGift.userInfo.Add("number", 3);
        //    notifDailyGift.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notifDailyGift.categoryId = EM_NotificationsConstants.UserCategory_notification_category_DailyGiftCall;

        //    // Increase badge number (iOS only)
        //    notifDailyGift.badge = Notifications.GetAppIconBadgeNumber() + 1;


        //    TimeSpan DeltaTime = new TimeSpan(0, 13, 0, 0);
        //  //For Test
        //   // TimeSpan DeltaTime = new TimeSpan(0, 0, 2, 0);

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //    //notifDailyGift.smallIcon = "YOUR_CUSTOM_SMALL_ICON";
        //    //notifDailyGift.largeIcon = "YOUR_CUSTOM_LARGE_ICON";

        //    // return notif;

        //    Notifications.ScheduleLocalNotification("DailyGiftCall",DeltaTime, notifDailyGift,NotificationRepeat.EveryDay);
        //    Debug.Log("RepeatLocalNotificationDailyGiftCall: " + DeltaTime);
        //}

    }

    public static void NotificationCall(float time , string id)
    {
        //Notifications.CancelPendingLocalNotification(id);

        //if (PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{

        //    NotificationContent notif = new NotificationContent();

        //    // Provide the notification title.
            
        //     notif.title = "Toy Box Party Time"; 
            
        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    notif.subtitle = "Free Gift Box Ready!";

        //    // Provide the notification message.
        //    notif.body = "Free Gift Box Ready! Get the free gift now!";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notif.userInfo = new Dictionary<string, object>();
        //    notif.userInfo.Add("string", "OK");
        //    notif.userInfo.Add("number", 3);
        //    notif.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notif.categoryId = EM_NotificationsConstants.UserCategory_notification_category_DailyGiftCall;

        //    // Increase badge number (iOS only)
        //    notif.badge = Notifications.GetAppIconBadgeNumber() + 1;          

        //    int day = Mathf.FloorToInt(time / 86400);
        //    int hour = Mathf.FloorToInt((time / 3600) % 24);
        //    int min = Mathf.FloorToInt((time / 60) % 60);
        //    int sec = Mathf.FloorToInt(time % 60);

        //    TimeSpan DeltaTime = new TimeSpan(day, hour, min, sec);            

        //    Notifications.ScheduleLocalNotification(id, DeltaTime, notif);
        //    Debug.Log(notif + ": " + day + ": " + DeltaTime);
        //}

    }

    public static void NotificationRetention1Call(int day, string id)
    {
        //Notifications.CancelPendingLocalNotification(id);

        //if (Configuration.instance.DailyGiftCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{

        //    NotificationContent notif = new NotificationContent();

        //    // Provide the notification title.
           
        //     notif.title = "Toy Box Party Time"; 
            
        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    notif.subtitle = "Get the free gift now!";

        //    // Provide the notification message.
        //    notif.body = "Daily Gift ready! Get the free gift now!";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notif.userInfo = new Dictionary<string, object>();
        //    notif.userInfo.Add("string", "OK");
        //    notif.userInfo.Add("number", 3);
        //    notif.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notif.categoryId = EM_NotificationsConstants.UserCategory_notification_category_DailyGiftCall;

        //    // Increase badge number (iOS only)
        //    notif.badge = Notifications.GetAppIconBadgeNumber() + 1;


        //    TimeSpan DeltaTime = new TimeSpan(day, 0, 0, 0);
        //    //For Test
        //    // TimeSpan DeltaTime = new TimeSpan(0, 0, 2, 0);

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //    //notifDailyGift.smallIcon = "YOUR_CUSTOM_SMALL_ICON";
        //    //notifDailyGift.largeIcon = "YOUR_CUSTOM_LARGE_ICON";

        //    // return notif;

        //    Notifications.ScheduleLocalNotification(id, DeltaTime, notif);
        //    Debug.Log(notif +": " + day + ": " + DeltaTime);
        //}

    }
    public static void NotificationRetention2Call(int day, string id)
    {
        //Notifications.CancelPendingLocalNotification(id);

        //if (Configuration.instance.DailyGiftCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{

        //    NotificationContent notif2 = new NotificationContent();

        //    // Provide the notification title.
           
        //     notif2.title = "Toy Box Party Time";
           
        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    notif2.subtitle = "Get the free gift now!";

        //    // Provide the notification message.
        //    notif2.body = "Daily Gift ready! Get the free gift now!";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notif2.userInfo = new Dictionary<string, object>();
        //    notif2.userInfo.Add("string", "OK");
        //    notif2.userInfo.Add("number", 3);
        //    notif2.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notif2.categoryId = EM_NotificationsConstants.UserCategory_notification_category_DailyGiftCall;

        //    // Increase badge number (iOS only)
        //    notif2.badge = Notifications.GetAppIconBadgeNumber() + 1;


        //    TimeSpan DeltaTime = new TimeSpan(day, 0, 0, 0);
        //    //For Test
        //    // TimeSpan DeltaTime = new TimeSpan(0, 0, 2, 0);

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //    //notifDailyGift.smallIcon = "YOUR_CUSTOM_SMALL_ICON";
        //    //notifDailyGift.largeIcon = "YOUR_CUSTOM_LARGE_ICON";

        //    // return notif;

        //    Notifications.ScheduleLocalNotification(id, DeltaTime, notif2);
        //    Debug.Log(notif2 + ": " + day + ": " + DeltaTime);
        //}

    }
    public static void NotificationRetention3Call(int day, string id)
    {
        //Notifications.CancelPendingLocalNotification(id);

        //if (Configuration.instance.DailyGiftCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //{

        //    NotificationContent notif3 = new NotificationContent();

        //    // Provide the notification title.
           
        //     notif3.title = "Toy Box Party Time"; 
           
        //    // You can optionally provide the notification subtitle, which is visible on iOS only.
        //    notif3.subtitle = "Get the free gift now!";

        //    // Provide the notification message.
        //    notif3.body = "Daily Gift ready! Get the free gift now!";

        //    // You can optionally attach custom user information to the notification
        //    // in form of a key-value dictionary.
        //    notif3.userInfo = new Dictionary<string, object>();
        //    notif3.userInfo.Add("string", "OK");
        //    notif3.userInfo.Add("number", 3);
        //    notif3.userInfo.Add("bool", true);

        //    // You can optionally assign this notification to a category using the category ID.
        //    // If you don't specify any category, the default one will be used.
        //    // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        //    // if it has been generated before. In this example, UserCategory_notification_category_test is the
        //    // generated constant of the category ID "notification.category.test".
        //    notif3.categoryId = EM_NotificationsConstants.UserCategory_notification_category_DailyGiftCall;

        //    // Increase badge number (iOS only)
        //    notif3.badge = Notifications.GetAppIconBadgeNumber() + 1;


        //    TimeSpan DeltaTime = new TimeSpan(day, 0, 0, 0);
        //    //For Test
        //    // TimeSpan DeltaTime = new TimeSpan(0, 0, 2, 0);

        //    // If you want to use default small icon and large icon (on Android),
        //    // don't set the smallIcon and largeIcon fields of the content.
        //    // If you want to use custom icons instead, simply specify their names here (without file extensions).
        //    //notifDailyGift.smallIcon = "YOUR_CUSTOM_SMALL_ICON";
        //    //notifDailyGift.largeIcon = "YOUR_CUSTOM_LARGE_ICON";

        //    // return notif;

        //    Notifications.ScheduleLocalNotification(id, DeltaTime, notif3);
        //    Debug.Log(notif3 + ": " + day + ": " + DeltaTime);
        //}

    }

    public void CancelAllPendingLocalNotifications()
    {
        if (!InitCheck())
            return;

        //Notifications.CancelAllPendingLocalNotifications();
        Debug.Log("Canceled all pending local notifications of this app.");
    }

    public void RemoveAllDeliveredNotifications()
    {
        //Notifications.ClearAllDeliveredNotifications();
        Debug.Log("Cleared all shown notifications of this app.");
    }

    bool InitCheck()
    {
       // bool isInit = Notifications.IsInitialized();
        return false;
    }
    public void UpdatePendingNotificationList()
    {
        //    Notifications.GetPendingLocalNotifications(pendingNotifs =>
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (var req in pendingNotifs)
        //        {
        //            NotificationContent content = req.content;

        //            sb.Append("ID: " + req.id.ToString() + "\n")
        //                .Append("Title: " + content.title + "\n")
        //                .Append("Subtitle: " + content.subtitle + "\n")
        //                .Append("Body: " + content.body + "\n")
        //                .Append("Badge: " + content.badge.ToString() + "\n")
        //                .Append("UserInfo: " + Json.Serialize(content.userInfo) + "\n")
        //                .Append("CategoryID: " + content.categoryId + "\n")
        //                .Append("NextTriggerDate: " + req.nextTriggerDate.ToShortDateString() + "\n")
        //                .Append("Repeat: " + req.repeat.ToString() + "\n")
        //                .Append("-------------------------\n");
        //        }

        //        var listText = sb.ToString();

        //        // Display list of pending notifications
        //        if (!pendingNotificationList.text.Equals(orgNotificationListText) || !string.IsNullOrEmpty(listText))            
        //            pendingNotificationList.text = sb.ToString();  
        //    });
    }

}



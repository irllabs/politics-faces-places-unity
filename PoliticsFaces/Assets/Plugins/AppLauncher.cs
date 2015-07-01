using UnityEngine;
#if UNITY_ANDROID && !UNITY_EDITOR
namespace AppLauncher {
	public class AndroidAppLauncher {
		public static void LaunchDialPad() {
			AndroidAppLauncher.LaunchDialPad("");
		}
		
		public static void LaunchDialPad(string number) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchDialPad", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) number
					});
			}
		}
		
		public static void LaunchSMS() {
			AndroidAppLauncher.LaunchSMS("", "");
		}
		
		public static void LaunchSMS(string number, string message) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchSMS", new object[3]
					                                                                                               {
						(object) androidJavaObject,
						(object) number,
						(object) message
					});
			}
		}
		
		public static void LaunchGallery() {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchGallery", new object[1]
					                                                                                               {
						(object) androidJavaObject
					});
			}
		}
		
		public static void LaunchSettings() {
			AndroidAppLauncher.LaunchSettings(AndroidAppLauncher.SettingMode.DEFAULT);
		}
		
		public static void LaunchSettings(AndroidAppLauncher.SettingMode mode) 	{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchSettings", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) mode
					});
			}
		}
		
		public static void LaunchGmail() {
			AndroidAppLauncher.LaunchGmail("", "", "");
		}
		
		public static void LaunchGmail(string sendTo, string subject, string message) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchGmail", new object[4]
					                                                                                               {
						(object) androidJavaObject,
						(object) sendTo,
						(object) subject,
						(object) message
					});
			}
		}
		
		public static void LaunchFB() {
			AndroidAppLauncher.LaunchFB(AndroidAppLauncher.FBMode.PROFILE, "");
		}
		
		public static void LaunchFB(AndroidAppLauncher.FBMode mode, string id) {
			switch (mode)
			{
			case AndroidAppLauncher.FBMode.FEED:
				Application.OpenURL("fb://feed/" + id);
				break;
			case AndroidAppLauncher.FBMode.PROFILE:
				Application.OpenURL("fb://profile/" + id);
				break;
			case AndroidAppLauncher.FBMode.PAGE:
				Application.OpenURL("fb://page/" + id);
				break;
			case AndroidAppLauncher.FBMode.PLACE:
				Application.OpenURL("fb://placefw?pid=" + id);
				break;
			case AndroidAppLauncher.FBMode.GROUP:
				Application.OpenURL("fb://groups");
				break;
			case AndroidAppLauncher.FBMode.SEARCH:
				Application.OpenURL("fb://search");
				break;
			case AndroidAppLauncher.FBMode.FRIENDS:
				Application.OpenURL("fb://friends");
				break;
			case AndroidAppLauncher.FBMode.PAGES:
				Application.OpenURL("fb://pages");
				break;
			case AndroidAppLauncher.FBMode.MESSAGING:
				Application.OpenURL("fb://messaging");
				break;
			case AndroidAppLauncher.FBMode.ONLINE:
				Application.OpenURL("fb://online");
				break;
			case AndroidAppLauncher.FBMode.REQUESTS:
				Application.OpenURL("fb://requests");
				break;
			case AndroidAppLauncher.FBMode.EVENTS:
				Application.OpenURL("fb://events");
				break;
			case AndroidAppLauncher.FBMode.PLACES:
				Application.OpenURL("fb://places");
				break;
			case AndroidAppLauncher.FBMode.BIRTHDAYS:
				Application.OpenURL("fb://birthdays");
				break;
			case AndroidAppLauncher.FBMode.NOTIFICATIONS:
				Application.OpenURL("fb://notifications");
				break;
			case AndroidAppLauncher.FBMode.ALBUMS:
				Application.OpenURL("fb://albums");
				break;
			}
		}
		
		public static void LaunchSkype() {
			AndroidAppLauncher.LaunchSkype("");
		}
		
		public static void LaunchSkype(string userName) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchSkype", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) userName
					});
			}
		}
		
		public static void LaunchPlayStore() {
			AndroidAppLauncher.LaunchPlayStore("");
		}
		
		public static void LaunchPlayStore(string appName) {
			Application.OpenURL("market://search?q=" + appName);
		}
		
		public static void LaunchPhonebook() {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchPhonebook", new object[1]
					                                                                                               {
						(object) androidJavaObject
					});
			}
		}
		
		public static void LaunchWhatsAppToChat(string number) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchWhatsAppToChat", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) number
					});
			}
		}
		
		public static void ShareWithWhatsApp(string text) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("ShareWithWhatsApp", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) text
					});
			}
		}
		
		public static void LaunchTwitter(string id) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchTwitter", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) id
					});
			}
		}
		
		public static void ShareWithTwitter(string text) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("ShareWithTwitter", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) text
					});
			}
		}
		
		public static void LaunchApp(string packageName) {
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic("LaunchApp", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) packageName
					});
			}
		}


		public static bool CanLaunchApp(string packageName)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
					return ((AndroidJavaObject) new AndroidJavaClass("de.innoactive.applauncher.AppLaunch")).CallStatic<bool>("CanLaunchApp", new object[2]
					                                                                                               {
						(object) androidJavaObject,
						(object) packageName
					});
			}
		}
	
		public enum FBMode
		{
			FEED,
			PROFILE,
			PAGE,
			PLACE,
			GROUP,
			SEARCH,
			FRIENDS,
			PAGES,
			MESSAGING,
			ONLINE,
			REQUESTS,
			EVENTS,
			PLACES,
			BIRTHDAYS,
			NOTIFICATIONS,
			ALBUMS,
		}
		
		public enum SettingMode
		{
			DEFAULT,
			SEARCH_SETTING,
			SECURITY_SETTING,
			SOUND_SETTING,
			ACCESSBILITY_SETTING,
			AIRPLANE_MODE_SETTING,
			DATE_SETTING,
			BLUETOOTH_SETTING,
			DISPLAY_SETTING,
			SYNC_SETTING,
			APPLICATION_SETTING,
		}
	}
}
#endif

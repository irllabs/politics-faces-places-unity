using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSHelper  {
	// Interface to native implementation
	[DllImport ("__Internal")]
	private static extern bool _CanOpenURL (string url);

	// Public interface for use inside C# / JS code 
	public static bool CanOpenURL(string url) {
		// Call plugin only when running on real device
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			return _CanOpenURL(url);
		else
			return false;
	}
}

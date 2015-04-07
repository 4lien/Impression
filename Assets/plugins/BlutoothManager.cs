using UnityEngine;
using System.Collections;

public static class BlutoothManager{
	
	#if UNITY_ANDROID

	public static AndroidJavaObject	_AndroidpluginObj = null;
	public static bool	isReady = false;
	// Use this for initialization/*
	public static void Awake () {

		AndroidJNI.AttachCurrentThread();
		_Init();
	}
	
	public static void Destroy()
	{
		if( _AndroidpluginObj != null )
			_AndroidpluginObj.Dispose();
	}

	// Plugins_Android_Init
	
	public static int _Init()
	{
		AndroidJavaClass _androdiJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		
		if( _androdiJC != null )
		{
			_AndroidpluginObj = _androdiJC.GetStatic<AndroidJavaObject>("currentActivity");
			_androdiJC.Dispose();
			if( _AndroidpluginObj == null )
			{
				return -1;
			}
			
			return 0;
		}
		
		return -1;
	}
	
	
	public static void getBTData() //CallIntValue
	{
		if( _AndroidpluginObj != null )
		{

			ForBluetoothState.getData = _AndroidpluginObj.Call<string>("bluetoothDataRead").Trim();
			/*
			if(!"".Equals(strReturnValue)){
				ForBluetoothState.getData = strReturnValue;

			}*/

		}else{

			//ForBluetoothState = "블루투스 연결이 불가능 합니다.";
		}
		

	}
	
	public static string bluetoothConnection(string address) //Req_CheckPluginMessage
	{

		if( _AndroidpluginObj == null ){
			
			AndroidJNI.AttachCurrentThread();
			_Init();
		}

		if( _AndroidpluginObj != null )
		{
			if(address.Equals(""))
				address = "98:D3:31:40:08:84";

			return _AndroidpluginObj.Call<string>("bluetoothConnection",address); //"98:D3:31:40:08:84"

		}else{

			return "연결이 불가능 합니다.";
		}
	}


	#endif // UNITY_ANDROID
}

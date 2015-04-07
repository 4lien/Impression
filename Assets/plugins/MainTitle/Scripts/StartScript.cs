using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScript : MonoBehaviour {

	public AudioClip myClip;

	static string bluetoothSuccessMsg = "블루 투스 연결 성공";
	string bluetoothInfoMsg;

	Text BTInfoText;
	Text DebugLog;
	Text AddressText;
	private Canvas BTCanvas;
	private Canvas ExitCanvas;
	private Canvas HelpCanvas;
	private Canvas OptionCanvas;
	private Canvas MainCanvas;
	private Canvas GradeCanvas;
	private Canvas EasyCanvas;
	private Canvas NormalCanvas;
	private Canvas HardCanvas;
	private Canvas StageCanvas;





	GameObject BTCanvasObj;
	GameObject ExitCanvasObj;
	GameObject HelpCanvasObj;
	GameObject OptionCanvasObj;
	GameObject CanvasObj;
	GameObject GradeCanvasObj;
	GameObject EasyCanvasObj;
	GameObject NormalCanvasObj;
	GameObject HardCanvasObj;
	GameObject StageCanvasObj;


	GameObject BTInfoObj;
	GameObject DebugLogObj;
	GameObject AddressObj;

	void Awake(){
		//bluetoothInfoMsg = BlutoothManager.bluetoothConnection("");
		BTInfoObj = GameObject.Find("BlueToothInfo");
		BTInfoText = BTInfoObj.GetComponent<Text>();

		DebugLogObj = GameObject.Find("Debug");
		DebugLog = DebugLogObj.GetComponent<Text>();

		AddressObj = GameObject.Find("Address");
		AddressText = AddressObj.GetComponent<Text>();
		
		BTCanvasObj = GameObject.Find("BluetoothCanvas");
		BTCanvas = BTCanvasObj.GetComponent<Canvas>();
		
		HelpCanvasObj = GameObject.Find("HelpCanvas");
		HelpCanvas = HelpCanvasObj.GetComponent<Canvas>();
		
		OptionCanvasObj = GameObject.Find("OptionCanvas");
		OptionCanvas = OptionCanvasObj.GetComponent<Canvas>();
		
		ExitCanvasObj = GameObject.Find("ExitCanvas");
		ExitCanvas = ExitCanvasObj.GetComponent<Canvas>();
		
		CanvasObj = GameObject.Find("Canvas");
		MainCanvas = CanvasObj.GetComponent<Canvas>();


		GradeCanvasObj = GameObject.Find("GradeCanvas");
		GradeCanvas = GradeCanvasObj.GetComponent<Canvas>();

		EasyCanvasObj = GameObject.Find("EasyCanvas");
		EasyCanvas = EasyCanvasObj.GetComponent<Canvas>();
		
		NormalCanvasObj = GameObject.Find("NormalCanvas");
		NormalCanvas = NormalCanvasObj.GetComponent<Canvas>();
		
		HardCanvasObj = GameObject.Find("HardCanvas");
		HardCanvas = HardCanvasObj.GetComponent<Canvas>();
		
		StageCanvasObj = GameObject.Find("StageCanvas");
		StageCanvas = StageCanvasObj.GetComponent<Canvas>();
		/*
		if(bluetoothSuccessMsg.Equals(bluetoothInfoMsg)){
			BlutoothManager.isReady = true;
			DebugLog.text = bluetoothInfoMsg;
			BTCanvas.enabled = false;
			
		}*/
		//btConn ();
		enableF();
	}/*
	void Update(){
		if (BlutoothManager.isReady) {
			BlutoothManager.getBTData ();
			DebugLog.text = ForBluetoothState.getData;
		}

	}*/
	public void btConn(){
		bluetoothInfoMsg = BlutoothManager.bluetoothConnection("");
		if(bluetoothSuccessMsg.Equals(bluetoothInfoMsg)){
			BlutoothManager.isReady = true;
			DebugLog.text += bluetoothInfoMsg;
			BTCanvas.enabled = false;
			
		}
	}
	public void startClickEvent(){
		enableF();


		if (!BlutoothManager.isReady) {
			bluetoothInfoMsg = BlutoothManager.bluetoothConnection("");
			AddressText.text = "00:18:6B:86:EA:C8과";
			DebugLog.text = "???";
			if (bluetoothSuccessMsg.Equals (bluetoothInfoMsg)) {
				BlutoothManager.isReady = true;
				DebugLog.text = "?";
				BTCanvas.enabled = false;
				
			} else {
				DebugLog.text = "??";
				BTCanvas.enabled = true;
				BTInfoText.text = "블루투스 정보 : " + bluetoothInfoMsg;
				
			}
		} else {
			DebugLog.text =  bluetoothInfoMsg;
		}

		if(BlutoothManager.isReady){
			MainCanvas.enabled = false;
			enableF();
			showGradeCanvas();
			//Application.LoadLevel(1);
		}
	}
	public void exitClickEvent(){
		
		enableF();
		ExitCanvas.enabled = true;
		
	}
	public void noClickEvent(){		
		enableF();		
	}
	public void yesClickEvent(){
		Application.Quit();
	}
	
	public void helpClickEvent(){
		
		enableF();
		HelpCanvas.enabled = true;
		
	}
	public void ClickUrl(){
		
		Application.OpenURL("https://4lien.co.kr/");
	}
	public void easyClick(){
		
		//Application.OpenURL("https://easyClick.co.kr/");
		OptionManager.level = 1;
		showStageCanvas();
	}
	public void normalClick(){
		
		//Application.OpenURL("https://normalClick.co.kr/");
		OptionManager.level = 2;
		showStageCanvas();
	}
	public void hardClick(){
		
		//Application.OpenURL("https://hardClick.co.kr/");
		OptionManager.level = 3;
		showStageCanvas();
	}
	
	public void stage1Click(){
		Application.LoadLevel(1);
	}
	public void stage2Click(){
		Application.LoadLevel(1);
	}
	public void stage3Click(){
		Application.LoadLevel(1);
	}

	public void optionClickEvent(){

		enableF();
		OptionCanvas.enabled = true;
		
	}
	public void soundSlide(float value){

		OptionManager.sound = AudioListener.volume = value;
		DebugLog.text = OptionManager.sound.ToString();
		audio.Play();
		
	}
	public void enableF(){
		
		if(ExitCanvas.enabled)
			ExitCanvas.enabled = false;
		if(BTCanvas.enabled)
			BTCanvas.enabled = false;
		if(HelpCanvas.enabled)
			HelpCanvas.enabled = false;
		if(OptionCanvas.enabled)
			OptionCanvas.enabled = false;
		
	}
	
	public void showMain(){
		
		if(GradeCanvas.enabled)
			GradeCanvas.enabled = false;
		if(EasyCanvas.enabled)
			EasyCanvas.enabled = false;
		if(NormalCanvas.enabled)
			NormalCanvas.enabled = false;
		if(HardCanvas.enabled)
			HardCanvas.enabled = false;
		if(StageCanvas.enabled)
			StageCanvas.enabled = false;
		if(!MainCanvas.enabled)
			MainCanvas.enabled = true;
		
	}
	public void showGradeCanvas(){
		
		
		
		if(!GradeCanvas.enabled)
			GradeCanvas.enabled = true;
		if(!EasyCanvas.enabled)
			EasyCanvas.enabled = true;
		if(!NormalCanvas.enabled)
			NormalCanvas.enabled = true;
		if(!HardCanvas.enabled)
			HardCanvas.enabled = true;
		if(MainCanvas.enabled)
			MainCanvas.enabled = false;
		if(StageCanvas.enabled)
			StageCanvas.enabled = false;
		
	}
	public void showStageCanvas(){
		
		
		
		if(GradeCanvas.enabled)
			GradeCanvas.enabled = false;
		if(EasyCanvas.enabled)
			EasyCanvas.enabled = false;
		if(NormalCanvas.enabled)
			NormalCanvas.enabled = false;
		if(HardCanvas.enabled)
			HardCanvas.enabled = false;
		if(MainCanvas.enabled)
			MainCanvas.enabled = false;
		if(!StageCanvas.enabled)
			StageCanvas.enabled = true;
		
	}

}

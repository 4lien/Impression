using UnityEngine; 
using System.Collections; 
using UnityEngine.UI;
public class Behavior_Cam_Tilt : MonoBehaviour { 
	private Quaternion cameraBase = Quaternion.Euler(new Vector3(90f, 90f, 90f));
	private Quaternion referanceRotation = Quaternion.identity; 
	public const float lowPassFilterFactor = 0.8f; //가속도계 노이즈 필터 1~0
	public float maxLookDown=80f;
	public float maxLookUp=80f;
	GameObject player;
	GameObject camPos;
	Vector3 thisLocPos;

	void Start() {
		thisLocPos = transform.localPosition;
		Screen.sleepTimeout=SleepTimeout.NeverSleep;//화면 꺼짐 방지
		Input.gyro.enabled = true; 
		//camera.nearClipPlane = 0.0001f;
		player = GameObject.Find ("First Person Controller");
		camPos = GameObject.Find ("CamPosition");
	} 
	void Update() {


		/* 부모객체 바라보는 좌우 y축 방향 일치 */
		Vector3 v = player.transform.eulerAngles;
		Vector3 v2 = transform.eulerAngles;
		player.transform.eulerAngles = new Vector3 (v.x,v2.y,v.z);
		transform.eulerAngles = v2;


		Quaternion fromRotation = transform.rotation;

		//Debug.Log (Input.gyro.attitude);
		Quaternion toRotation = cameraBase * (ConvertRotation (referanceRotation * Input.gyro.attitude));
		Vector3 degree = toRotation.eulerAngles;
		/*
		if (degree.x>0&&degree.x<=90&&degree.x >maxLookDown) {//최대 내려보는 각도보다 낮으면
			toRotation.eulerAngles = new Vector3 (maxLookDown,degree.y,degree.z);
		}
		if (degree.x<360&&degree.x>=270&&degree.x < 360-maxLookUp) {//최대 내려보는 각도보다 높으면
			toRotation.eulerAngles = new Vector3 (360-maxLookUp, degree.y, degree.z);
		}
		*/
		/*GameObject canvas = GameObject.Find("Canvas");
		Text[] text = canvas.GetComponentsInChildren<Text>();
		text[4].text = toRotation.eulerAngles.ToString("G4");
		*/

		Quaternion slerp = Quaternion.Slerp (fromRotation, toRotation, lowPassFilterFactor);
		transform.rotation = slerp;
		transform.localPosition = new Vector3 (transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
		Vector3 camPosv = camPos.transform.position;
		transform.position = camPosv;

	} 
	
	private static Quaternion ConvertRotation(Quaternion q) {
		//아무 처리 예정 없음
		return new Quaternion(q.x, q.y, -q.z, -q.w); 
	} 
	private Quaternion GetRotFix() { 
		if (Screen.orientation == ScreenOrientation.Portrait)
			return Quaternion.identity; 
		if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape)
			return Quaternion.Euler(0, 0, -90); 
		if (Screen.orientation == ScreenOrientation.LandscapeRight)
			return Quaternion.Euler(0, 0, 90); 
		if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
			return Quaternion.Euler(0, 0, 180);
		return Quaternion.identity; 
	} 
}
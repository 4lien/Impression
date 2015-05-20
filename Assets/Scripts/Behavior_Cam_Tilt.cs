using UnityEngine; 
using System.Collections; 
using UnityEngine.UI;
public class Behavior_Cam_Tilt : MonoBehaviour { 
	public Quaternion cameraBase = Quaternion.Euler(new Vector3(90f, 90f, 90f));
	private Quaternion referanceRotation = Quaternion.identity; 
	public const float lowPassFilterFactor = 0.8f; //가속도계 노이즈 필터 1~0
	public GameObject camPos;
	Vector3 thisLocPos;

	void Start() {
		thisLocPos = transform.localPosition;
		Screen.sleepTimeout=SleepTimeout.NeverSleep;//화면 꺼짐 방지
		Input.gyro.enabled = true; 
		//camera.nearClipPlane = 0.0001f;
	} 
	void Update() {
		/* 부모객체 바라보는 좌우 y축 방향 일치 */
		Vector3 v = transform.parent.eulerAngles;
		Vector3 v2 = transform.eulerAngles;
		transform.parent.eulerAngles = new Vector3 (v.x,v2.y,v.z);
		transform.eulerAngles = v2;


		Quaternion fromRotation = transform.rotation;

		Quaternion toRotation = cameraBase * (ConvertRotation (referanceRotation * Input.gyro.attitude));
		Vector3 degree = toRotation.eulerAngles;

		Quaternion slerp = Quaternion.Slerp (fromRotation, toRotation, lowPassFilterFactor);
		transform.rotation = slerp;

		/* 카메라 위치 지정 */
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
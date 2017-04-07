using UnityEngine;
using System.Collections;

public class IconAutoTrim : MonoBehaviour {
	public Transform canvas;
	public float distance=20;
	public float screenScale=0.05f;
	private Camera theCamera;
	private Transform tx;
	private Vector2 canvasSize;
	void Awake()
	{
		theCamera = Camera.main;
		tx = theCamera.transform;
	}
	void Start()
	{
		canvas.GetComponent<RectTransform> ().localPosition = new Vector3 (0,0,distance);
		canvasSize = canvas.GetComponent<RectTransform> ().sizeDelta;
		Vector3[] corners = GetCorners (distance);
		float width = Vector3.Distance(corners [1],corners [0]);
		float sizeWidth = width * screenScale;
		canvas.GetComponent<RectTransform> ().localScale = new Vector3 (sizeWidth/canvasSize.x,sizeWidth/canvasSize.x,0);
	}
	void Update()
	{
		//			Debug.DrawLine (corners[0],corners[1],Color.red); //UpperLeft->UpperRight
		//			Debug.DrawLine(corners[1],corners[3],Color.red); //UpperRight->LowerRight
		//			Debug.DrawLine(corners[3],corners[2],Color.red); //UpperLeft->LowerLeft
		//			Debug.DrawLine(corners[2],corners[0],Color.red); //LowerLeft->LowerRight
//		RaycastHit hit;
//		if (Physics.Raycast (theCamera.transform.position,canvas.position, out hit)) {
//			distance = Vector3.Distance(hit.point,tx.position);
//			print (distance);
//			Vector3[] corners = GetCorners (distance);
//			float width = Vector3.Distance(corners [1],corners [0]);
//			float sizeWidth = width * screenScale;
//			canvas.GetComponent<RectTransform> ().localPosition = new Vector3 (0,0,distance);
//			canvas.GetComponent<RectTransform> ().localScale = new Vector3 (sizeWidth / canvasSize.x, sizeWidth / canvasSize.x, 0);
//		}
	}
	Vector3[] GetCorners(float distance)
	{
		Vector3[] corners = new Vector3[4];

		float halfFOV = (theCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
		float aspect = theCamera.aspect;

		float height = distance * Mathf.Tan (halfFOV);
		float width = height * aspect;
		//UpperLeft
		corners [0] = tx.position - (tx.right * width);
		corners [0] += tx.up * height;
		corners [0] += tx.forward * distance;
		//UpperRight
		corners [1] = tx.position + (tx.right) * width;
		corners [1] += tx.up * height;
		corners [1] += tx.forward * distance;
		//LowerLeft
		corners [2] = tx.position - (tx.right * width);
		corners [2] -= tx.up * height;
		corners [2] += tx.forward * distance;
		//LowerRight
		corners [3] = tx.position + (tx.right * width);
		corners [3] -= tx.up * height;
		corners [3] += tx.forward * distance;
		return corners;
	}
}

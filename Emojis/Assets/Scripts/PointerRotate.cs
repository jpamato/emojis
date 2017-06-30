using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerRotate : MonoBehaviour {

	Canvas canvas;

	public Vector2 pos0,pos1;
	public float rotateZ;
	float rotateOffset;
	public float rotateSpeed;

	float portraitRot;

	bool first;

	bool isRotate;

	Transform content;

	void Start()
	{
		canvas = GetComponentInParent<Canvas> ();

		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerDown;
		entry1.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry1);

		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerUp;
		entry2.callback.AddListener((data) => { OnPointerUpDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry2);

		content = transform.parent.Find ("content");
		if (Game.Instance.portrait)
			portraitRot = 90;
	}

	void Update()
	{
		if (isRotate) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
			pos1 = canvas.transform.TransformPoint(pos);
			if (Game.Instance.portrait) {
				rotateZ = rotateOffset + pos1.x - pos0.x;
			} else {
				rotateZ = rotateOffset + pos0.y - pos1.y;
			}
			content.transform.eulerAngles = new Vector3(0f,0f,portraitRot+rotateZ*rotateSpeed);
		}
	}

	public void OnPointerDownDelegate(PointerEventData data)
	{
		if (!isRotate) {
			//if(!first){
				Vector2 pos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
				pos0 = canvas.transform.TransformPoint(pos);
				first = true;
			//}	
			isRotate = true;
		}
	}

	public void OnPointerUpDelegate(PointerEventData data)
	{
		if (isRotate) {
			rotateOffset = rotateZ;
			isRotate = false;
		}
	}

	public void ResetOrigin(){
		//pos0 = transform.position;
	}
}

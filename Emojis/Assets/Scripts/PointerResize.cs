using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerResize : MonoBehaviour {
	Canvas canvas;

	public Vector2 pos0,pos1;
	public Vector3 scale;
	bool isScale;

	bool first;

	Vector3 lastMousePos;

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

		scale = Vector3.one;
		lastMousePos = Vector3.zero;
	}

	void Update()
	{
		if (isScale) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, lastMousePos+Input.mousePosition, canvas.worldCamera, out pos);
			pos1 = canvas.transform.TransformPoint(pos);
			pos1 += Vector2.one*10f;

			if(Game.Instance.portrait)
				scale = new Vector3 (Mathf.Clamp((pos1.y*pos1.x)/(pos0.y*pos0.x),0.1f,3f),Mathf.Clamp((pos1.x*pos1.y)/(pos0.x*pos0.y),0.1f,3f),0f);
			else
				scale = new Vector3 (pos1.x/pos0.x,pos0.y/pos1.y,0f);

			transform.parent.transform.localScale = scale;
		}
	}

	public void OnPointerDownDelegate(PointerEventData data)
	{
		if (!isScale) {	
			//if(!first){
				Vector2 pos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
				pos0 = canvas.transform.TransformPoint(pos);
				pos0 += Vector2.one*10f;
				pos0 = new Vector2 (pos0.x/scale.x, pos0.y/scale.y);
				first = true;
			//}
			isScale = true;		
		}
	}

	public void OnPointerUpDelegate(PointerEventData data)
	{
		if (isScale) {			
			isScale = false;
			//lastMousePos = Input.mousePosition;
		}
	}

	public void ResetOrigin(){
		pos0 = transform.position;
	}
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {
	
	Canvas canvas;

	bool moving;

	public GameObject rotate, resize, del;
	Image image;

	void Start()
	{
		canvas = GetComponentInParent<Canvas> ();
		image = GetComponent<Image> ();

		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerDown;
		entry1.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry1);

		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerUp;
		entry2.callback.AddListener((data) => { OnPointerUpDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry2);

		Events.ResetFrame += ResetFrame;
		Events.DeleteFrame += DeleteFrame;
	}

	void OnDestroy(){
		Events.ResetFrame -= ResetFrame;
		Events.DeleteFrame -= DeleteFrame;
	}

	void Update()
	{
		if (moving) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
			transform.position = canvas.transform.TransformPoint(pos);
		}
	}

	public void OnPointerDownDelegate(PointerEventData data)
	{
		if (!moving) {
			moving = true;
			Events.ResetFrame (gameObject.name);
		}
	}

	public void OnPointerUpDelegate(PointerEventData data)
	{
		if (moving)
			moving = false;
	}

	void ResetFrame(string n){
		bool enable = gameObject.name == n;
		rotate.SetActive (enable);
		resize.SetActive (enable);
		del.SetActive (enable);
		float alpha = enable ? 1f : 0f;
		image.color = new Color (1f,1f,1f,alpha);
	}

	void DeleteFrame(){
		Destroy (gameObject);
	}
}

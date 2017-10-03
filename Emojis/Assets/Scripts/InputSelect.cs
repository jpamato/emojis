using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSelect : MonoBehaviour {
	public int id;

	void Start(){
		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerDown;
		entry1.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry1);
	}

	public void OnPointerDownDelegate(PointerEventData data){
		Events.SelectField (id);
	}

}

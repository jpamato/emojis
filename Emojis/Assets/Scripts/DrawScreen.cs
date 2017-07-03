using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawScreen : MonoBehaviour {

	public GameObject menu;

	public Color[] colors;

	public GameObject swipePrefab;

	public Slider slider;

	public RectTransform panel;
	public GameObject showBtn;
	public GameObject hideBtn;
	public bool panelShow;

	GameObject swipe;

	TrailRenderer tr;

	bool drawing;
	int lastColor=1;

	float yLimit = 400f;
	public float xLimit = -1500;
	float limit;

	public List<GameObject> swipeList;

	// Use this for initialization
	void Start () {	

		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerDown;
		entry1.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry1);

		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerUp;
		entry2.callback.AddListener((data) => { OnPointerUpDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry2);
		Events.DrawPanelShow += DrawPanelShow;

		panelShow = true;

		xLimit *= Screen.currentResolution.width / Game.Instance.defaultResolution.x;
		limit = Game.Instance.portrait?xLimit:yLimit;
	}

	public void DrawPanelShow(bool enable){
		if(Game.Instance.portrait)
			limit = enable ? xLimit : 0;
		else
			limit = enable ? yLimit : 0;
	}

	void OnDestroy(){
		Events.DrawPanelShow -= DrawPanelShow;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDownDelegate(PointerEventData data)
	{
		//Debug.Log ("down");
		float posL = Game.Instance.portrait?Input.mousePosition.x*-1f:Input.mousePosition.y;
		if (posL > limit || limit==0f) {
			swipe = Instantiate (swipePrefab);
			//swipe.transform.parent = transform;
			tr = swipe.GetComponent<TrailRenderer> ();
			Invoke ("SetSwipe", 1f);
			tr.material.color = colors [lastColor];
			tr.startWidth = slider.value;
			tr.endWidth = slider.value;
			tr.sortingLayerName = "back";
			tr.sortingOrder = -100;
			swipeList.Add (swipe);
		}

	}

	public void OnPointerUpDelegate(PointerEventData data){
		//Debug.Log ("up");
		if (swipe != null)
			swipe.GetComponent<SwipeTrail> ().enabled = false;
	}

	void SetSwipe(){
		swipe.GetComponent<SwipeTrail> ().panelShow = panelShow;
	}

	public void PickColor(int i){	
		if (tr != null)
			tr.material.color = colors [i];
		lastColor = i;
	}

	public void SetWidth(){	
		if (tr != null) {	
			tr.startWidth = slider.value;
			tr.endWidth = slider.value;
		}
	}

	public void ShowPanel(bool enable){
		Vector3 pos = panel.localPosition;
		float y = enable ? 0 : -300;
		panel.localPosition = new Vector3 (pos.x, y, pos.z);
		hideBtn.SetActive (enable);
		showBtn.SetActive (!enable);
		panelShow = enable;
		Events.DrawPanelShow (enable);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour {

	float yLimit = 400f;
	float xLimit = -1500;
	public float limit;

	public bool panelShow;

	// Use this for initialization
	void Start () {
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
		panelShow = enable;
	}

	void OnDestroy(){
		Events.DrawPanelShow -= DrawPanelShow;
	}
		
	
	// Update is called once per frame
	void Update () {

		if (((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) || Input.GetMouseButton (0))) {
			Plane objPlane = new Plane (Camera.main.transform.forward * -1, this.transform.position);

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float posL = Game.Instance.portrait ? Input.mousePosition.x*-1f :  Input.mousePosition.y;
			if (panelShow) {
				if (posL > limit || limit ==0f) {
					float rayDistance;
					if (objPlane.Raycast (ray, out rayDistance))
						this.transform.position = ray.GetPoint (rayDistance);
				}
			} else {
				float rayDistance;
				if (objPlane.Raycast (ray, out rayDistance))
					this.transform.position = ray.GetPoint (rayDistance);
			}
		}
		
	}
}

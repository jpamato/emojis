using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour {
	//## VERSION TRANSPARENTE
	//public float yLimit0 = 400f;
	//public float xLimit0 = -1500;

	//## VERSION PERSONAL
	public float yLimit0 = 84;
	public float yLimit1 = 1019;
	public float xLimit0 = 365;
	public float xLimit1 = 1307;

	public float limit;

	public bool panelShow;

	// Use this for initialization
	void Start () {
		Events.DrawPanelShow += DrawPanelShow;
		panelShow = Game.Instance.portrait?true:false;
		xLimit0 *= Screen.currentResolution.width / Game.Instance.defaultResolution.x;
		xLimit1 *= Screen.currentResolution.width / Game.Instance.defaultResolution.x;
		yLimit0 *= Screen.currentResolution.height / Game.Instance.defaultResolution.y;
		yLimit1 *= Screen.currentResolution.height / Game.Instance.defaultResolution.y;
		limit = Game.Instance.portrait?Game.Instance.xLimit0:Game.Instance.yLimit0;
	}

	public void DrawPanelShow(bool enable){
		if(Game.Instance.portrait)
			limit = enable ? Game.Instance.xLimit0 : 0;
		else
			limit = enable ? Game.Instance.yLimit0 : 0;
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
			//## VERSION TRANSPARENTE
			bool test=false;
			if(Game.Instance.portrait ){
				float posL = Game.Instance.portrait ? Input.mousePosition.x*-1f :  Input.mousePosition.y;			
				if (posL > limit || limit == 0f)
					test = true;

				// ##VERSION PERSONAL
			}else if (Input.mousePosition.x > Game.Instance.xLimit0 &&
			    Input.mousePosition.x < Game.Instance.xLimit1 &&
			    Input.mousePosition.y > Game.Instance.yLimit0 &&
			    Input.mousePosition.y < Game.Instance.yLimit1)
				test = true;
			
			if (panelShow) {
				if (test){									
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

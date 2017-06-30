using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResize : MonoBehaviour {


	void Start () {

		if (Game.Instance.portrait)
			transform.eulerAngles = new Vector3 (0, 0, 90f);
		else
			transform.eulerAngles = new Vector3 (0, 0, 0f);

		Vector3 scale = new Vector3 (Screen.currentResolution.width / Game.Instance.defaultResolution.x,
			                Screen.currentResolution.height / Game.Instance.defaultResolution.y, 0f);
		RectTransform rt = GetComponent<RectTransform> ();
		rt.localScale = scale;
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}

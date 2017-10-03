using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScreen : MonoBehaviour {

	public GameObject menu;

	public Color[] colors;
	public Text text;
	public Text placeholder;

	public InputField iText;

	public RectTransform panel;
	public GameObject showBtn;
	public GameObject hideBtn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		//text.text = new string ();
		iText.text = "";
		iText.Select();
		iText.ActivateInputField();
	}

	public void PickColor(int i){
		text.color = colors [i];
		placeholder.color = colors [i];
	}

	public void ShowPanel(bool enable){
		Vector3 pos = panel.localPosition;
		//float y = enable ? 300 : -250;


		//float y = enable ? 0 : -250;

		//personal
		float y = enable ? 70: -120;

		panel.localPosition = new Vector3 (pos.x, y, pos.z);
		hideBtn.SetActive (enable);
		showBtn.SetActive (!enable);
	}
}

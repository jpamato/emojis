using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resultante : MonoBehaviour {

	public GameObject splash;
	public GameObject encuesta;
	public Text text;
	public Text textPrefab;

	public GameObject draggable;

	public Image emojiPrefab;
	public Sprite[] emojis;

	Menu menu;
	bool menuEnabled;
	GameObject menuGO;

	int draggCount;

	DrawScreen dscreen;
	WWWFormImage post;

	// Use this for initialization
	void Start () {
		menu = GetComponent<Menu> ();
		menuGO = gameObject.transform.Find ("Menu").gameObject;
		menuEnabled = true;

		dscreen = menu.DrawScreen.GetComponent<DrawScreen> ();
		post = GetComponent<WWWFormImage> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RenderText(){
		GameObject go = Instantiate (draggable);
		go.name = go.name + "_" + draggCount;
		draggCount++;
		Text t = Instantiate (textPrefab) as Text;
		//t.rectTransform.position = text.rectTransform.position;
		t.text = text.text;
		t.color = text.color;
		RectTransform rt = go.transform as RectTransform;
		rt.anchorMin = t.rectTransform.anchorMin;
		rt.anchorMax = t.rectTransform.anchorMax;
		rt.pivot = t.rectTransform.pivot;
		rt.sizeDelta = t.rectTransform.sizeDelta;
		t.rectTransform.SetParent (go.transform.Find("content"), false);
		go.transform.SetParent (gameObject.transform, false);

		//go.GetComponentInChildren<PointerResize> ().ResetOrigin ();

		CloseTxtM();
	}

	public void RenderEmoji(int i){
		GameObject go = Instantiate (draggable);
		go.name = go.name + "_" + draggCount;
		draggCount++;
		Image im = Instantiate (emojiPrefab) as Image;
		im.sprite = emojis [i];

		im.rectTransform.SetParent (go.transform.Find("content"), false);
		go.transform.SetParent (gameObject.transform, false);

		CloseEmojiM ();
	}

	public void ShowDraw(){
		menu.DrawScreen.SetActive (false);
		gameObject.SetActive (true);
	}

	public void Clean(){
		Events.ResetFrame ("");
		menuEnabled = !menuEnabled;
		//menuGO.SetActive (menuEnabled);
	}

	public void CloseDrawM(){
		foreach (GameObject swipe in dscreen.swipeList)
			Destroy(swipe);

		dscreen.swipeList.Clear ();
		
		menu.DrawScreen.SetActive (false);
		gameObject.SetActive (true);
	}

	public void CloseTxtM(){
		menu.TextScreen.SetActive (false);
		gameObject.SetActive (true);
	}

	public void CloseEmojiM(){
		menu.EmojiScreen.SetActive (false);
		gameObject.SetActive (true);
	}

	public void PostImage(){
		Game.Instance.enviando.EnableLoading (true);
		post.SendImage ();
		//Restart ();	
	}

	public void Restart(){
		if (dscreen != null) {
			foreach (GameObject swipe in dscreen.swipeList)
				Destroy (swipe);
			dscreen.swipeList.Clear ();
		}
		Events.DeleteFrame ();
		if (menu != null) {
			menu.DrawScreen.SetActive (false);
			menu.TextScreen.SetActive (false);
			menu.EmojiScreen.SetActive (false);
		}
		gameObject.SetActive (false);
		Events.ResetForm ();
		encuesta.SetActive (true);
		splash.SetActive (true);
	}

	public void NewPhoto(){		
		Events.NewPhoto ();
		gameObject.SetActive (false);
	}
}

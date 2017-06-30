using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {	

	public GameObject TextScreen;
	public GameObject DrawScreen;
	public GameObject EmojiScreen;	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TextButton(){
		TextScreen.SetActive (true);
		gameObject.SetActive (false);
	}

	public void DrawButton(){
		DrawScreen.SetActive (true);
		gameObject.SetActive (false);
	}

	public void EmojiButton(){
		EmojiScreen.SetActive (true);
		gameObject.SetActive (false);
	}
}

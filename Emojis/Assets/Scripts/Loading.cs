using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

	Text text;
	int count;

	bool isLoading;

	void Start(){
		text = GetComponent<Text> ();
	}

	public void EnableLoading(bool enable){
		isLoading = enable;
		text.enabled = enable;
		if (enable) {
			text.enabled = enable;
			StartCoroutine (StartLoading ());
		}
	}

	IEnumerator StartLoading(){
		if (count == 0)
			text.text = "Enviando";
		else
			text.text += ".";

		count++;
		if(count>3)count=0;

		if (isLoading) {			
			yield return new WaitForSeconds (0.1f);
			StartCoroutine (StartLoading());
		}

	}
}

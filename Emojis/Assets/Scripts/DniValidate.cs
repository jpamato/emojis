using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DniValidate : MonoBehaviour {

	InputField ifield;

	// Use this for initialization
	void Start () {
		ifield = GetComponent<InputField> ();
		ifield.onValidateInput += delegate(string input, int charIndex, char addedChar){ return NumberValidate(addedChar);};
	}

	private char NumberValidate(char toValidate){
		Regex regex = new Regex ("[0-9]");
		if (!regex.IsMatch (toValidate+""))
			toValidate = '\0';		
		return toValidate;				
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

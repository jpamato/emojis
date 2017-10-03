using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VKeyboardMultiInput : MonoBehaviour {

	public InputField[] inpuFields;

	KeyboardVirtual keyboard;

	void Start(){
		keyboard = GetComponent<KeyboardVirtual> ();
		Events.SelectField += SelectField;
		keyboard.inpuField = inpuFields [0];
	}

	void OnDestroy(){
		Events.SelectField -= SelectField;
	}

	void SelectField(int i){
		keyboard.inpuField = inpuFields [i];
	}

}

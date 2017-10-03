using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Form : MonoBehaviour {

	public InputField[] inpuFields;
	public Dropdown compania;
	public Button listo;

	public string PATH = "/Datos/";

	string filename = "data.txt";

	string actualPath;

	string txt;

	// Use this for initialization
	void Start () {

		Events.ResetForm += ResetForm;

		#if UNITY_EDITOR
		actualPath = Directory.GetParent(Application.dataPath).FullName+PATH;
		#elif UNITY_STANDALONE_WIN
		actualPath = Directory.GetParent(Application.dataPath).FullName+PATH;
		#endif

		Import(actualPath+filename);
	}

	void OnDestroy(){
		Events.ResetForm -= ResetForm;
	}

	// Update is called once per frame
	void Update () {
		
	}


	public void CheckForm(){
		listo.interactable = true;
		foreach (InputField i in inpuFields) {
			if (i.text == "") {
				listo.interactable = false;
				return;
			}
		}
		if(compania.captionText.text==compania.options[0].text)
			listo.interactable = false;
	}

	void Import(string file){

		/*string filePath = "Dialogs/" + file.Replace (".json", "");
		TextAsset text = Resources.Load<TextAsset> (filePath);*/

		/*StreamReader reader = new StreamReader(file);
		txt = reader.ReadToEnd ();*/

		txt = File.ReadAllText (file);
		//print (file+": "+txt);
	}

	public void SaveTxt(){
		txt += "#NOMBRE:"+inpuFields[0].text;
		txt += ";APELLIDO:"+inpuFields[1].text;
		txt += ";TIPO_DOC:DNI";
		txt += ";NRO_DOC:"+inpuFields[2].text;
		txt += ";MAIL:"+inpuFields[3].text;
		txt += ";TELEFONO_MOVIL:"+inpuFields[4].text;
		txt += ";PROV_TELEF_MOVIL:" + compania.captionText.text;
		// Initialise mes cellules visuel et detruit les murs en fonction des cellules virtuel

		//Debug.Log(txt);

		//using (FileStream fs = new FileStream(actualPath+System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".txt", FileMode.Create)){
		using (FileStream fs = new FileStream(actualPath+filename, FileMode.Create)){
			using (StreamWriter writer = new StreamWriter(fs)){
				writer.Write(txt);
			}
		}

		ResetForm ();

		compania.captionText.text = compania.options [0].text;

		transform.parent.gameObject.SetActive (false);
	}

	void ResetForm(){
		foreach (InputField i in inpuFields)
			i.text = "";
	}
}

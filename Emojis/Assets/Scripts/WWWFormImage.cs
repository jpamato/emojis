using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class WWWFormImage : MonoBehaviour {

	public Camera cam;
	public string screenShotURL= "";

	public int size;
	public int xOfS,yOfS;

	bool takeShot;

	public string PATH = "";

	void Awake () {

		#if UNITY_EDITOR
		DirectoryInfo dir = new DirectoryInfo(Directory.GetParent(Application.dataPath).FullName+PATH);
		#elif UNITY_STANDALONE_WIN
		DirectoryInfo dir = new DirectoryInfo(Directory.GetParent(Application.dataPath).FullName+PATH);
		#elif UNITY_STANDALONE_OSX
		DirectoryInfo dir = new DirectoryInfo(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName+PATH);
		#endif

		FileInfo[] info = dir.GetFiles("*.json");
		foreach (FileInfo f in info) {
			StartCoroutine(Import(f.FullName));
		}
		//foreach (string file in System.IO.Directory.GetFiles(Application.dataPath+PATH)){}
		}

	IEnumerator Import(string file){
		/*string filePath = "Dialogs/" + file.Replace (".json", "");
		TextAsset text = Resources.Load<TextAsset> (filePath);*/

		WWW www = new WWW ("file://" + file);
		yield return www;
		string text = www.text;

		var N = JSON.Parse (text);

		screenShotURL = N ["url"];
	}

	// Use this for initialization
	public void SendImage() {
		Game.Instance.Clean ();
		StartCoroutine(UploadPNG());
		//takeShot=true;
	}

	void LateUpdate(){
		if (takeShot) {
			print ("aca");
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			RenderTexture rt = new RenderTexture (width,height,32);
			cam.targetTexture = rt;
		Game.Instance.tex = new Texture2D( size, size, TextureFormat.RGB24, false );

			cam.Render ();
			RenderTexture.active = rt;

			// Read screen contents into the texture
		print((xOfS+((width-size)/2))+ "-" +(yOfS+((height-size)/2)) + "-" + (xOfS+(width-(width-size)/2)) + "-" + (yOfS+(height-(height-size)/2)));

		Game.Instance.tex.ReadPixels( new Rect(xOfS+((width-size)/2), yOfS+((height-size)/2), xOfS+(width-(width-size)/2), yOfS+(height-(height-size)/2)), 0, 0 );
		Game.Instance.tex.Apply();

			cam.targetTexture = null;
			RenderTexture.active = null;
			Destroy (rt);

			takeShot = false;
		}
	}

	IEnumerator UploadPNG() {
		
		// We should only read the screen after all rendering is complete
		//yield return new WaitForEndOfFrame();

		int width = Screen.width;
		int height = Screen.height;
		RenderTexture rt = new RenderTexture (width,height,32);
		cam.targetTexture = rt;
		Game.Instance.tex = new Texture2D( size, size, TextureFormat.RGB24, false );

		cam.Render ();
		RenderTexture.active = rt;

		// Read screen contents into the texture
		Game.Instance.tex.ReadPixels( new Rect(xOfS+((width-size)/2), yOfS+((height-size)/2), xOfS+(width-(width-size)/2), yOfS+(height-(height-size)/2)), 0, 0 );
		Game.Instance.tex.Apply();

		cam.targetTexture = null;
		RenderTexture.active = null;
		Destroy (rt);

		// Encode texture into PNG
		byte[] bytes = Game.Instance.tex.EncodeToPNG();

		//Destroy( tex );

		// Create a Web Form
		WWWForm form = new WWWForm();
		//form.AddField("frameCount", Time.frameCount.ToString());
		form.AddBinaryData("file", bytes, "screenShot.png", "image/png");
		//form.AddBinaryData("file", bytes, "screenShot_"+System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".png", "image/png");

		print (screenShotURL);
		// Upload to a cgi script
		WWW w = new WWW(screenShotURL, form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error);
		}
		else {
			print("Finished Uploading Screenshot");
		}

		//System.IO.File.WriteAllBytes ("C:\\Documentos\\Unity\\Proyectos\\Emojis\\screenshot.png",bytes);

		Game.Instance.Fin ();
	}

}

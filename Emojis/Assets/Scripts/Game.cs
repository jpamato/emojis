using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public bool portrait;

	public Vector2 defaultResolution;

	static Game mInstance = null;

	public float yLimit0 = 84;
	public float yLimit1 = 1019;
	public float xLimit0 = 365;
	public float xLimit1 = 1307;

	public Resultante result;

	public GameObject fin;
	public GameObject encuesta;

	public Texture2D tex;

	public Loading enviando;

	public static Game Instance
	{
		get
		{
			if (mInstance == null)
			{
				// Debug.LogError("Algo llama a Game antes de inicializarse");
			}
			return mInstance;
		}
	}
	void Awake () {
		mInstance = this;
		Input.multiTouchEnabled = false;
	}
	void Start  ()
	{
		xLimit0 *= Screen.currentResolution.width / Game.Instance.defaultResolution.x;
		xLimit1 *= Screen.currentResolution.width / Game.Instance.defaultResolution.x;
		yLimit0 *= Screen.currentResolution.height / Game.Instance.defaultResolution.y;
		yLimit1 *= Screen.currentResolution.height / Game.Instance.defaultResolution.y;
	}
	void OnDestroy()
	{
		
	}

	public void Clean(){
		if (result.isActiveAndEnabled)
			result.Clean ();
	}

	public void Fin (){
		enviando.EnableLoading (false);
		fin.SetActive (true);
		fin.GetComponentInChildren<RawImage> ().texture = tex;
		Invoke ("Restart", 3f);
	}

	public void Restart(){
		fin.SetActive (false);
		tex = null;
		result.Restart ();
	}
}

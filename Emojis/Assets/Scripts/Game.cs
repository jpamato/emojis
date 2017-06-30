using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public bool portrait;

	public Vector2 defaultResolution;

	static Game mInstance = null;

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
		
	}
	void OnDestroy()
	{
		
	}
}

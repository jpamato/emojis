using UnityEngine;
using System.Collections;

public static class Events {
	
    //public static System.Action<bool> OnPicker = delegate { };
    //public static System.Action GameIntro = delegate { };

	public static System.Action<string> ResetFrame = delegate { };

	public static System.Action DeleteFrame = delegate { };

	public static System.Action<bool> DrawPanelShow = delegate { };

}

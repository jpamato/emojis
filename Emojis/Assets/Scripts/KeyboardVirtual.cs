using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardVirtual : MonoBehaviour {

    public InputField inpuField;
    public int maxChars;

    public void Clciked(Button button)
    {
       string str = button.GetComponentInChildren<Text>().text;
        
       if (inpuField.text.Length >= maxChars) return;
            inpuField.text += str;
    }
    public void EraseLast()
    {
        
        if (inpuField.text.Length > 0)
        {
            string str = inpuField.text;
            print("borra " + str);
            str = str.Remove(str.Length - 1);
            print("borra " + str);
            inpuField.text = str;
        }

    }
}

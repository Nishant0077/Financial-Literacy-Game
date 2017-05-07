using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshResize : MonoBehaviour {

    public TextMesh tm;

    private void Start()
    {
        string builder = "";
        tm.text = "";
        float rowLimit = 0.5f; //find the sweet spot    
        string text = "This is some text we'll use to demonstrate word wrapping. It would be too easy if a proper wrapping was already implemented in Unity :)";
        string[] parts = text.Split(' ');
        for (int i = 0; i < parts.Length; i++)
        {
            Debug.Log(parts[i]);
            tm.text += parts[i] + " ";
            if (tm.GetComponent<Renderer>().bounds.extents.x > rowLimit)
            {
                tm.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
            }
            builder = tm.text;
        }
    }

    
}

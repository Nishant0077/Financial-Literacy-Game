using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightText : MonoBehaviour {

    Color originalColor;
    int originalFontSize;
    AudioSource audioSource;
    AudioClip audioClip;

	// Use this for initialization
	void Start () {

         originalColor = GetComponent<TextMesh>().color;
        originalFontSize = GetComponent<TextMesh>().fontSize;
        audioSource = this.GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        
        GetComponent<TextMesh>().color = Color.yellow ;
        GetComponent<TextMesh>().fontSize = 60;
        audioSource.PlayOneShot(audioClip);
    }

    void OnMouseExit()
    {
        GetComponent<TextMesh>().color = originalColor;
        GetComponent<TextMesh>().fontSize = originalFontSize;
    }
}

////////////////////////////////////////////////////////////////////////////////
//
// CRYSTAL CLEAR SOFTWARE
// Copyright 2012 Crystal Clear Software. http://ccsoft.ru
// All Rights Reserved. CCsoft Bear Shooter
// @author Osipov Stanislav lacost.20@gmail.com
//
//
// NOTICE: Crystal Soft does not allow to use, modify, or distribute this file
// for any purpose
//
////////////////////////////////////////////////////////////////////////////////

using System; 
using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public delegate void OnDocLoaded(string data);


public class WWWDocLoader : MonoBehaviour
{
	
	public OnDocLoaded LoadEvent;
	
	
	//--------------------------------------
	// PUBLIC METHODS
	//-------------------------------------
	
	public static WWWDocLoader createRequest() {
		GameObject inst =  new GameObject("WWWDocLoader");
		return inst.AddComponent<WWWDocLoader> ();
	}
	
	public void send(string url) {
  		WWW www = new WWW (url);
	    StartCoroutine(WaitForRequest (www));
    }
	
	
    public void send(string url, WWWForm form)  {
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }
	
	
	//--------------------------------------
	// PRIVATE METHODS
	//-------------------------------------
	
	
	private IEnumerator WaitForRequest(WWW www) {
        yield return www;

        // check for errors
        if (www.error == null) {
			SendEvent(www.text);
			Destroy(gameObject);
        } else {
			SendEvent(string.Empty);
			Destroy(gameObject);
        }
	}

	private void SendEvent(string data) {
		if (LoadEvent != null) {
			LoadEvent(data);
		}
	}

   
}


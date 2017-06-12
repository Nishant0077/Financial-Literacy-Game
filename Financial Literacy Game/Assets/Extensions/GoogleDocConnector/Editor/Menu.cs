////////////////////////////////////////////////////////////////////////////////
//  
// @module V2D
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;


namespace SA.GoogleDoc {

public class GoogleDocConnectorMenu : EditorWindow {
	
	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	#if UNITY_EDITOR

	[MenuItem("Window/Stan's Assets/Google Doc Connector", false, 1)]
	public static void Edit() {
		Selection.activeObject = Settings.Instance;
	}

	


	#endif

}

}

////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;


namespace SA.GoogleDoc {

[CustomEditor(typeof(GoogleData))]
public class GoogleDataEditor : Editor {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public override void OnInspectorGUI() {

		Component[] components = data.gameObject.GetComponents<Component>();


		GenericMenu menu  = new GenericMenu ();



		foreach(Component c in components) {
			if(c.Equals(data)) {
				continue;
			}

			bool valid = true;
			foreach(DataComponentConnection coonection in data.connections) {
				if(c.Equals(coonection.component)) {
					valid = false;
					break;
				}
			}

			if(!valid) {
				continue;
			}

			FieldInfo [] fields = c.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
			if(fields.Length > 0) {
				menu.AddItem (new GUIContent (c.GetType().Name), false, AddMenuCallBack, c);
			}

		}


		if(menu.GetItemCount() == 0) {
			if(data.connections.Count == 0) {
				EditorGUILayout.HelpBox("No valid data recivers avaliable", MessageType.Info);
			}
		}



		foreach(DataComponentConnection connection in data.connections) {
			EditorGUI.indentLevel = 0;
			EditorGUILayout.BeginHorizontal();
			connection.IsOpened = EditorGUILayout.Foldout(connection.IsOpened, connection.component.GetType().Name);
			if (GUILayout.Button("X", GUILayout.Width(25))) {
				data.connections.Remove(connection);
				return;
			}
			EditorGUILayout.EndHorizontal();

			if (connection.component == null) {
				data.connections.Remove(connection);
				break;
			}

			if(connection.IsOpened) {
				connection.Draw();
			}
		}

		EditorGUILayout.Space();

		if(menu.GetItemCount() != 0) {
			
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			if(GUILayout.Button("Add New Connection",  GUILayout.Width(150))) {
				menu.ShowAsContext ();
			}
			EditorGUILayout.Space();
			
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.Space();
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public GoogleData data {
		get {
			return target as GoogleData;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void AddMenuCallBack(object obj) {
		Component component = obj as Component;

		DataComponentConnection connection = new DataComponentConnection();
		connection.owner = data.gameObject;
		connection.component = component;
		connection.componentName = component.GetType().Name;
		data.connections.Add(connection);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}

}

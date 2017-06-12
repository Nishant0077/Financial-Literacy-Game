//#define SA_DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR && !SA_DEBUG
using UnityEditor;
using System.Reflection;
#endif

namespace SA.GoogleDoc {

[System.Serializable]
public class DataComponentConnection  {

	public bool IsOpened = true;
	public bool IsMarkForDel = false;
	public Component component;

	public GameObject owner;
	
	[SerializeField]
	[HideInInspector]
	public string componentName;

	[SerializeField]
	public List<DataDelegate> delegates =  new List<DataDelegate>();


	List<string> fieldsNames;
	List<bool> fieldConnectable;
#if UNITY_EDITOR && !SA_DEBUG
	List<FieldInfo> fieldsVelues;
	private int filedId = 0;
#endif






	public void Draw() {
		#if UNITY_EDITOR && !SA_DEBUG
		ReflectObjectFields();

		EditorGUI.indentLevel = 1;
		EditorGUILayout.Space();

		foreach(DataDelegate del in delegates) {

			EditorGUILayout.BeginVertical (GUI.skin.box);
			if (del.bConnectableField) {

				EditorGUILayout.BeginHorizontal();
				del.IsOpened = EditorGUILayout.Foldout(del.IsOpened, del.FiledName);
				EditorGUILayout.Space();

				GUIStyle s =  new GUIStyle(EditorStyles.miniButton);
				s.padding =  new RectOffset();
				if (GUILayout.Button("X", s, GUILayout.Width(15))) {
					delegates.Remove(del);
					return;
				}
				EditorGUILayout.EndHorizontal();

				if(del.IsOpened) {
					del.Draw();
				}
			}
			EditorGUILayout.EndVertical();
		}

		if(fieldsNames.Count == 0) {
			if(delegates.Count == 0) {
				EditorGUILayout.HelpBox("No valid data fileds avaliable", MessageType.Info);
			}
			
		} else {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			
			GenericMenu popupMenu = new GenericMenu();
			
			if (GUILayout.Button("Attach Filed", EditorStyles.miniButton,  GUILayout.Width(90))) {
				
				List<string> names = new List<string>();
				for (int i = 0; i < fieldsNames.Count; i++) {
					if (fieldConnectable[i]) {
						string newName = fieldsNames[i].Replace('.', '/');
						names.Add(newName);
						
						popupMenu.AddItem(new GUIContent(newName), false, PopupMenuCallback, i);
					}
				}
				
				popupMenu.ShowAsContext();
			}
			
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.Space();
		#endif
	}

	private void PopupMenuCallback(object obj) {
		#if UNITY_EDITOR && !SA_DEBUG
		int filedId = (int)obj;

		if (fieldConnectable [filedId]) {
			DataDelegate del = new DataDelegate();
			del.owner = owner;
			del.bConnectableField = fieldConnectable[filedId];
			
			del.FiledName = fieldsNames[filedId];
			del.field = fieldsVelues[filedId];
			delegates.Add(del);
		}
		#endif
	}

	private void ReflectObjectFields() {
		#if UNITY_EDITOR && !SA_DEBUG
		FieldInfo [] fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
		fieldsNames = new List<string> ();
		fieldsVelues = new List<FieldInfo>();
		fieldConnectable = new List<bool> ();

		
		foreach(FieldInfo f in fields) {			
			bool valid = true;
			foreach(DataDelegate del in delegates) {
				if(f.Name.Equals(del.FiledName)) {
					del.field = f;
					valid = false;
					break;
				}
			}
			
			if(!valid) {
				continue;
			}

			bool bConnectableField = CheckType(f);
			if (!bConnectableField) {
				if (f.FieldType.GetFields().Length > 0 && f.FieldType != typeof(System.Boolean) && !f.FieldType.IsEnum) {
					ReflectField(f, f.Name, 0);
				}
			} else {
				fieldsNames.Add(f.Name);
				fieldsVelues.Add(f);
				fieldConnectable.Add (bConnectableField);
			}
			
		}
		
		if(filedId >  fieldsNames.Count - 1) {
			filedId = 0;
		}
		#endif
	}


	#if UNITY_EDITOR && !SA_DEBUG
	private bool ReflectField(FieldInfo field, string parentFieldName, int reflectLevel) {

		FieldInfo [] fields = field.FieldType.GetFields(BindingFlags.Public | BindingFlags.Instance);

		if (fields.Length == 0) {
			return false;
		}

		foreach(FieldInfo f in fields) {												
			string fieldName = parentFieldName + "." + f.Name;

			bool valid = true;
			foreach(DataDelegate del in delegates) {
				if(fieldName.Trim().Equals(del.FiledName)) {
					del.field = f;
					valid = false;
					break;
				}
			}

			if(!valid) {
				continue;
			}

			bool bConnectableField = CheckType(f);
			if (!bConnectableField) {
				if (f.FieldType.GetFields().Length > 0 && f.FieldType != typeof(System.Boolean) && !f.FieldType.IsEnum) {
					ReflectField(f, fieldName, 0);
				}
			} else {
				fieldsNames.Add(fieldName);
				fieldsVelues.Add(f);
				fieldConnectable.Add (bConnectableField);
			}
		}

		return true;

	}
	#endif

	#if UNITY_EDITOR && !SA_DEBUG
	private bool CheckType(FieldInfo field) {

		System.Type fieldType = field.FieldType;
		if(fieldType == typeof(System.Int32) ||
		   fieldType == typeof(System.Int64) ||
		   fieldType == typeof(System.Single) ||
		   fieldType == typeof(System.String) ||
		   fieldType == typeof(int[]) ||
		   fieldType == typeof(long[]) ||
		   fieldType == typeof(float[]) ||
		   fieldType == typeof(string[]) ||
		   fieldType == typeof(System.Collections.Generic.List<string>) ||
		   fieldType == typeof(System.Collections.Generic.List<int>) ||
		   fieldType == typeof(System.Collections.Generic.List<long>) ||
		   fieldType == typeof(System.Collections.Generic.List<float>) ||
		   fieldType == typeof(System.Collections.Generic.Dictionary<string, string>) ||
		   fieldType == typeof(System.Collections.Generic.Dictionary<string, int>) ||
		   fieldType == typeof(System.Collections.Generic.Dictionary<string, long>) ||
		   fieldType == typeof(System.Collections.Generic.Dictionary<string, float>)) {
			return true;
		} else {
			return false;
		}

	}
	#endif


}

}

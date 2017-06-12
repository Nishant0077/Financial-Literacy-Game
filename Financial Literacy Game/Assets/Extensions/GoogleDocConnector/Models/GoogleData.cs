////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SA.GoogleDoc {
	
[ExecuteInEditMode]
public class GoogleData : MonoBehaviour {

	[SerializeField]
	public List<DataComponentConnection> connections = new List<DataComponentConnection> ();

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	void Awake() {
		ApplyData();
	}

	void Update() {

	#if UNITY_EDITOR

		if(Selection.activeGameObject == gameObject && !Application.isPlaying) {
			ApplyData();
		}
	#endif
	
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public void ApplyData() {
		foreach(DataComponentConnection connection in connections) {
			foreach(DataDelegate del in connection.delegates) {
				object val = null;
				switch (del.dataType) {
				case DataType.Value: {
					switch (del.variableType) {
					case VariableType.String: {
						val = API.GetValue<string>(del.SourceDocName, del.cell);
					} break;
					case VariableType.Int: {
						val = API.GetValue<int>(del.SourceDocName, del.cell);
					} break;
					case VariableType.Long: {
						val = API.GetValue<long>(del.SourceDocName, del.cell);
					} break;
					case VariableType.Float: {
						val = API.GetValue<float>(del.SourceDocName, del.cell);
					} break;
					default: Debug.LogWarning("Unrecognizable VariableType"); break;
					}
				}break;
				case DataType.Array: {

					switch (del.variableType) {
					case VariableType.String: {
						val = API.GetArray<string>(del.SourceDocName, del.range);
					} break;
					case VariableType.Int: {
						val = API.GetArray<int>(del.SourceDocName, del.range);
					} break;
					case VariableType.Long: {
						val = API.GetArray<long>(del.SourceDocName, del.range);
					} break;
					case VariableType.Float: {
						val = API.GetArray<float>(del.SourceDocName, del.range);
					} break;
					default: Debug.LogWarning("Unrecognizable VariableType"); break;
					}
				}break;
				case DataType.Range: {
					switch (del.variableType) {
					case VariableType.String: {
						val = API.GetList<string>(del.SourceDocName, del.range);
					} break;
					case VariableType.Int: {
						val = API.GetList<int>(del.SourceDocName, del.range);
					} break;
					case VariableType.Long: {
						val = API.GetList<long>(del.SourceDocName, del.range);
					} break;
					case VariableType.Float: {
						val = API.GetList<float>(del.SourceDocName, del.range);
					} break;
					default: Debug.LogWarning("Unrecognizable VariableType"); break;
					}
				}break;
				case DataType.HashMap: {
					switch (del.variableType) {
					case VariableType.String: {
						val = API.GetDictionary<string, string>(del.SourceDocName, del.hashMap);
					} break;
					case VariableType.Int: {
						val = API.GetDictionary<string, int>(del.SourceDocName, del.hashMap);
					} break;
					case VariableType.Long: {
						val = API.GetDictionary<string, long>(del.SourceDocName, del.hashMap);
					} break;
					case VariableType.Float: {
						val = API.GetDictionary<string, float>(del.SourceDocName, del.hashMap);
					} break;
					default: Debug.LogWarning("Unrecognizable VariableType"); break;
					}
				}break;
				default: Debug.LogWarning("Unrecognizable DataType"); break;
				}

				if (!del.SourceDocName.Equals(string.Empty)) {
					GoogleDocConnectorEvalScript evalScript = gameObject.AddComponent<GoogleDocConnectorEvalScript> ();
					evalScript.EvalAssignValue (connection.componentName, del.FiledName, val);

				#if UNITY_EDITOR
					DestroyImmediate (evalScript, true);
				#else
					Destroy(evalScript);
				#endif
				}

			}
		}
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}

}

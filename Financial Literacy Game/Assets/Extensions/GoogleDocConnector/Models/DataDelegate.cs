namespace SA.GoogleDoc {


using UnityEngine;
using System;
using System.IO;
using System.Collections;

#if UNITY_EDITOR && !SA_DEBUG
using System.Reflection;
using UnityEditor;
#endif


[System.Serializable]
public class DataDelegate  {
	public string FiledName;
	public bool IsOpened = true;

	public Cell cell =  new Cell();
	public CellRange range =  new CellRange();
	public CellDictionaryRange hashMap = new CellDictionaryRange();

	public DataType dataType = DataType.Value;
	public VariableType variableType = VariableType.String;

	public bool bConnectableField = false;
	public string SourceDocName = string.Empty;

	public GameObject owner;

	private GoogleData _googleData;


#if UNITY_EDITOR && !SA_DEBUG

	public FieldInfo field;

	GUIContent SourceDocLable  = new GUIContent("DataSource [?]", "Select Google document which will be used as data source");
	GUIContent WorksheetLabel = new GUIContent ("Worksheet [?]", "Select Worksheet which will be used as data source");

	GUIContent CellLabel  = new GUIContent("Cell [?]", "Spesify cell as data source");


	GUIContent RangeDirLabel  	= new GUIContent("Direction [?]", "Range Direction");
	GUIContent RangeLabel  		= new GUIContent("Worksheet Range [?]", "Spesify cell range as data source");

	GUIContent HashMapLabel = new GUIContent ("Worksheet HashMap [?]", "Specify data source for HashMap");
	GUIContent HashMapColShift = new GUIContent ("Column shift", "Column shift for data selection");
	GUIContent HashMapRowShift = new GUIContent ("Row shift", "Row shift for data selection");

	GUIContent StartCellLabel  		= new GUIContent("Start Cell [?]", "Range Start Cell");
	GUIContent UseLineBreakLabel  	= new GUIContent("Use Line Break [?]", "If this option is enabled, when row/col lenght reached it will increment row/col index");
	GUIContent RangeLength  		= new GUIContent("Range Length [?]", "Spesify range line length");

	public void Draw() {

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(SourceDocLable);

		EditorGUI.BeginChangeCheck ();

		int index =  Settings.Instance.GetDocIndexByName(SourceDocName);  
		index = EditorGUILayout.Popup(index, Settings.Instance.GetDocNames());

		DocTemplate tpl = Settings.Instance.GetDocByIndex(index);

		EditorGUILayout.EndHorizontal();

		if(tpl == null) {
			SourceDocName = string.Empty;
		} else {
			SourceDocName = tpl.name;
			
			if (!File.Exists(Settings.GetCachePath(SourceDocName))) {
				EditorGUILayout.BeginVertical();
				EditorGUILayout.Space ();
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.HelpBox ("Such Document doesn't exist. You need to cache this file", MessageType.Warning);
				EditorGUILayout.Space();
				if (GUILayout.Button("Settings", GUILayout.Width(150))) {
					Selection.activeObject = Settings.Instance;
				}

				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical();
			} else {

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField (WorksheetLabel);
					
				tpl.selectedWorksheet = EditorGUILayout.Popup (tpl.selectedWorksheet, tpl.GetWorksheetNames ());
					
				EditorGUILayout.EndHorizontal ();
				
				DefineDataType();
				
				if(SourceDocName.Equals(string.Empty)) {
					return;
				}

				switch(dataType) {
				case DataType.Value:
					DrawCellSelect();
					break;
				case DataType.Array:
				case DataType.Range:
					DrawRangeSelect();
					break;
				case DataType.HashMap:
					DrawHashMapSelect();
					break;
				}

			}
		}

		if (EditorGUI.EndChangeCheck()) {
			UpdateData ();
		}

		// Update data with first draw of Inspector GUI
		if (_googleData == null && !SourceDocName.Equals(string.Empty)) {
			_googleData = owner.GetComponent<GoogleData>();
			UpdateData ();
		}
	}

	private void DrawHashMapSelect() {
		EditorGUILayout.LabelField (HashMapLabel);

		EditorGUI.indentLevel++; {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(RangeDirLabel);
			hashMap.cellRange.direction = (RanageDirection) EditorGUILayout.EnumPopup(hashMap.cellRange.direction);
			EditorGUILayout.EndHorizontal();
			

			
		

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(StartCellLabel);
			hashMap.cellRange.StartCell.key = EditorGUILayout.TextField (hashMap.cellRange.StartCell.key);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(HashMapColShift);
			int newColShift = EditorGUILayout.IntField(hashMap.columnShift);
			if (newColShift >= 0) {
				hashMap.columnShift = newColShift;
			}
			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(HashMapRowShift);
			int newRowShift = EditorGUILayout.IntField(hashMap.rowShift);
			if (newRowShift >= 0) {
				hashMap.rowShift = newRowShift;
			}
			
			EditorGUILayout.EndHorizontal();


				



			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(UseLineBreakLabel);
			hashMap.cellRange.useLinebreak =  EditorGUILayout.Toggle(hashMap.cellRange.useLinebreak);
			EditorGUILayout.EndHorizontal();
			
			if(hashMap.cellRange.useLinebreak) {
				EditorGUI.indentLevel++;
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(RangeLength);
				
				int newLineLength = EditorGUILayout.IntField(hashMap.cellRange.LineLength);
				if (newLineLength > 0) {
					hashMap.cellRange.LineLength = newLineLength;
				}
				
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel--;
			}			


			
		}

		//TODO: Enter/Exit Play Mode need fix
		if (bTypesMismatch) {
			EditorGUILayout.HelpBox(typesMismatchInfo, MessageType.Warning);
		}

		EditorGUI.indentLevel--;

	}

	private void DrawRangeSelect() {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(RangeLabel);
		EditorGUILayout.EndHorizontal();

		EditorGUI.indentLevel++; {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(RangeDirLabel);
			range.direction = (RanageDirection) EditorGUILayout.EnumPopup(range.direction);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(StartCellLabel);
			range.StartCell.key = EditorGUILayout.TextField (range.StartCell.key);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(UseLineBreakLabel);
			range.useLinebreak =  EditorGUILayout.Toggle(range.useLinebreak);
			EditorGUILayout.EndHorizontal();
			
			if(range.useLinebreak) {
				EditorGUI.indentLevel++;
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(RangeLength);

				int newLineLenght =  EditorGUILayout.IntField(range.LineLength);
				if (newLineLenght > 0) {
					range.LineLength = newLineLenght;
				}

				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel--;
			}






		

		} EditorGUI.indentLevel--;


		//TODO: Enter/Exit Play Mode need fix
		if (bTypesMismatch) {
			EditorGUILayout.HelpBox(typesMismatchInfo, MessageType.Warning);
		}



	}

	private bool bTypesMismatch = false;
	private string typesMismatchInfo = string.Empty;

	private void DrawCellSelect() {
	


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(CellLabel);
		cell.key = EditorGUILayout.TextField (cell.key);
		EditorGUILayout.EndHorizontal();

		//TODO: Enter/Exit Play Mode need fix
		if (bTypesMismatch) {
			EditorGUILayout.HelpBox(typesMismatchInfo, MessageType.Warning);
		}


	}

	private void UpdateData() {		
		switch (variableType) {
		case VariableType.Int: {
			if (dataType == DataType.Value) {
				bTypesMismatch = !API.IsValueOfType<int>(SourceDocName, cell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Int' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, cell) : string.Empty;
			} else if (dataType == DataType.Range || dataType == DataType.Array) {
				bTypesMismatch = !API.IsValueOfType<int>(SourceDocName, range.StartCell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Int' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, range.StartCell) : string.Empty;
			} else {
				Cell hCell = new Cell ();
				hCell.col = hashMap.cellRange.StartCell.col + hashMap.columnShift;
				hCell.row = hashMap.cellRange.StartCell.row + hashMap.rowShift;
				bTypesMismatch = !API.IsValueOfType<int>(SourceDocName, hCell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Int' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, hCell) : string.Empty;
			}
		}break;
		case VariableType.Long: {
			if (dataType == DataType.Value) {
				bTypesMismatch = !API.IsValueOfType<long>(SourceDocName, cell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Long' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, cell) : string.Empty;
			} else if (dataType == DataType.Range || dataType == DataType.Array) {
				bTypesMismatch = !API.IsValueOfType<long>(SourceDocName, range.StartCell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Long' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, range.StartCell) : string.Empty;
			} else {
				Cell hCell = new Cell ();
				hCell.col = hashMap.cellRange.StartCell.col + hashMap.columnShift;
				hCell.row = hashMap.cellRange.StartCell.row + hashMap.rowShift;
				bTypesMismatch = !API.IsValueOfType<long>(SourceDocName, hCell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Long' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, hCell) : string.Empty;
			}
		}break;
		case VariableType.Float: {
			if (dataType == DataType.Value) {
				bTypesMismatch = !API.IsValueOfType<float>(SourceDocName, cell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Float' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, cell) : string.Empty;
			} else if (dataType == DataType.Range || dataType == DataType.Array) {
				bTypesMismatch = !API.IsValueOfType<float>(SourceDocName, range.StartCell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Float' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, range.StartCell) : string.Empty;
			} else {
				Cell hCell = new Cell ();
				hCell.col = hashMap.cellRange.StartCell.col + hashMap.columnShift;
				hCell.row = hashMap.cellRange.StartCell.row + hashMap.rowShift;
				bTypesMismatch = !API.IsValueOfType<float>(SourceDocName, hCell);
				typesMismatchInfo = bTypesMismatch ? "Target DataType = 'Float' <-> Source Data = " 
					+ API.GetValue<string>(SourceDocName, hCell) : string.Empty;
			}
		}break;
		default: break;
		}
	
		if (_googleData != null) {
			_googleData.ApplyData ();
		}
	}
	
	private void DefineDataType() {
		if (field == null) {
			return;
		}
		if(field.FieldType == typeof(System.Int32)) {
			dataType = DataType.Value;
			variableType = VariableType.Int;
		} else if(field.FieldType == typeof(System.Int64)) {
			dataType = DataType.Value;
			variableType = VariableType.Long;
		} else if(field.FieldType == typeof(System.Single)) {
			dataType = DataType.Value;
			variableType = VariableType.Float;
		} else if (field.FieldType == typeof(System.String)) {
			dataType = DataType.Value;
			variableType = VariableType.String;
		} else if (field.FieldType == typeof(string[])) {
			dataType = DataType.Array;
			variableType = VariableType.String;
		} else if (field.FieldType == typeof(int[])) {
			dataType = DataType.Array;
			variableType = VariableType.Int;
		} else if (field.FieldType == typeof(long[])) {
			dataType = DataType.Array;
			variableType = VariableType.Long;
		} else if (field.FieldType == typeof(float[])) {
			dataType = DataType.Array;
			variableType = VariableType.Float;
		} else if(field.FieldType == typeof(System.Collections.Generic.List<string>)) {
			dataType = DataType.Range;
			variableType = VariableType.String;
		} else if(field.FieldType == typeof(System.Collections.Generic.List<int>)) {
			dataType = DataType.Range;
			variableType = VariableType.Int;
		}else if(field.FieldType == typeof(System.Collections.Generic.List<long>)) {
			dataType = DataType.Range;
			variableType = VariableType.Long;
		}else if(field.FieldType == typeof(System.Collections.Generic.List<float>)) {
			dataType = DataType.Range;
			variableType = VariableType.Float;
		}else if(field.FieldType == typeof(System.Collections.Generic.Dictionary<string, string>)) {
			dataType = DataType.HashMap;
			variableType = VariableType.String;
		}else if(field.FieldType == typeof(System.Collections.Generic.Dictionary<string, int>)) {
			dataType = DataType.HashMap;
			variableType = VariableType.Int;
		}else if(field.FieldType == typeof(System.Collections.Generic.Dictionary<string, long>)) {
			dataType = DataType.HashMap;
			variableType = VariableType.Long;
		}else if(field.FieldType == typeof(System.Collections.Generic.Dictionary<string, float>)) {
			dataType = DataType.HashMap;
			variableType = VariableType.Float;
		}else {
			Debug.LogWarning("Unrecognizable Field Type Detecled");
		}
	}

	#endif

}

}


using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SA.GoogleDoc {

[System.Serializable]
public class DocTemplate  {
	
	public string key;

	[SerializeField]
	private List<WorksheetTemplate> _worksheets = new List<WorksheetTemplate>();

	private string _creationTime = string.Empty;

	public string name = string.Empty;

	public bool IsTableListOpen = false;
	public bool IsOpen = true;

	public string docName;
	public int docIndex = 0;
	public int selectedWorksheet = 0;

	public void InitDoc() {
		int add = 1;
		// Select unique Document name
		do {
			name = "NewDocument" + (Settings.Instance.docs.Count + add);
			add++;
		} while (Settings.Instance.GetDocByName(name) != null);

		// Add table with gid=0 by default
		_worksheets.Add (new WorksheetTemplate("Sheet1", 0));
	}

	public void AddNewWorksheet() {
		_worksheets.Add (new WorksheetTemplate("Sheet" + (_worksheets.Count + 1), _worksheets.Count));
	}

	public void RemoveWorksheet(WorksheetTemplate list) {
		_worksheets.Remove (list);
	}

	public int GetWorksheetId(string name) {
		int id = 0;
		foreach (WorksheetTemplate list in _worksheets) {
			if (list.listName == name) {
				return list.listId;
			}
		}

		return id;
	}

	public string GetWorksheetName(int id) {
		string name = id.ToString();
		foreach (WorksheetTemplate list in _worksheets) {
			if (list.listId == id) {
				return list.listName;
			}
		}
		
		return name;
	}

	public bool WorksheetExist(string name) {
		int same = 0;
		foreach (WorksheetTemplate list in _worksheets) {
			if (list.listName == name) {
				same++;
			}
		}
	
		return same > 1 ? true : false;
	}

	public bool WorksheetExist(int id) {
		int same = 0;
		foreach (WorksheetTemplate list in _worksheets) {
			if (list.listId == id) {
				same++;
			}
		}
		
		return same > 1 ? true : false;
	}

	public void UpdateTime(string timeStr) {
		_creationTime = timeStr;
	}

	public string[] GetWorksheetNames() {
		List<string> names =  new List<string>();
		foreach(WorksheetTemplate w in _worksheets) {
			names.Add(w.listName);
		}
		
		return names.ToArray();
	}

	public List<WorksheetTemplate> Worksheets {
		get {
			return _worksheets;
		}
	}

	public string CreationTime {
		get {
			if (File.Exists(Settings.GetCachePath(name))) {
				StreamReader reader = new StreamReader(Settings.GetCachePath(name));
				_creationTime = reader.ReadLine();
				reader.Close();
			} else { _creationTime = string.Empty; }

			return _creationTime;
		}
	}

}
}

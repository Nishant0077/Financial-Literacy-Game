using UnityEngine;
using System.Collections;

namespace SA.GoogleDoc {

[System.Serializable]
public class WorksheetTemplate {
	public string listName;
	public int listId;
	
	public WorksheetTemplate(string name, int id) {
		listName = name;
		listId = id;
	}
}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour {

    List<string> names = new List<string>() { "Choose a Goal", "Car", "College", "House" };
    public Dropdown dropdown;
    public Text selectedName;

    public void Dropdown_IndexChanged(int index)
    {
        selectedName.text = names[index];
    }

	
	void Start () {

        PopulateList();
		
	}
	
	void PopulateList()
    {
        dropdown.AddOptions(names);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour {

    List<string> names = new List<string>() { "Choose a Goal", "Car", "College", "House" };
    public Dropdown dropdown;
    public string selectedName;
    public Text goalAmountText;
    

    public void Dropdown_IndexChanged(int index)
    {
        selectedName = names[index];
        PlayerPrefs.SetString("Goal", selectedName);
        goalAmountText.text = "The goal amount text is:  " + GetGoalAmount();
        Debug.Log("The " + "goal " + " is " + PlayerPrefs.GetString("Goal"));
    }

    public int GetGoalAmount()
    {
        if (selectedName.Equals("Car"))
        {
            return 2000;
        }

        if (selectedName.Equals("College"))
        {
            return 16000;
        }

        if (selectedName.Equals("House"))
        {
            return 30000;
        }

        else return 0;

    }
	
	void Start () {

        PopulateList();
		
	}
	
	void PopulateList()
    {
        dropdown.AddOptions(names);
    }
}

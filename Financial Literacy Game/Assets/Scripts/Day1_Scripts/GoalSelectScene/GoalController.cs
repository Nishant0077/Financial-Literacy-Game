using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour {

    List<string> names = new List<string>() { "Choose a Goal", "Car", "College", "House" };
    public Dropdown dropdown;
    public string selectedName;
    public Text goalAmountText;
    public int goalAmount;
    

    public void Dropdown_IndexChanged(int index)
    {
        selectedName = names[index];
        // PlayerPrefs.SetString("Goal", selectedName);

        if (selectedName.Equals("Car"))
        {

            goalAmount = 20000;
        }

        if (selectedName.Equals("College"))
        {
            goalAmount = 160000;
        }

        if (selectedName.Equals("House"))
        {
            goalAmount = 300000;
        }

        if ((!names.Contains(selectedName)))
        {
            goalAmount = 0;
        }

        PlayerPrefs.SetInt("GoalAmount", goalAmount);

        goalAmountText.text = "The goal amount is:  " + goalAmount;
       // Debug.Log("The " + "goal " + " is " + PlayerPrefs.GetString("Goal"));
    }

  
        

    
	
	void Start () {

        PopulateList();
		
	}
	
	void PopulateList()
    {
        dropdown.AddOptions(names);
    }
}

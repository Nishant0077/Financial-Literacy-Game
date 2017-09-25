using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllnessGeneratorScript : MonoBehaviour {

    int randomNumber;
    int numberOfIllnessScenarios = 4;
    public Text illnessText;
    public Text impactOnSavingsText;
    float noIllnessPenalty = 0;
    float illness1Penalty = 1000;
    float illness2Penalty = 2500;
    float illness3Penalty = 5000;
    float impactOnSavings;

	// Use this for initialization
	void Start () {

        randomNumber = Random.Range(1, numberOfIllnessScenarios);

        if (randomNumber == 1)   // no illness scenario 
        {
            illnessText.text = "You are perfectly healthy";
            impactOnSavings = noIllnessPenalty;
        }

        if (randomNumber == 2)   // illness scenario #1
        {
            illnessText.text = "You have cavities in your teeth. Immediate treatment is advised.";
            impactOnSavings = illness1Penalty;
        }

        if (randomNumber == 3)   // illness scenario #2 
        {
            illnessText.text = "You have a severe vitamin deficiency. You will be prescribed an extended course " +
                                "of medication.";
            impactOnSavings = illness2Penalty;
        }

        if (randomNumber == 4)   // illness scenario #3 
        {
            illnessText.text = "You have been diagnosed with kidney stones. They will need to be removed surgically";
            impactOnSavings = illness3Penalty;
        }

        impactOnSavingsText.text = "Medical Bill: $" + impactOnSavings.ToString();

    }
	
	
}

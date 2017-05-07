using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BudgetingMethodNextButton : MonoBehaviour {

    public void OnMouseDown()
    {
        SceneManager.LoadScene("Day2_Scenario1");
    }
}

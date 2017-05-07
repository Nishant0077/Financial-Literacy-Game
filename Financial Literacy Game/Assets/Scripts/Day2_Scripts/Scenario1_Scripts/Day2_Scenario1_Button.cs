using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day2_Scenario1_Button : MonoBehaviour
{

    public void OnMouseDown()
    {
        SceneManager.LoadScene("Day2_Scenario2");
    }
}

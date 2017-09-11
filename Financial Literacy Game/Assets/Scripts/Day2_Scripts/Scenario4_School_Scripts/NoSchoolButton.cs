using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSchoolButton : MonoBehaviour {


    public void OnMouseDown()
    {
        // set the back button active
        GameControllerSchool.b.gameObject.SetActive(true);
        Debug.Log("No program selected");       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetInputFromInputField : MonoBehaviour
{
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;

        //or simply use the line below, 
        //input.onEndEdit.AddListener(SubmitName);  // This also works
    }

    private void SubmitName(string id)
    {
        Debug.Log(id);
        PlayerPrefs.SetString("PlayerID", id);
        Debug.Log("The player id is: " + PlayerPrefs.GetString("PlayerID"));
       // input.text = "";
        SceneManager.LoadScene("scene0");
    }
}

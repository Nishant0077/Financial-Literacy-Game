using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScamController : MonoBehaviour
{
       
    public static Button b;
    //private BudgetingMethodController bc;

    public void Start()
    {
        b = GameObject.FindGameObjectsWithTag("BackToInboxScamScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);
    }

    
}

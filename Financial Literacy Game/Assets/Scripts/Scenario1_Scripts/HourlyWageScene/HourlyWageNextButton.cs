﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HourlyWageNextButton : MonoBehaviour {

    public void OnMouseDown()
    {
        SceneManager.LoadScene("scenario1_CreditScore");
    }
}

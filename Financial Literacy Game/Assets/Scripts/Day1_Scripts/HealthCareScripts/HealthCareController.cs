using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCareController : MonoBehaviour {

    public float monthlyDeduction = 0;
    public float projectedSavingsAmount;
    public Text projectedSavingsText;
    public enum healthCarePlanType {No, Basic, Standard, Premium};
    public static Button b;

    public healthCarePlanType currentPlan;

    private void Start()
    {
        b = GameObject.FindGameObjectsWithTag("BackToInboxHealthcareScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);
    }

}
